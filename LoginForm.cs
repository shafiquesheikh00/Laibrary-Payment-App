using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Laibrary_Payment_App
{
    public partial class LoginForm : Form
    {
        public LibraryDbContext? DbContext { get; set; }
        public string LoggedInUser { get; private set; } = string.Empty;

        public LoginForm()
        {
            InitializeComponent();
        }

        private LibraryDbContext GetDbContext()
        {
            if (DbContext != null)
            {
                return DbContext;
            }

            if (Program.ServiceProvider != null)
            {
                var scope = Program.ServiceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<LibraryDbContext>();
                if (db != null)
                {
                    return db;
                }
            }

            return new LibraryDbContext();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            lblError.Visible = false;

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowError("Please enter your username.");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowError("Please enter your password.");
                txtPassword.Focus();
                return;
            }

            try
            {
                btnLogin.Enabled = false;
                btnLogin.Text = "Logging in...";
                Cursor = Cursors.WaitCursor;

                using var dbContext = GetDbContext();

                // 1. Look up user by username (case-insensitive)
                var user = await dbContext.Users
                    .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());

                if (user == null)
                {
                    ShowError("Invalid username or password.");
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                // 2. Check active state
                if (!user.IsActive)
                {
                    ShowError("Your account has been deactivated. Please contact an administrator.");
                    return;
                }

                // 3. Verify password (supports hashed or seeded fallback)
                if (!PasswordSecurity.VerifyPassword(password, user.PasswordHash))
                {
                    ShowError("Invalid username or password.");
                    txtPassword.Clear();
                    txtPassword.Focus();
                    return;
                }

                // 4. Update last login timestamp
                user.LastLoginDate = DateTime.UtcNow;
                await dbContext.SaveChangesAsync();

                // 5. Store in global session
                CurrentSession.CurrentUser = user;
                LoggedInUser = user.FullName;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                ShowError($"Database connection error: {ex.Message}");
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Login";
                Cursor = Cursors.Default;
            }
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            lblError.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
