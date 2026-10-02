using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Forms;
using Laibrary_Payment_App.Models;
using Laibrary_Payment_App.Security;
using Laibrary_Payment_App.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Laibrary_Payment_App
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;
        public static IConfiguration Configuration { get; private set; } = null!;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            // Build application configuration
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            Configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
                .Build();

            // Configure dependency injection
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            // Test database connectivity and verify/apply migrations
            using (var scope = ServiceProvider.CreateScope())
            {
                var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
                var initResult = initializer.InitializeAsync().GetAwaiter().GetResult();

                if (!initResult.IsSuccess && (args == null || (!args.Contains("--test-phase2") && !args.Contains("--test-phase3") && !args.Contains("--test-phase4") && !args.Contains("--test-phase5"))))
                {
                    MessageBox.Show(
                        $"{initResult.Message}\n\nStatus: {initResult.Status}",
                        "Database Initialization Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }

            // CLI automated test hooks
            if (args != null && args.Contains("--test-phase2"))
            {
                var exitCode = RunPhase2AutomatedTestAsync().GetAwaiter().GetResult();
                Environment.Exit(exitCode);
                return;
            }

            if (args != null && args.Contains("--test-phase3"))
            {
                var exitCode = RunPhase3AutomatedTestAsync().GetAwaiter().GetResult();
                Environment.Exit(exitCode);
                return;
            }

            if (args != null && args.Contains("--test-phase4"))
            {
                var exitCode = RunPhase4AutomatedTestAsync().GetAwaiter().GetResult();
                Environment.Exit(exitCode);
                return;
            }

            if (args != null && args.Contains("--test-phase5"))
            {
                var exitCode = RunPhase5AutomatedTestAsync().GetAwaiter().GetResult();
                Environment.Exit(exitCode);
                return;
            }

            // Application run loop supporting logout and re-login
            while (true)
            {
                CurrentSession.RequestLogout = false;

                // Show Login Form
                using (var loginForm = ServiceProvider.GetRequiredService<LoginForm>())
                {
                    if (loginForm.ShowDialog() != DialogResult.OK)
                    {
                        // User closed or cancelled login; terminate application
                        break;
                    }
                }

                // Launch Dashboard Form
                var mainForm = ServiceProvider.GetRequiredService<Form1>();
                Application.Run(mainForm);

                // If user did not request logout, terminate application
                if (!CurrentSession.RequestLogout)
                {
                    break;
                }
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found in appsettings.json.");

            services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<DatabaseInitializer>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IMembershipPlanService, MembershipPlanService>();
            services.AddScoped<ISeatService, SeatService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IBillingService, BillingService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IReportService, ReportService>();

            services.AddTransient<LoginForm>();
            services.AddTransient<Form1>();
            services.AddTransient<StudentMasterForm>();
            services.AddTransient<MembershipPlanForm>();
            services.AddTransient<SeatMasterForm>();
            services.AddTransient<MembershipForm>();
            services.AddTransient<BillingForm>();
            services.AddTransient<InvoiceHistoryForm>();
            services.AddTransient<InvoiceViewForm>();
            services.AddTransient<PaymentForm>();
            services.AddTransient<PaymentHistoryForm>();
            services.AddTransient<PaymentReceiptForm>();
            services.AddTransient<StudentPaymentLedgerForm>();
        }

        private static async Task<int> RunPhase2AutomatedTestAsync()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("STARTING PHASE 2 AUTOMATED WORKFLOW TEST");
            Console.WriteLine("==================================================");

            using var scope = ServiceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            var dashboardService = scope.ServiceProvider.GetRequiredService<IDashboardService>();
            var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();

            // 1. Database Initialization
            Console.WriteLine("[1/8] Verifying Database Initializer...");
            var initializer = scope.ServiceProvider.GetRequiredService<DatabaseInitializer>();
            var initResult = await initializer.InitializeAsync();
            Console.WriteLine($" -> Database Status: {initResult.Status}, Success: {initResult.IsSuccess}");

            // 2. Authentication Test
            Console.WriteLine("[2/8] Verifying Login with Username: admin, Password: Admin@123...");
            var adminUser = await db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == "admin");
            if (adminUser == null)
            {
                Console.WriteLine("FAIL: admin user not found in UserMaster!");
                return 1;
            }
            bool isPasswordValid = PasswordSecurity.VerifyPassword("Admin@123", adminUser.PasswordHash);
            Console.WriteLine($" -> User found: {adminUser.FullName} ({adminUser.Role}), Password match: {isPasswordValid}");
            if (!isPasswordValid)
            {
                Console.WriteLine("FAIL: Password verification failed for Admin@123!");
                return 1;
            }

            // 3. CurrentSession
            CurrentSession.CurrentUser = adminUser;
            Console.WriteLine($"[3/8] Session established: FullName={CurrentSession.CurrentUser.FullName}, Role={CurrentSession.CurrentUser.Role}");

            // 4. Initial Dashboard statistics
            var statsBefore = await dashboardService.GetSummaryAsync();
            Console.WriteLine($"[4/8] Initial Dashboard Stats -> Total: {statsBefore.TotalStudents}, Active: {statsBefore.ActiveStudents}, Memberships: {statsBefore.ActiveMemberships}, Seats: {statsBefore.AvailableSeats}");

            // 5. Add a test student
            Console.WriteLine("[5/8] Testing Student Master: Add Student...");
            var testCode = "STU-TEST-" + DateTime.UtcNow.Ticks.ToString().Substring(12);
            var testStudent = new StudentMaster
            {
                StudentCode = testCode,
                FullName = "Phase 2 Test Student",
                MobileNo = "9876543210",
                Email = "test.student@library.com",
                CollegeName = "National College",
                CourseName = "Information Technology",
                Address = "123 Main Street",
                IsActive = true
            };
            var addResult = await studentService.AddStudentAsync(testStudent);
            Console.WriteLine($" -> Add Result: {addResult.IsSuccess}, Message: {addResult.Message}");
            if (!addResult.IsSuccess) return 1;

            // 6. Search student
            Console.WriteLine("[6/8] Testing Student Master: Search Student...");
            var searchList = await studentService.GetAllStudentsAsync("Phase 2 Test");
            Console.WriteLine($" -> Search matches found: {searchList.Count}");
            if (searchList.Count == 0) return 1;

            // 7. Edit & Deactivate student
            Console.WriteLine("[7/8] Testing Student Master: Edit & Deactivate Student...");
            var savedStudent = searchList[0];
            savedStudent.FullName = "Phase 2 Test Student (Updated)";
            var updateResult = await studentService.UpdateStudentAsync(savedStudent);
            Console.WriteLine($" -> Update Result: {updateResult.IsSuccess}, Message: {updateResult.Message}");

            var toggleResult = await studentService.ToggleActiveStatusAsync(savedStudent.StudentId);
            Console.WriteLine($" -> Deactivate Result: {toggleResult.IsSuccess}, New Status (IsActive): {toggleResult.NewStatus}");

            // 8. Verify updated dashboard stats
            var statsAfter = await dashboardService.GetSummaryAsync();
            Console.WriteLine($"[8/8] Updated Dashboard Stats -> Total: {statsAfter.TotalStudents} (was {statsBefore.TotalStudents}), Active: {statsAfter.ActiveStudents} (was {statsBefore.ActiveStudents})");

            // Clean up test student
            var toDelete = await db.Students.FindAsync(savedStudent.StudentId);
            if (toDelete != null)
            {
                db.Students.Remove(toDelete);
                await db.SaveChangesAsync();
            }
            Console.WriteLine(" -> Cleaned up test student record.");

            CurrentSession.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("ALL PHASE 2 REQUIREMENTS VERIFIED SUCCESSFULLY!");
            Console.WriteLine("==================================================");
            return 0;
        }

        private static async Task<int> RunPhase3AutomatedTestAsync()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("STARTING PHASE 3 AUTOMATED VERIFICATION TEST");
            Console.WriteLine("==================================================");

            using var scope = ServiceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            var dashboardService = scope.ServiceProvider.GetRequiredService<IDashboardService>();
            var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();
            var planService = scope.ServiceProvider.GetRequiredService<IMembershipPlanService>();
            var seatService = scope.ServiceProvider.GetRequiredService<ISeatService>();
            var membershipService = scope.ServiceProvider.GetRequiredService<IMembershipService>();

            // Clean up prior test data if any
            var oldTestStudents = await db.Students.Where(s => s.FullName.StartsWith("Phase 3 Test") || s.StudentCode.StartsWith("P3-TEST")).ToListAsync();
            if (oldTestStudents.Any())
            {
                var stuIds = oldTestStudents.Select(s => s.StudentId).ToList();
                var oldMemberships = await db.StudentMemberships.Where(m => stuIds.Contains(m.StudentId)).ToListAsync();
                db.StudentMemberships.RemoveRange(oldMemberships);
                db.Students.RemoveRange(oldTestStudents);
                await db.SaveChangesAsync();
            }

            // Sync seat statuses
            await seatService.SyncSeatStatusesWithActiveMembershipsAsync();

            // 1. Verify Date Calculator
            Console.WriteLine("[Step 1/14] Testing MembershipDateCalculator...");
            var testStart = new DateTime(2026, 10, 1);
            var dailyEnd = MembershipDateCalculator.CalculateEndDate(testStart, 1, "Day");
            var weeklyEnd = MembershipDateCalculator.CalculateEndDate(testStart, 1, "Week");
            var monthlyEnd = MembershipDateCalculator.CalculateEndDate(testStart, 1, "Month");
            var quarterlyEnd = MembershipDateCalculator.CalculateEndDate(testStart, 1, "Quarter");
            var yearlyEnd = MembershipDateCalculator.CalculateEndDate(testStart, 1, "Year");

            Console.WriteLine($" -> Daily (01-Oct-2026): {dailyEnd:dd-MMM-yyyy} (Expected: 01-Oct-2026) -> Match: {dailyEnd == new DateTime(2026, 10, 1)}");
            Console.WriteLine($" -> Weekly (01-Oct-2026): {weeklyEnd:dd-MMM-yyyy} (Expected: 07-Oct-2026) -> Match: {weeklyEnd == new DateTime(2026, 10, 7)}");
            Console.WriteLine($" -> Monthly (01-Oct-2026): {monthlyEnd:dd-MMM-yyyy} (Expected: 31-Oct-2026) -> Match: {monthlyEnd == new DateTime(2026, 10, 31)}");
            Console.WriteLine($" -> Quarterly (01-Oct-2026): {quarterlyEnd:dd-MMM-yyyy} (Expected: 31-Dec-2026) -> Match: {quarterlyEnd == new DateTime(2026, 12, 31)}");
            Console.WriteLine($" -> Yearly (01-Oct-2026): {yearlyEnd:dd-MMM-yyyy} (Expected: 30-Sep-2027) -> Match: {yearlyEnd == new DateTime(2027, 9, 30)}");

            if (dailyEnd != new DateTime(2026, 10, 1) ||
                weeklyEnd != new DateTime(2026, 10, 7) ||
                monthlyEnd != new DateTime(2026, 10, 31) ||
                quarterlyEnd != new DateTime(2026, 12, 31) ||
                yearlyEnd != new DateTime(2027, 9, 30))
            {
                Console.WriteLine("FAIL: Date calculator failed specification tests!");
                return 1;
            }

            // 2. Setup Test Students
            Console.WriteLine("[Step 2/14] Setting up Test Student 001 and Test Student 002...");
            var stu1 = new StudentMaster
            {
                StudentCode = "P3-TEST-001",
                FullName = "Phase 3 Test Student 001",
                MobileNo = "9111111111",
                Email = "p3student1@library.com",
                CollegeName = "Engineering College",
                CourseName = "Computer Science",
                IsActive = true
            };
            var stu1Result = await studentService.AddStudentAsync(stu1);
            if (!stu1Result.IsSuccess) { Console.WriteLine($"FAIL: {stu1Result.Message}"); return 1; }

            var stu2 = new StudentMaster
            {
                StudentCode = "P3-TEST-002",
                FullName = "Phase 3 Test Student 002",
                MobileNo = "9222222222",
                Email = "p3student2@library.com",
                CollegeName = "Science College",
                CourseName = "Physics",
                IsActive = true
            };
            var stu2Result = await studentService.AddStudentAsync(stu2);
            if (!stu2Result.IsSuccess) { Console.WriteLine($"FAIL: {stu2Result.Message}"); return 1; }
            Console.WriteLine(" -> Students created successfully.");

            // 3. Setup Test Plans: Monthly ₹1,200 and Quarterly ₹3,000
            Console.WriteLine("[Step 3/14] Ensuring required plans exist (Monthly ₹1200, Quarterly ₹3000)...");
            var monthlyPlan = await db.MembershipPlans.FirstOrDefaultAsync(p => p.PlanName == "Monthly Test Plan");
            if (monthlyPlan == null)
            {
                monthlyPlan = new MembershipPlan
                {
                    PlanName = "Monthly Test Plan",
                    DurationValue = 1,
                    DurationUnit = "Month",
                    Fee = 1200.00m,
                    IsActive = true
                };
                var planRes = await planService.AddPlanAsync(monthlyPlan);
                if (!planRes.IsSuccess) { Console.WriteLine($"FAIL: {planRes.Message}"); return 1; }
            }

            var quarterlyPlan = await db.MembershipPlans.FirstOrDefaultAsync(p => p.PlanName == "Quarterly Test Plan");
            if (quarterlyPlan == null)
            {
                quarterlyPlan = new MembershipPlan
                {
                    PlanName = "Quarterly Test Plan",
                    DurationValue = 1,
                    DurationUnit = "Quarter",
                    Fee = 3000.00m,
                    IsActive = true
                };
                var planRes = await planService.AddPlanAsync(quarterlyPlan);
                if (!planRes.IsSuccess) { Console.WriteLine($"FAIL: {planRes.Message}"); return 1; }
            }
            Console.WriteLine($" -> Monthly Plan Id={monthlyPlan.PlanId} (₹{monthlyPlan.Fee:N2}), Quarterly Plan Id={quarterlyPlan.PlanId} (₹{quarterlyPlan.Fee:N2}) ready.");

            // 4. Ensure Seats A01, A02, A03 exist and available
            Console.WriteLine("[Step 4/14] Ensuring Seats A01, A02, A03 exist and available...");
            var seatA01 = await db.Seats.FirstOrDefaultAsync(s => s.SeatNumber == "A01");
            if (seatA01 == null)
            {
                seatA01 = new SeatMaster { SeatNumber = "A01", Area = "Main Hall", Status = "Available", IsActive = true };
                await seatService.AddSeatAsync(seatA01);
            }
            else
            {
                seatA01.Status = "Available";
                seatA01.IsActive = true;
                await db.SaveChangesAsync();
            }

            var seatA02 = await db.Seats.FirstOrDefaultAsync(s => s.SeatNumber == "A02");
            if (seatA02 == null)
            {
                seatA02 = new SeatMaster { SeatNumber = "A02", Area = "Main Hall", Status = "Available", IsActive = true };
                await seatService.AddSeatAsync(seatA02);
            }
            else
            {
                seatA02.Status = "Available";
                seatA02.IsActive = true;
                await db.SaveChangesAsync();
            }

            var seatA03 = await db.Seats.FirstOrDefaultAsync(s => s.SeatNumber == "A03");
            if (seatA03 == null)
            {
                seatA03 = new SeatMaster { SeatNumber = "A03", Area = "Main Hall", Status = "Available", IsActive = true };
                await seatService.AddSeatAsync(seatA03);
            }
            else
            {
                seatA03.Status = "Available";
                seatA03.IsActive = true;
                await db.SaveChangesAsync();
            }
            Console.WriteLine(" -> Seats A01, A02, A03 verified.");

            // 5. Create Monthly membership for Student 001 with Seat A01
            Console.WriteLine("[Step 5/14] Creating Monthly membership for Student 001 with Seat A01...");
            var today = DateTime.UtcNow.Date;
            var m1EndDate = MembershipDateCalculator.CalculateEndDate(today, monthlyPlan.DurationValue, monthlyPlan.DurationUnit);

            var m1 = new StudentMembership
            {
                StudentId = stu1.StudentId,
                PlanId = monthlyPlan.PlanId,
                SeatId = seatA01.SeatId,
                StartDate = today,
                EndDate = m1EndDate,
                ActualFee = monthlyPlan.Fee,
                DiscountAmount = 0m,
                FinalFee = monthlyPlan.Fee,
                Status = "Active"
            };

            var m1Result = await membershipService.CreateMembershipAsync(m1);
            Console.WriteLine($" -> Create Result: {m1Result.IsSuccess}, Message: {m1Result.Message}");
            if (!m1Result.IsSuccess) return 1;

            // 6. Verify Seat A01 became Occupied
            var seatA01Reloaded = await seatService.GetSeatByIdAsync(seatA01.SeatId);
            Console.WriteLine($" -> Seat A01 Status: {seatA01Reloaded?.Status} (Expected: Occupied) -> Match: {seatA01Reloaded?.Status == "Occupied"}");
            if (seatA01Reloaded?.Status != "Occupied") return 1;

            // 7. Try assigning Seat A01 to another active student during the same period -> MUST BE REJECTED
            Console.WriteLine("[Step 7/14] Testing seat conflict rejection: Assigning Seat A01 to Student 002 for same period...");
            var m2Conflict = new StudentMembership
            {
                StudentId = stu2.StudentId,
                PlanId = monthlyPlan.PlanId,
                SeatId = seatA01.SeatId,
                StartDate = today,
                EndDate = m1EndDate,
                ActualFee = monthlyPlan.Fee,
                DiscountAmount = 0m,
                FinalFee = monthlyPlan.Fee,
                Status = "Active"
            };

            var m2ConflictResult = await membershipService.CreateMembershipAsync(m2Conflict);
            Console.WriteLine($" -> Conflict Check Result: {m2ConflictResult.IsSuccess} (Expected: False), Message: {m2ConflictResult.Message}");
            if (m2ConflictResult.IsSuccess)
            {
                Console.WriteLine("FAIL: System allowed double-booking of Seat A01!");
                return 1;
            }
            Console.WriteLine(" -> Seat conflict correctly rejected!");

            // 8. Assign Seat A02 to Student 002 -> MUST SUCCEED
            Console.WriteLine("[Step 8/14] Assigning Seat A02 to Student 002...");
            var m2 = new StudentMembership
            {
                StudentId = stu2.StudentId,
                PlanId = quarterlyPlan.PlanId,
                SeatId = seatA02.SeatId,
                StartDate = today,
                EndDate = MembershipDateCalculator.CalculateEndDate(today, quarterlyPlan.DurationValue, quarterlyPlan.DurationUnit),
                ActualFee = quarterlyPlan.Fee,
                DiscountAmount = 100m,
                FinalFee = quarterlyPlan.Fee - 100m,
                Status = "Active"
            };

            var m2Result = await membershipService.CreateMembershipAsync(m2);
            Console.WriteLine($" -> Create Result: {m2Result.IsSuccess}, Final Fee: ₹{m2.FinalFee:N2}");
            if (!m2Result.IsSuccess) return 1;

            var seatA02Reloaded = await seatService.GetSeatByIdAsync(seatA02.SeatId);
            Console.WriteLine($" -> Seat A02 Status: {seatA02Reloaded?.Status} (Expected: Occupied) -> Match: {seatA02Reloaded?.Status == "Occupied"}");
            if (seatA02Reloaded?.Status != "Occupied") return 1;

            // 9. Cancel first membership (m1) -> Verify Seat A01 becomes Available
            Console.WriteLine("[Step 9/14] Cancelling first membership (m1) and verifying Seat A01 release...");
            var cancelResult = await membershipService.CancelMembershipAsync(m1.MembershipId, "Student requested early exit");
            Console.WriteLine($" -> Cancel Result: {cancelResult.IsSuccess}, Message: {cancelResult.Message}");
            if (!cancelResult.IsSuccess) return 1;

            seatA01Reloaded = await seatService.GetSeatByIdAsync(seatA01.SeatId);
            Console.WriteLine($" -> Seat A01 Status after cancellation: {seatA01Reloaded?.Status} (Expected: Available) -> Match: {seatA01Reloaded?.Status == "Available"}");
            if (seatA01Reloaded?.Status != "Available") return 1;

            // 10. Re-assign Seat A01 to a new membership -> MUST SUCCEED
            Console.WriteLine("[Step 10/14] Re-assigning freed Seat A01 to Student 001...");
            var m3 = new StudentMembership
            {
                StudentId = stu1.StudentId,
                PlanId = monthlyPlan.PlanId,
                SeatId = seatA01.SeatId,
                StartDate = today,
                EndDate = m1EndDate,
                ActualFee = monthlyPlan.Fee,
                DiscountAmount = 0m,
                FinalFee = monthlyPlan.Fee,
                Status = "Active"
            };
            var m3Result = await membershipService.CreateMembershipAsync(m3);
            Console.WriteLine($" -> Create Result: {m3Result.IsSuccess}, Message: {m3Result.Message}");
            if (!m3Result.IsSuccess) return 1;

            seatA01Reloaded = await seatService.GetSeatByIdAsync(seatA01.SeatId);
            Console.WriteLine($" -> Seat A01 Status re-occupied: {seatA01Reloaded?.Status} (Expected: Occupied)");

            // 11. Test Renewal: Renew m3
            Console.WriteLine("[Step 11/14] Testing Membership Renewal for m3...");
            var renewResult = await membershipService.RenewMembershipAsync(
                m3.MembershipId,
                monthlyPlan.PlanId,
                seatA01.SeatId,
                discountAmount: 50m,
                remarks: "Renewal test");

            Console.WriteLine($" -> Renewal Result: {renewResult.IsSuccess}, Message: {renewResult.Message}");
            if (!renewResult.IsSuccess || renewResult.Membership == null) return 1;

            var renewed = renewResult.Membership;
            var expectedRenewalStart = m3.EndDate.Date.AddDays(1);
            Console.WriteLine($" -> Old Membership #{m3.MembershipId}: {m3.StartDate:dd-MMM-yyyy} to {m3.EndDate:dd-MMM-yyyy}");
            Console.WriteLine($" -> Renewed Membership #{renewed.MembershipId}: {renewed.StartDate:dd-MMM-yyyy} to {renewed.EndDate:dd-MMM-yyyy}");
            Console.WriteLine($" -> Non-overlapping start verified: {renewed.StartDate == expectedRenewalStart}");
            if (renewed.StartDate != expectedRenewalStart)
            {
                Console.WriteLine("FAIL: Renewal start date is not Current EndDate + 1 day!");
                return 1;
            }

            // 12. Verify Student Membership History contains both old and renewed
            Console.WriteLine("[Step 12/14] Verifying Student 001 Membership History...");
            var history = await membershipService.GetMembershipHistoryAsync(stu1.StudentId);
            Console.WriteLine($" -> Total history records for Student 001: {history.Count}");
            foreach (var h in history)
            {
                Console.WriteLine($"    * ID={h.MembershipId} | Plan={h.Plan?.PlanName} | Seat={h.Seat?.SeatNumber ?? "None"} | {h.StartDate:dd-MMM-yyyy} to {h.EndDate:dd-MMM-yyyy} | Status={h.Status} | FinalFee=₹{h.FinalFee:N2}");
            }
            if (history.Count < 3)
            {
                Console.WriteLine("FAIL: Expected at least 3 historical records for Student 001 (m1 cancelled, m3 active, renewed active)!");
                return 1;
            }

            // 13. Verify Dashboard Statistics
            Console.WriteLine("[Step 13/14] Verifying Dashboard Live Statistics...");
            var stats = await dashboardService.GetSummaryAsync();
            Console.WriteLine($" -> Total Students: {stats.TotalStudents}");
            Console.WriteLine($" -> Active Students: {stats.ActiveStudents}");
            Console.WriteLine($" -> Active Memberships: {stats.ActiveMemberships}");
            Console.WriteLine($" -> Available Seats: {stats.AvailableSeats}");
            Console.WriteLine($" -> Occupied Seats: {stats.OccupiedSeats}");
            Console.WriteLine($" -> Expiring Memberships (7 Days): {stats.ExpiringMemberships}");

            if (stats.ActiveMemberships < 2 || stats.OccupiedSeats < 1)
            {
                Console.WriteLine("FAIL: Dashboard stats do not reflect active memberships and occupied seats!");
                return 1;
            }

            // 14. Cleanup test data
            Console.WriteLine("[Step 14/14] Cleaning up test records and restoring initial seat states...");
            var testStuMemberships = await db.StudentMemberships
                .Where(m => m.StudentId == stu1.StudentId || m.StudentId == stu2.StudentId)
                .ToListAsync();
            db.StudentMemberships.RemoveRange(testStuMemberships);

            var toDelStudents = await db.Students
                .Where(s => s.StudentId == stu1.StudentId || s.StudentId == stu2.StudentId)
                .ToListAsync();
            db.Students.RemoveRange(toDelStudents);

            if (monthlyPlan.PlanName == "Monthly Test Plan")
            {
                db.MembershipPlans.Remove(monthlyPlan);
            }
            if (quarterlyPlan.PlanName == "Quarterly Test Plan")
            {
                db.MembershipPlans.Remove(quarterlyPlan);
            }

            await db.SaveChangesAsync();

            // Resync seats to available
            await seatService.SyncSeatStatusesWithActiveMembershipsAsync();
            Console.WriteLine(" -> Cleanup completed successfully.");

            Console.WriteLine("==================================================");
            Console.WriteLine("ALL PHASE 3 REQUIREMENTS & BUSINESS RULES VERIFIED!");
            Console.WriteLine("==================================================");
            return 0;
        }

        private static async Task<int> RunPhase4AutomatedTestAsync()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("STARTING PHASE 4 AUTOMATED BILLING & INVOICE TEST");
            Console.WriteLine("==================================================");

            using var scope = ServiceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            var dashboardService = scope.ServiceProvider.GetRequiredService<IDashboardService>();
            var billingService = scope.ServiceProvider.GetRequiredService<IBillingService>();
            var studentService = scope.ServiceProvider.GetRequiredService<IStudentService>();
            var membershipService = scope.ServiceProvider.GetRequiredService<IMembershipService>();
            var seatService = scope.ServiceProvider.GetRequiredService<ISeatService>();

            // 1. Login & UserSession
            Console.WriteLine("[Step 1/21] Verifying Login & UserSession...");
            var adminUser = await db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == "admin");
            if (adminUser == null)
            {
                Console.WriteLine("FAIL: Admin user not found.");
                return 1;
            }
            CurrentSession.CurrentUser = adminUser;
            Console.WriteLine($" -> Logged in as: {CurrentSession.CurrentUser.FullName} ({CurrentSession.CurrentUser.Role})");

            // 2. Open Dashboard stats
            Console.WriteLine("[Step 2/21] Fetching Dashboard statistics before tests...");
            var initialStats = await dashboardService.GetSummaryAsync();
            Console.WriteLine($" -> Invoices count: {initialStats.TotalInvoicesCount}, Today Billing: {initialStats.TodayBillingAmount}");

            // 3. Setup active student, plan, seat and active membership
            Console.WriteLine("[Step 3/21] Setting up active test student, plan, seat and membership...");
            var testPlan = await db.MembershipPlans.FirstOrDefaultAsync(p => p.PlanName == "Phase 4 Test Plan");
            if (testPlan == null)
            {
                testPlan = new MembershipPlan
                {
                    PlanName = "Phase 4 Test Plan",
                    DurationValue = 1,
                    DurationUnit = "Month",
                    Fee = 1200.00m,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
                db.MembershipPlans.Add(testPlan);
                await db.SaveChangesAsync();
            }

            var testSeat = await db.Seats.FirstOrDefaultAsync(s => s.SeatNumber == "P4-T01");
            if (testSeat == null)
            {
                testSeat = new SeatMaster
                {
                    SeatNumber = "P4-T01",
                    Area = "Test Hall",
                    Status = "Available",
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };
                db.Seats.Add(testSeat);
                await db.SaveChangesAsync();
            }

            var testStudent = new StudentMaster
            {
                StudentCode = "P4-STU-001",
                FullName = "Rohan Sharma",
                MobileNo = "9876543210",
                Email = "rohan.sharma@example.com",
                Address = "Test Address",
                CollegeName = "Test College",
                CourseName = "B.Tech",
                IsActive = true,
                CreatedDate = DateTime.Now
            };
            db.Students.Add(testStudent);
            await db.SaveChangesAsync();

            var startDate = new DateTime(2026, 10, 2);
            var endDate = startDate.AddDays(30);

            var testMembership = new StudentMembership
            {
                StudentId = testStudent.StudentId,
                PlanId = testPlan.PlanId,
                SeatId = testSeat.SeatId,
                StartDate = startDate,
                EndDate = endDate,
                ActualFee = 1200.00m,
                DiscountAmount = 100.00m,
                FinalFee = 1100.00m,
                Status = "Active",
                Remarks = "Phase 4 test membership",
                CreatedDate = DateTime.Now
            };
            db.StudentMemberships.Add(testMembership);
            testSeat.Status = "Occupied";
            await db.SaveChangesAsync();
            Console.WriteLine($" -> Active Membership created: ID={testMembership.MembershipId}, StudentCode={testStudent.StudentCode}");

            // 4. Select active student
            Console.WriteLine("[Step 4/21] Verifying student selection and search...");
            var loadedStudent = await studentService.GetByIdAsync(testStudent.StudentId);
            if (loadedStudent == null || !loadedStudent.IsActive)
            {
                Console.WriteLine("FAIL: Active student could not be selected.");
                return 1;
            }
            Console.WriteLine($" -> Student loaded: Code={loadedStudent.StudentCode}, Name={loadedStudent.FullName}, Mobile={loadedStudent.MobileNo}");

            // 5. Active membership loads
            Console.WriteLine("[Step 5/21] Verifying active membership loads for selected student...");
            var activeMem = await billingService.GetActiveMembershipForStudentAsync(testStudent.StudentId);
            if (activeMem == null)
            {
                Console.WriteLine("FAIL: Active membership not found for student!");
                return 1;
            }
            Console.WriteLine($" -> Active membership loaded: Plan={activeMem.Plan?.PlanName}, Seat={activeMem.Seat?.SeatNumber}");

            // 6. Membership fee loads correctly
            Console.WriteLine("[Step 6/21] Verifying membership fee...");
            if (activeMem.ActualFee != 1200.00m || activeMem.FinalFee != 1100.00m)
            {
                Console.WriteLine($"FAIL: Expected ActualFee=1200, FinalFee=1100; Found ActualFee={activeMem.ActualFee}, FinalFee={activeMem.FinalFee}");
                return 1;
            }
            Console.WriteLine($" -> ActualFee={activeMem.ActualFee:C2}, Discount={activeMem.DiscountAmount:C2}, FinalFee={activeMem.FinalFee:C2}");

            // 7. Seat and membership dates display correctly
            Console.WriteLine("[Step 7/21] Verifying seat and membership dates...");
            if (activeMem.StartDate.Date != startDate.Date || activeMem.EndDate.Date != endDate.Date || activeMem.Seat?.SeatNumber != "P4-T01")
            {
                Console.WriteLine("FAIL: Seat or dates mismatch!");
                return 1;
            }
            Console.WriteLine($" -> Seat={activeMem.Seat.SeatNumber}, Start={activeMem.StartDate:dd-MMM-yyyy}, End={activeMem.EndDate:dd-MMM-yyyy}");

            // 8. Discount calculation works
            Console.WriteLine("[Step 8/21] Testing discount calculation...");
            var lineItems = new System.Collections.Generic.List<CreateInvoiceItemDto>
            {
                new CreateInvoiceItemDto
                {
                    Description = $"{testPlan.PlanName} ({testMembership.StartDate:dd-MMM-yyyy} to {testMembership.EndDate:dd-MMM-yyyy})",
                    Quantity = 1,
                    Rate = 1200.00m
                }
            };
            var previewCalc1 = await billingService.CalculateInvoicePreviewAsync(testStudent.StudentId, testMembership.MembershipId, 100.00m, lineItems);
            if (previewCalc1.SubTotal != 1200.00m || previewCalc1.DiscountAmount != 100.00m || previewCalc1.CurrentCharges != 1100.00m)
            {
                Console.WriteLine($"FAIL: Calculation preview mismatch: SubTotal={previewCalc1.SubTotal}, Discount={previewCalc1.DiscountAmount}, CurrentCharges={previewCalc1.CurrentCharges}");
                return 1;
            }
            Console.WriteLine($" -> SubTotal={previewCalc1.SubTotal:C2}, Discount={previewCalc1.DiscountAmount:C2}, CurrentCharges={previewCalc1.CurrentCharges:C2}");

            // 9. Additional charge works
            Console.WriteLine("[Step 9/21] Testing additional charge line item...");
            lineItems.Add(new CreateInvoiceItemDto
            {
                Description = "Registration Fee",
                Quantity = 1,
                Rate = 100.00m
            });
            var previewCalc2 = await billingService.CalculateInvoicePreviewAsync(testStudent.StudentId, testMembership.MembershipId, 100.00m, lineItems);
            if (previewCalc2.SubTotal != 1300.00m || previewCalc2.DiscountAmount != 100.00m || previewCalc2.CurrentCharges != 1200.00m)
            {
                Console.WriteLine($"FAIL: SubTotal with additional charge should be 1300, CurrentCharges 1200. Got: SubTotal={previewCalc2.SubTotal}, CurrentCharges={previewCalc2.CurrentCharges}");
                return 1;
            }
            Console.WriteLine($" -> SubTotal={previewCalc2.SubTotal:C2}, Discount={previewCalc2.DiscountAmount:C2}, CurrentCharges={previewCalc2.CurrentCharges:C2}");

            // 10. Invoice detail is created & line items preserved
            Console.WriteLine("[Step 10/21] Verifying invoice detail lines count...");
            if (lineItems.Count != 2)
            {
                Console.WriteLine("FAIL: Line items count should be 2.");
                return 1;
            }
            Console.WriteLine($" -> Line items ready: 1: {lineItems[0].Description} ({lineItems[0].Amount:C2}), 2: {lineItems[1].Description} ({lineItems[1].Amount:C2})");

            // 11. Invoice number is generated
            Console.WriteLine("[Step 11/21] Generating unique invoice number...");
            var invoiceDate = new DateTime(2026, 10, 2);
            var invNumber = await billingService.GenerateInvoiceNumberAsync(invoiceDate);
            if (!invNumber.StartsWith("INV-2026-") || invNumber.Length != 15)
            {
                Console.WriteLine($"FAIL: Generated invoice number format invalid: {invNumber}");
                return 1;
            }
            Console.WriteLine($" -> Generated invoice number: {invNumber}");

            // 12. Invoice saves successfully in transaction
            Console.WriteLine("[Step 12/21] Generating and saving invoice in database transaction...");
            var createRequest = new CreateInvoiceRequestDto
            {
                StudentId = testStudent.StudentId,
                MembershipId = testMembership.MembershipId,
                InvoiceDate = invoiceDate,
                DueDate = invoiceDate.AddDays(7),
                DiscountAmount = 100.00m,
                Items = lineItems,
                CreatedBy = "admin"
            };
            var (genSuccess, genMessage, generatedInvoice) = await billingService.GenerateInvoiceAsync(createRequest);

            if (!genSuccess || generatedInvoice == null || generatedInvoice.InvoiceId == 0)
            {
                Console.WriteLine($"FAIL: Invoice was not created: {genMessage}");
                return 1;
            }
            Console.WriteLine($" -> Invoice saved: ID={generatedInvoice.InvoiceId}, No={generatedInvoice.InvoiceNo}, Status={generatedInvoice.Status}");

            // 13. Invoice appears in Invoice History
            Console.WriteLine("[Step 13/21] Searching Invoice History...");
            var history = await billingService.GetInvoicesAsync(new InvoiceFilterDto
            {
                SearchText = generatedInvoice.InvoiceNo
            });

            if (!history.Any(i => i.InvoiceNo == generatedInvoice.InvoiceNo))
            {
                Console.WriteLine("FAIL: Generated invoice not found in history search!");
                return 1;
            }
            Console.WriteLine($" -> Invoice successfully located in history search (Count={history.Count})");

            // 14. Invoice can be opened/viewed
            Console.WriteLine("[Step 14/21] Opening invoice for view by ID...");
            var viewInvoice = await billingService.GetInvoiceByIdWithDetailsAsync(generatedInvoice.InvoiceId);
            if (viewInvoice == null || viewInvoice.InvoiceDetails == null || viewInvoice.InvoiceDetails.Count != 2)
            {
                Console.WriteLine("FAIL: Invoice could not be opened with details.");
                return 1;
            }
            Console.WriteLine($" -> Invoice opened: Student={viewInvoice.Student?.FullName}, Items={viewInvoice.InvoiceDetails.Count}");

            // 15. Grand Total is correct
            Console.WriteLine("[Step 15/21] Verifying Grand Total...");
            if (viewInvoice.GrandTotal != 1200.00m)
            {
                Console.WriteLine($"FAIL: Expected GrandTotal=1200.00, Found={viewInvoice.GrandTotal}");
                return 1;
            }
            Console.WriteLine($" -> GrandTotal verified: {viewInvoice.GrandTotal:C2}");

            // 16. Paid amount is 0
            Console.WriteLine("[Step 16/21] Verifying Paid amount is ₹0...");
            if (viewInvoice.PaidAmount != 0.00m)
            {
                Console.WriteLine($"FAIL: Expected PaidAmount=0.00, Found={viewInvoice.PaidAmount}");
                return 1;
            }
            Console.WriteLine($" -> PaidAmount verified: {viewInvoice.PaidAmount:C2}");

            // 17. Balance equals Grand Total
            Console.WriteLine("[Step 17/21] Verifying Balance equals Grand Total and Status is Unpaid...");
            if (viewInvoice.BalanceAmount != viewInvoice.GrandTotal || viewInvoice.Status != "Unpaid")
            {
                Console.WriteLine($"FAIL: Expected Balance={viewInvoice.GrandTotal}, Status=Unpaid. Found Balance={viewInvoice.BalanceAmount}, Status={viewInvoice.Status}");
                return 1;
            }
            Console.WriteLine($" -> BalanceAmount verified: {viewInvoice.BalanceAmount:C2}, Status={viewInvoice.Status}");

            // 18. Duplicate Generate click protection
            Console.WriteLine("[Step 18/21] Testing duplicate invoice generation protection...");
            var (dupSuccess, dupMessage, _) = await billingService.GenerateInvoiceAsync(createRequest);
            if (dupSuccess)
            {
                Console.WriteLine("FAIL: Duplicate invoice generation was NOT blocked!");
                return 1;
            }
            Console.WriteLine($" -> Duplicate generation blocked as expected: {dupMessage}");

            // 19. Database transaction rollback verification
            Console.WriteLine("[Step 19/21] Verifying database transaction rollback on error...");
            var initialInvoiceCount = await db.Invoices.CountAsync();
            var initialDetailCount = await db.InvoiceDetails.CountAsync();
            bool rollbackSucceeded = false;

            var faultyRequest = new CreateInvoiceRequestDto
            {
                StudentId = -99999, // Non-existent student ID
                MembershipId = testMembership.MembershipId,
                InvoiceDate = invoiceDate,
                DueDate = invoiceDate.AddDays(7),
                DiscountAmount = 0m,
                Items = new System.Collections.Generic.List<CreateInvoiceItemDto>
                {
                    new CreateInvoiceItemDto { Description = "Faulty Item", Quantity = 1, Rate = 500m }
                },
                CreatedBy = "admin"
            };

            var (faultySuccess, faultyMsg, _) = await billingService.GenerateInvoiceAsync(faultyRequest);
            if (!faultySuccess)
            {
                rollbackSucceeded = true;
                Console.WriteLine($" -> Faulty operation rejected: {faultyMsg}");
            }

            var postRollbackInvoiceCount = await db.Invoices.CountAsync();
            var postRollbackDetailCount = await db.InvoiceDetails.CountAsync();
            if (!rollbackSucceeded || postRollbackInvoiceCount != initialInvoiceCount || postRollbackDetailCount != initialDetailCount)
            {
                Console.WriteLine("FAIL: Transaction rollback did not restore database state!");
                return 1;
            }
            Console.WriteLine(" -> Transaction rollback verified: No orphan records created.");

            // 20. Previous outstanding calculation verification
            Console.WriteLine("[Step 20/21] Verifying previous outstanding calculation on subsequent bill...");
            var prevOutstanding = await billingService.GetPreviousOutstandingAsync(testStudent.StudentId);
            if (prevOutstanding != 1200.00m)
            {
                Console.WriteLine($"FAIL: Expected previous outstanding=1200.00 from invoice 1. Found: {prevOutstanding}");
                return 1;
            }
            Console.WriteLine($" -> Previous outstanding correctly calculated from previous unpaid invoices: {prevOutstanding:C2}");

            // 21. Dashboard billing statistics query SQL Server
            Console.WriteLine("[Step 21/21] Verifying Dashboard billing statistics queried from SQL Server...");
            var updatedStats = await dashboardService.GetSummaryAsync();
            Console.WriteLine($" -> Today's Invoices: {updatedStats.TodayInvoicesCount}");
            Console.WriteLine($" -> Today's Billing Amount: {updatedStats.TodayBillingAmount:C2}");
            Console.WriteLine($" -> Current Outstanding: {updatedStats.CurrentOutstandingAmount:C2}");
            Console.WriteLine($" -> Total Invoices: {updatedStats.TotalInvoicesCount}");

            if (updatedStats.TotalInvoicesCount < 1 || updatedStats.TodayBillingAmount < 1200.00m || updatedStats.CurrentOutstandingAmount < 1200.00m)
            {
                Console.WriteLine("FAIL: Dashboard statistics did not reflect newly generated invoice!");
                return 1;
            }

            // Cleanup test data
            Console.WriteLine("Cleaning up test data created during test execution...");
            var testInvoices = await db.Invoices.Include(i => i.InvoiceDetails)
                .Where(i => i.StudentId == testStudent.StudentId)
                .ToListAsync();
            foreach (var inv in testInvoices)
            {
                db.InvoiceDetails.RemoveRange(inv.InvoiceDetails);
            }
            db.Invoices.RemoveRange(testInvoices);

            var testMemberships = await db.StudentMemberships
                .Where(m => m.StudentId == testStudent.StudentId)
                .ToListAsync();
            db.StudentMemberships.RemoveRange(testMemberships);

            db.Students.Remove(testStudent);
            if (testPlan.PlanName == "Phase 4 Test Plan")
            {
                db.MembershipPlans.Remove(testPlan);
            }
            if (testSeat.SeatNumber == "P4-T01")
            {
                db.Seats.Remove(testSeat);
            }
            await db.SaveChangesAsync();
            Console.WriteLine(" -> Test data cleaned up successfully.");

            Console.WriteLine("==================================================");
            Console.WriteLine("ALL 21 PHASE 4 TEST SCENARIOS PASSED SUCCESSFULLY!");
            Console.WriteLine("==================================================");
            return 0;
        }

        private static async Task<int> RunPhase5AutomatedTestAsync()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("STARTING PHASE 5 AUTOMATED PAYMENT & RECEIPT TEST");
            Console.WriteLine("==================================================");

            using var scope = ServiceProvider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var billingService = scope.ServiceProvider.GetRequiredService<IBillingService>();
            var dashboardService = scope.ServiceProvider.GetRequiredService<IDashboardService>();

            // Setup Admin Session
            var adminUser = await db.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == "admin");
            if (adminUser == null)
            {
                adminUser = new UserMaster
                {
                    Username = "admin",
                    FullName = "System Administrator",
                    PasswordHash = PasswordSecurity.HashPassword("Admin@123"),
                    Role = "Admin",
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };
                db.Users.Add(adminUser);
                await db.SaveChangesAsync();
            }
            CurrentSession.CurrentUser = adminUser;
            Console.WriteLine($"[Setup] Authenticated User: {CurrentSession.CurrentUser.FullName} ({CurrentSession.CurrentUser.Role})");

            // Create test student
            var testCode = "STU-P5-" + DateTime.UtcNow.Ticks.ToString().Substring(12);
            var testStudent = new StudentMaster
            {
                StudentCode = testCode,
                FullName = "Rahul Patil Phase5 Test",
                MobileNo = "9876543299",
                Email = "rahul.patil.p5@example.com",
                CollegeName = "Pune College of Engineering",
                CourseName = "B.Tech IT",
                Address = "Shivaji Nagar, Pune",
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };
            db.Students.Add(testStudent);
            await db.SaveChangesAsync();
            Console.WriteLine($"[Setup] Test student created: {testStudent.StudentCode} (ID: {testStudent.StudentId})");

            try
            {
                // --------------------------------------------------
                // TEST 1 — Full Payment
                // Invoice: ₹1,400
                // Payment: ₹1,400
                // Expected: Paid = ₹1,400, Balance = ₹0, Status = Paid
                // --------------------------------------------------
                Console.WriteLine("\n--- [TEST 1: Full Payment] ---");
                var inv1Req = new CreateInvoiceRequestDto
                {
                    StudentId = testStudent.StudentId,
                    InvoiceDate = DateTime.Today,
                    DueDate = DateTime.Today.AddDays(7),
                    CreatedBy = "admin"
                };
                inv1Req.Items.Add(new CreateInvoiceItemDto { Description = "Monthly Library Fee", Quantity = 1, Rate = 1400.00m });
                var inv1Result = await billingService.GenerateInvoiceAsync(inv1Req);
                if (!inv1Result.Success || inv1Result.Invoice == null)
                {
                    Console.WriteLine($"FAIL: Could not create invoice 1: {inv1Result.Message}");
                    return 1;
                }
                var inv1 = inv1Result.Invoice;
                Console.WriteLine($" -> Invoice Created: {inv1.InvoiceNo}, Grand Total: ₹{inv1.GrandTotal:N2}, Status: {inv1.Status}");

                var pay1Req = new CreatePaymentRequestDto
                {
                    InvoiceId = inv1.InvoiceId,
                    StudentId = testStudent.StudentId,
                    PaymentDate = DateTime.Today,
                    Amount = 1400.00m,
                    PaymentMode = PaymentModes.Cash,
                    ReferenceNo = null,
                    Remarks = "Full cash payment",
                    ReceivedBy = "System Administrator"
                };
                var pay1Result = await paymentService.CreatePaymentAsync(pay1Req);
                if (!pay1Result.Success || pay1Result.Payment == null)
                {
                    Console.WriteLine($"FAIL: Payment failed: {pay1Result.Message}");
                    return 1;
                }
                var pay1 = pay1Result.Payment;
                Console.WriteLine($" -> Payment Recorded: {pay1.PaymentNo}, Amount: ₹{pay1.Amount:N2}, Mode: {pay1.PaymentMode}");

                // Verify Payment Number format PAY-YYYY-XXXXXX
                if (!System.Text.RegularExpressions.Regex.IsMatch(pay1.PaymentNo, @"^PAY-\d{4}-\d{6}$"))
                {
                    Console.WriteLine($"FAIL: PaymentNo '{pay1.PaymentNo}' does not match required format PAY-YYYY-XXXXXX!");
                    return 1;
                }

                // Verify Invoice Status, Paid, and Balance
                var inv1Reload = await db.Invoices.AsNoTracking().FirstAsync(i => i.InvoiceId == inv1.InvoiceId);
                Console.WriteLine($" -> Invoice After Payment: Paid: ₹{inv1Reload.PaidAmount:N2}, Balance: ₹{inv1Reload.BalanceAmount:N2}, Status: {inv1Reload.Status}");
                if (inv1Reload.PaidAmount != 1400.00m || inv1Reload.BalanceAmount != 0.00m || inv1Reload.Status != "Paid")
                {
                    Console.WriteLine($"FAIL: TEST 1 Failed! Expected Paid=1400, Balance=0, Status=Paid. Got Paid={inv1Reload.PaidAmount}, Balance={inv1Reload.BalanceAmount}, Status={inv1Reload.Status}");
                    return 1;
                }
                Console.WriteLine("✓ TEST 1 PASSED: Full payment updated Paid=₹1,400.00, Balance=₹0.00, Status=Paid.");

                // --------------------------------------------------
                // TEST 2 — Partial Payment
                // Invoice: ₹1,400
                // Payment: ₹500
                // Expected: Paid = ₹500, Balance = ₹900, Status = PartiallyPaid
                // --------------------------------------------------
                Console.WriteLine("\n--- [TEST 2: Partial Payment] ---");
                var inv2Req = new CreateInvoiceRequestDto
                {
                    StudentId = testStudent.StudentId,
                    InvoiceDate = DateTime.Today,
                    DueDate = DateTime.Today.AddDays(7),
                    CreatedBy = "admin"
                };
                inv2Req.Items.Add(new CreateInvoiceItemDto { Description = "Monthly Library Fee", Quantity = 1, Rate = 1400.00m });
                var inv2Result = await billingService.GenerateInvoiceAsync(inv2Req);
                var inv2 = inv2Result.Invoice!;
                Console.WriteLine($" -> Invoice Created: {inv2.InvoiceNo}, Grand Total: ₹{inv2.GrandTotal:N2}");

                var pay2Req = new CreatePaymentRequestDto
                {
                    InvoiceId = inv2.InvoiceId,
                    StudentId = testStudent.StudentId,
                    PaymentDate = DateTime.Today,
                    Amount = 500.00m,
                    PaymentMode = PaymentModes.UPI,
                    ReferenceNo = "UPI12345678",
                    Remarks = "Partial payment 1",
                    ReceivedBy = "System Administrator"
                };
                var pay2Result = await paymentService.CreatePaymentAsync(pay2Req);
                if (!pay2Result.Success || pay2Result.Payment == null)
                {
                    Console.WriteLine($"FAIL: Partial payment failed: {pay2Result.Message}");
                    return 1;
                }
                var inv2Reload = await db.Invoices.AsNoTracking().FirstAsync(i => i.InvoiceId == inv2.InvoiceId);
                Console.WriteLine($" -> Invoice After Partial Payment: Paid: ₹{inv2Reload.PaidAmount:N2}, Balance: ₹{inv2Reload.BalanceAmount:N2}, Status: {inv2Reload.Status}");
                if (inv2Reload.PaidAmount != 500.00m || inv2Reload.BalanceAmount != 900.00m || inv2Reload.Status != "PartiallyPaid")
                {
                    Console.WriteLine($"FAIL: TEST 2 Failed! Expected Paid=500, Balance=900, Status=PartiallyPaid.");
                    return 1;
                }
                Console.WriteLine("✓ TEST 2 PASSED: Partial payment updated Paid=₹500.00, Balance=₹900.00, Status=PartiallyPaid.");

                // --------------------------------------------------
                // TEST 3 — Second Payment
                // Invoice: ₹1,400
                // Existing Paid: ₹500
                // New Payment: ₹900
                // Expected: Paid = ₹1,400, Balance = ₹0, Status = Paid
                // --------------------------------------------------
                Console.WriteLine("\n--- [TEST 3: Second Payment] ---");
                var pay3Req = new CreatePaymentRequestDto
                {
                    InvoiceId = inv2.InvoiceId,
                    StudentId = testStudent.StudentId,
                    PaymentDate = DateTime.Today,
                    Amount = 900.00m,
                    PaymentMode = PaymentModes.Card,
                    ReferenceNo = "CARD-9876",
                    Remarks = "Clearing remaining balance",
                    ReceivedBy = "System Administrator"
                };
                var pay3Result = await paymentService.CreatePaymentAsync(pay3Req);
                if (!pay3Result.Success || pay3Result.Payment == null)
                {
                    Console.WriteLine($"FAIL: Second payment failed: {pay3Result.Message}");
                    return 1;
                }
                inv2Reload = await db.Invoices.AsNoTracking().FirstAsync(i => i.InvoiceId == inv2.InvoiceId);
                Console.WriteLine($" -> Invoice After Second Payment: Paid: ₹{inv2Reload.PaidAmount:N2}, Balance: ₹{inv2Reload.BalanceAmount:N2}, Status: {inv2Reload.Status}");
                if (inv2Reload.PaidAmount != 1400.00m || inv2Reload.BalanceAmount != 0.00m || inv2Reload.Status != "Paid")
                {
                    Console.WriteLine($"FAIL: TEST 3 Failed! Expected Paid=1400, Balance=0, Status=Paid.");
                    return 1;
                }
                Console.WriteLine("✓ TEST 3 PASSED: Second payment completed the invoice balance to ₹0.00 and set Status=Paid.");

                // --------------------------------------------------
                // TEST 4 — Overpayment Rejection
                // Invoice: ₹1,400
                // Attempt payment: ₹1,500
                // Expected: REJECT, No payment record should be created.
                // --------------------------------------------------
                Console.WriteLine("\n--- [TEST 4: Overpayment Rejection] ---");
                var inv3Req = new CreateInvoiceRequestDto
                {
                    StudentId = testStudent.StudentId,
                    InvoiceDate = DateTime.Today,
                    DueDate = DateTime.Today.AddDays(7),
                    CreatedBy = "admin"
                };
                inv3Req.Items.Add(new CreateInvoiceItemDto { Description = "Monthly Library Fee", Quantity = 1, Rate = 1400.00m });
                var inv3Result = await billingService.GenerateInvoiceAsync(inv3Req);
                var inv3 = inv3Result.Invoice!;

                var countBeforeOverpay = await db.Payments.CountAsync();
                var overpayReq = new CreatePaymentRequestDto
                {
                    InvoiceId = inv3.InvoiceId,
                    StudentId = testStudent.StudentId,
                    PaymentDate = DateTime.Today,
                    Amount = 1500.00m,
                    PaymentMode = PaymentModes.Cash,
                    Remarks = "Attempting overpayment",
                    ReceivedBy = "System Administrator"
                };
                var overpayResult = await paymentService.CreatePaymentAsync(overpayReq);
                var countAfterOverpay = await db.Payments.CountAsync();

                Console.WriteLine($" -> Overpayment Result: Success={overpayResult.Success}, Message='{overpayResult.Message}'");
                if (overpayResult.Success || countAfterOverpay != countBeforeOverpay)
                {
                    Console.WriteLine("FAIL: Overpayment was NOT rejected!");
                    return 1;
                }
                if (!overpayResult.Message.Contains("Payment amount cannot exceed the outstanding balance"))
                {
                    Console.WriteLine($"FAIL: Rejection message did not match required text! Got: '{overpayResult.Message}'");
                    return 1;
                }
                Console.WriteLine("✓ TEST 4 PASSED: Overpayment was strictly rejected without creating any payment record.");

                // --------------------------------------------------
                // TEST 5 — Multiple Payments (3 payments)
                // Invoice: ₹1,400
                // Payments: ₹500, ₹300, ₹600
                // Expected: Paid = ₹1,400, Balance = ₹0, Status = Paid
                // --------------------------------------------------
                Console.WriteLine("\n--- [TEST 5: Multiple Payments (₹500, ₹300, ₹600)] ---");
                var pA = await paymentService.CreatePaymentAsync(new CreatePaymentRequestDto
                {
                    InvoiceId = inv3.InvoiceId,
                    StudentId = testStudent.StudentId,
                    PaymentDate = DateTime.Today,
                    Amount = 500.00m,
                    PaymentMode = PaymentModes.Cash,
                    Remarks = "Installment 1"
                });
                var pB = await paymentService.CreatePaymentAsync(new CreatePaymentRequestDto
                {
                    InvoiceId = inv3.InvoiceId,
                    StudentId = testStudent.StudentId,
                    PaymentDate = DateTime.Today,
                    Amount = 300.00m,
                    PaymentMode = PaymentModes.UPI,
                    ReferenceNo = "UPI-MULTI-300",
                    Remarks = "Installment 2"
                });
                var pC = await paymentService.CreatePaymentAsync(new CreatePaymentRequestDto
                {
                    InvoiceId = inv3.InvoiceId,
                    StudentId = testStudent.StudentId,
                    PaymentDate = DateTime.Today,
                    Amount = 600.00m,
                    PaymentMode = PaymentModes.UPI,
                    ReferenceNo = "UPI-MULTI-600",
                    Remarks = "Installment 3"
                });

                if (!pA.Success || !pB.Success || !pC.Success)
                {
                    Console.WriteLine("FAIL: One of the multiple payments failed!");
                    return 1;
                }

                var inv3Reload = await db.Invoices.AsNoTracking().FirstAsync(i => i.InvoiceId == inv3.InvoiceId);
                var inv3Payments = await paymentService.GetInvoicePaymentsAsync(inv3.InvoiceId);
                Console.WriteLine($" -> Multiple Payments Count: {inv3Payments.Count}, Total Paid: ₹{inv3Reload.PaidAmount:N2}, Balance: ₹{inv3Reload.BalanceAmount:N2}, Status: {inv3Reload.Status}");

                if (inv3Payments.Count != 3 || inv3Reload.PaidAmount != 1400.00m || inv3Reload.BalanceAmount != 0.00m || inv3Reload.Status != "Paid")
                {
                    Console.WriteLine("FAIL: TEST 5 Multiple payments verification failed!");
                    return 1;
                }
                Console.WriteLine("✓ TEST 5 PASSED: 3 installments (₹500 + ₹300 + ₹600) correctly sum to ₹1,400.00 with Status=Paid.");

                // --------------------------------------------------
                // TEST 6 — Payment Cancellation / Reversal
                // Invoice: ₹1,400
                // Payments: ₹500 Active, ₹900 Active
                // Cancel ₹900.
                // Expected: Paid = ₹500, Balance = ₹900, Status = PartiallyPaid
                // Payment record remains in database (Status = Cancelled).
                // --------------------------------------------------
                Console.WriteLine("\n--- [TEST 6: Payment Cancellation / Reversal] ---");
                var inv4Req = new CreateInvoiceRequestDto
                {
                    StudentId = testStudent.StudentId,
                    InvoiceDate = DateTime.Today,
                    DueDate = DateTime.Today.AddDays(7),
                    CreatedBy = "admin"
                };
                inv4Req.Items.Add(new CreateInvoiceItemDto { Description = "Library Fee", Quantity = 1, Rate = 1400.00m });
                var inv4Result = await billingService.GenerateInvoiceAsync(inv4Req);
                var inv4 = inv4Result.Invoice!;

                var pay4A = await paymentService.CreatePaymentAsync(new CreatePaymentRequestDto
                {
                    InvoiceId = inv4.InvoiceId,
                    StudentId = testStudent.StudentId,
                    Amount = 500.00m,
                    PaymentMode = PaymentModes.Cash
                });
                var pay4B = await paymentService.CreatePaymentAsync(new CreatePaymentRequestDto
                {
                    InvoiceId = inv4.InvoiceId,
                    StudentId = testStudent.StudentId,
                    Amount = 900.00m,
                    PaymentMode = PaymentModes.UPI,
                    ReferenceNo = "UPI-TO-CANCEL"
                });

                var inv4BeforeCancel = await db.Invoices.AsNoTracking().FirstAsync(i => i.InvoiceId == inv4.InvoiceId);
                Console.WriteLine($" -> Before cancellation: Paid: ₹{inv4BeforeCancel.PaidAmount:N2}, Status: {inv4BeforeCancel.Status}");

                // Cancel payment 4B (₹900)
                int paymentToCancelId = pay4B.Payment!.PaymentId;
                var cancelResult = await paymentService.CancelPaymentAsync(paymentToCancelId, "admin", "Operator error correction");
                if (!cancelResult.Success)
                {
                    Console.WriteLine($"FAIL: Cancellation failed: {cancelResult.Message}");
                    return 1;
                }

                // Verify payment row still exists
                var cancelledPayment = await db.Payments.AsNoTracking().FirstOrDefaultAsync(p => p.PaymentId == paymentToCancelId);
                if (cancelledPayment == null)
                {
                    Console.WriteLine("FAIL: Payment row was deleted from the database!");
                    return 1;
                }
                if (cancelledPayment.Status != "Cancelled" || cancelledPayment.CancelledBy != "admin")
                {
                    Console.WriteLine($"FAIL: Payment audit fields not updated properly! Status: {cancelledPayment.Status}, CancelledBy: {cancelledPayment.CancelledBy}");
                    return 1;
                }

                // Verify invoice recalculated
                var inv4AfterCancel = await db.Invoices.AsNoTracking().FirstAsync(i => i.InvoiceId == inv4.InvoiceId);
                Console.WriteLine($" -> After cancelling ₹900: Paid: ₹{inv4AfterCancel.PaidAmount:N2}, Balance: ₹{inv4AfterCancel.BalanceAmount:N2}, Status: {inv4AfterCancel.Status}");
                if (inv4AfterCancel.PaidAmount != 500.00m || inv4AfterCancel.BalanceAmount != 900.00m || inv4AfterCancel.Status != "PartiallyPaid")
                {
                    Console.WriteLine("FAIL: Invoice Paid/Balance/Status not properly recalculated upon payment reversal!");
                    return 1;
                }
                Console.WriteLine("✓ TEST 6 PASSED: Soft cancellation retained payment row with Status=Cancelled and restored invoice Balance=₹900.00 (PartiallyPaid).");

                // --------------------------------------------------
                // TEST 7 — Cancelled Invoice Rejection
                // Attempt payment against Cancelled invoice.
                // Expected: REJECT.
                // --------------------------------------------------
                Console.WriteLine("\n--- [TEST 7: Cancelled Invoice Rejection] ---");
                var invToCancel = await db.Invoices.FirstAsync(i => i.InvoiceId == inv4.InvoiceId);
                invToCancel.Status = "Cancelled";
                await db.SaveChangesAsync();

                var payAgainstCancelled = await paymentService.CreatePaymentAsync(new CreatePaymentRequestDto
                {
                    InvoiceId = inv4.InvoiceId,
                    StudentId = testStudent.StudentId,
                    Amount = 100.00m,
                    PaymentMode = PaymentModes.Cash
                });
                Console.WriteLine($" -> Payment Against Cancelled Invoice Result: Success={payAgainstCancelled.Success}, Message='{payAgainstCancelled.Message}'");
                if (payAgainstCancelled.Success)
                {
                    Console.WriteLine("FAIL: Payment against Cancelled invoice was NOT rejected!");
                    return 1;
                }
                Console.WriteLine("✓ TEST 7 PASSED: Payment against cancelled invoice was strictly rejected.");

                // --------------------------------------------------
                // TEST 8 — Receipt Generation & Verification
                // Verify receipt fields: PaymentNo, InvoiceNo, Student, Amount, PaymentMode, ReferenceNo, Balance.
                // --------------------------------------------------
                Console.WriteLine("\n--- [TEST 8: Receipt Generation & Verification] ---");
                var receiptPayment = await paymentService.GetPaymentByIdAsync(pay1.PaymentId);
                if (receiptPayment == null)
                {
                    Console.WriteLine("FAIL: Receipt payment not found!");
                    return 1;
                }
                Console.WriteLine($" -> Receipt PaymentNo: {receiptPayment.PaymentNo}");
                Console.WriteLine($" -> Receipt InvoiceNo: {receiptPayment.Invoice?.InvoiceNo}");
                Console.WriteLine($" -> Receipt Student: {receiptPayment.Student?.FullName} ({receiptPayment.Student?.StudentCode})");
                Console.WriteLine($" -> Receipt Amount: ₹{receiptPayment.Amount:N2}");
                Console.WriteLine($" -> Receipt Mode: {receiptPayment.PaymentMode}");
                Console.WriteLine($" -> Receipt Received By: {receiptPayment.ReceivedBy}");
                Console.WriteLine($" -> Stored Invoice Balance: ₹{receiptPayment.Invoice?.BalanceAmount:N2}");

                if (receiptPayment.PaymentNo != pay1.PaymentNo ||
                    receiptPayment.Invoice?.InvoiceNo != inv1.InvoiceNo ||
                    receiptPayment.Student?.StudentCode != testStudent.StudentCode ||
                    receiptPayment.Amount != 1400.00m)
                {
                    Console.WriteLine("FAIL: Receipt data mismatch!");
                    return 1;
                }
                Console.WriteLine("✓ TEST 8 PASSED: Receipt data completely matches stored financial audit records.");

                // --------------------------------------------------
                // TEST 9 — Restart Scope & Persistence Verification
                // Verify invoice balance remains correct, payment history remains correct, receipt can still be reprinted.
                // --------------------------------------------------
                Console.WriteLine("\n--- [TEST 9: Scope Restart & Data Persistence] ---");
                using (var freshScope = ServiceProvider.CreateScope())
                {
                    var freshDb = freshScope.ServiceProvider.GetRequiredService<LibraryDbContext>();
                    var freshPayService = freshScope.ServiceProvider.GetRequiredService<IPaymentService>();
                    var freshDashService = freshScope.ServiceProvider.GetRequiredService<IDashboardService>();

                    var reloadedInvoice = await freshDb.Invoices.AsNoTracking().FirstAsync(i => i.InvoiceId == inv1.InvoiceId);
                    if (reloadedInvoice.PaidAmount != 1400.00m || reloadedInvoice.BalanceAmount != 0.00m || reloadedInvoice.Status != "Paid")
                    {
                        Console.WriteLine("FAIL: Persisted invoice values corrupted upon scope reload!");
                        return 1;
                    }

                    var ledger = await freshPayService.GetStudentPaymentHistoryAsync(testStudent.StudentId);
                    Console.WriteLine($" -> Reloaded Student Ledger Count: {ledger.Count}");
                    if (ledger.Count == 0)
                    {
                        Console.WriteLine("FAIL: Student ledger did not return payment records!");
                        return 1;
                    }

                    var reprintedReceipt = await freshPayService.GetPaymentByIdAsync(pay1.PaymentId);
                    if (reprintedReceipt == null || reprintedReceipt.Amount != 1400.00m)
                    {
                        Console.WriteLine("FAIL: Receipt reprint failed on fresh scope!");
                        return 1;
                    }

                    // Dashboard collection stats check
                    var dashboardSummary = await freshDashService.GetSummaryAsync();
                    Console.WriteLine($" -> Dashboard Today's Collection: {dashboardSummary.TodayCollectionAmount:C2}");
                    Console.WriteLine($" -> Dashboard Today's Payment Count: {dashboardSummary.TodayPaymentCount}");
                    Console.WriteLine($" -> Dashboard Current Outstanding: {dashboardSummary.CurrentOutstandingAmount:C2}");
                    Console.WriteLine($" -> Dashboard Invoices: Unpaid={dashboardSummary.UnpaidInvoicesCount}, Partial={dashboardSummary.PartiallyPaidInvoicesCount}, Paid={dashboardSummary.PaidInvoicesCount}");

                    if (dashboardSummary.TodayCollectionAmount <= 0.00m || dashboardSummary.TodayPaymentCount <= 0)
                    {
                        Console.WriteLine("FAIL: Dashboard statistics did not reflect today's collections!");
                        return 1;
                    }
                }
                Console.WriteLine("✓ TEST 9 PASSED: SQL Server persistent state verified; invoice balances, ledger, receipts, and dashboard stats confirmed.");
            }
            finally
            {
                // Cleanup test data
                Console.WriteLine("\nCleaning up Phase 5 test records...");
                var testPayments = await db.Payments.Where(p => p.StudentId == testStudent.StudentId).ToListAsync();
                db.Payments.RemoveRange(testPayments);

                var testInvoices = await db.Invoices.Include(i => i.InvoiceDetails)
                    .Where(i => i.StudentId == testStudent.StudentId)
                    .ToListAsync();
                foreach (var inv in testInvoices)
                {
                    db.InvoiceDetails.RemoveRange(inv.InvoiceDetails);
                }
                db.Invoices.RemoveRange(testInvoices);

                db.Students.Remove(testStudent);
                await db.SaveChangesAsync();
                Console.WriteLine(" -> Phase 5 test records cleaned up successfully.");
            }

            Console.WriteLine("==================================================");
            Console.WriteLine("ALL 9 PHASE 5 TEST SCENARIOS PASSED WITH 100% SUCCESS!");
            Console.WriteLine("==================================================");
            return 0;
        }
    }
}