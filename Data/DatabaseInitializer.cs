using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Laibrary_Payment_App.Data
{
    public enum DatabaseInitStatus
    {
        Success,
        PlaceholderCredentials,
        MissingConfiguration,
        ServerUnavailable,
        InvalidCredentials,
        PermissionDenied,
        MigrationFailed,
        UnknownError
    }

    public class DatabaseInitializationResult
    {
        public bool IsSuccess { get; set; }
        public DatabaseInitStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool DatabaseExisted { get; set; }
        public List<string> AppliedMigrations { get; set; } = new();
        public Exception? Exception { get; set; }
    }

    public class DatabaseInitializer
    {
        private readonly LibraryDbContext _dbContext;

        public DatabaseInitializer(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DatabaseInitializationResult> InitializeAsync(CancellationToken cancellationToken = default)
        {
            var result = new DatabaseInitializationResult();

            try
            {
                var connectionString = _dbContext.Database.GetConnectionString();

                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    result.IsSuccess = false;
                    result.Status = DatabaseInitStatus.MissingConfiguration;
                    result.Message = "Connection string 'DefaultConnection' is missing or empty in configuration.";
                    return result;
                }

                // Check for placeholder credentials
                if (connectionString.Contains("YOUR_SQL_USERNAME", StringComparison.OrdinalIgnoreCase) ||
                    connectionString.Contains("YOUR_SQL_PASSWORD", StringComparison.OrdinalIgnoreCase))
                {
                    result.IsSuccess = false;
                    result.Status = DatabaseInitStatus.PlaceholderCredentials;
                    result.Message = "SQL credentials are not configured. Please update 'User Id' and 'Password' in 'appsettings.json' (or 'appsettings.local.json').";
                    return result;
                }

                // Step 1: Check SQL Server connectivity
                var canConnectInitially = false;
                try
                {
                    canConnectInitially = await _dbContext.Database.CanConnectAsync(cancellationToken);
                }
                catch (SqlException ex)
                {
                    return ClassifySqlException(ex, "Failed to verify database connectivity.");
                }

                result.DatabaseExisted = canConnectInitially;

                // Step 2: Retrieve pending migrations
                IEnumerable<string> pendingMigrations;
                try
                {
                    pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
                }
                catch (SqlException ex)
                {
                    return ClassifySqlException(ex, "Failed to retrieve pending migrations.");
                }

                var pendingList = pendingMigrations.ToList();

                // Step 3 & 4: Create database if missing and apply migrations
                try
                {
                    await _dbContext.Database.MigrateAsync(cancellationToken);
                    result.AppliedMigrations = pendingList;
                }
                catch (SqlException ex)
                {
                    return ClassifySqlException(ex, "Failed while creating database or applying migrations.");
                }
                catch (Exception ex)
                {
                    result.IsSuccess = false;
                    result.Status = DatabaseInitStatus.MigrationFailed;
                    result.Message = $"Migration failure: {ex.Message}";
                    result.Exception = ex;
                    return result;
                }

                // Step 5: Database ready
                result.IsSuccess = true;
                result.Status = DatabaseInitStatus.Success;
                result.Message = result.DatabaseExisted
                    ? $"Database 'LaibraryPaymentDB' is ready. Applied {result.AppliedMigrations.Count} pending migration(s)."
                    : $"Database 'LaibraryPaymentDB' created and ready. Applied {result.AppliedMigrations.Count} migration(s).";

                return result;
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Status = DatabaseInitStatus.UnknownError;
                result.Message = $"Unexpected database initialization error: {ex.Message}";
                result.Exception = ex;
                return result;
            }
        }

        private static DatabaseInitializationResult ClassifySqlException(SqlException ex, string context)
        {
            var result = new DatabaseInitializationResult
            {
                IsSuccess = false,
                Exception = ex
            };

            switch (ex.Number)
            {
                case 18456: // Login failed for user
                    result.Status = DatabaseInitStatus.InvalidCredentials;
                    result.Message = $"Invalid SQL credentials: {ex.Message.Trim()} (Error #{ex.Number})";
                    break;

                case -1: // Timeout / unreachable
                case 2:  // Server not found
                case 53: // Network path not found
                case 4060: // Cannot open database requested by the login (if db doesn't exist yet and connecting directly without master fallback)
                    if (ex.Number == 4060)
                    {
                        // Database doesn't exist yet, which is expected before creation
                        result.Status = DatabaseInitStatus.ServerUnavailable;
                        result.Message = $"Database not found or cannot open: {ex.Message.Trim()} (Error #{ex.Number})";
                    }
                    else
                    {
                        result.Status = DatabaseInitStatus.ServerUnavailable;
                        result.Message = $"SQL Server unavailable: Could not connect to instance. Ensure SQL Server service is running. (Error #{ex.Number})";
                    }
                    break;

                case 229: // The EXECUTE / SELECT permission was denied
                case 262: // CREATE DATABASE permission denied in database 'master'
                    result.Status = DatabaseInitStatus.PermissionDenied;
                    result.Message = $"Database creation permission denied: {ex.Message.Trim()} (Error #{ex.Number})";
                    break;

                default:
                    result.Status = DatabaseInitStatus.MigrationFailed;
                    result.Message = $"{context} SQL Error #{ex.Number}: {ex.Message.Trim()}";
                    break;
            }

            return result;
        }
    }
}
