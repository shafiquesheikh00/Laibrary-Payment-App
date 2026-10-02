using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Laibrary_Payment_App.Data;
using Laibrary_Payment_App.Models;

namespace Laibrary_Payment_App.Services
{
    public interface IStudentService
    {
        Task<List<StudentMaster>> GetAllStudentsAsync(string? searchTerm = null, CancellationToken cancellationToken = default);
        Task<StudentMaster?> GetByIdAsync(int studentId, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string Message, StudentMaster? Student)> AddStudentAsync(StudentMaster student, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string Message)> UpdateStudentAsync(StudentMaster student, CancellationToken cancellationToken = default);
        Task<(bool IsSuccess, string Message, bool NewStatus)> ToggleActiveStatusAsync(int studentId, CancellationToken cancellationToken = default);
        Task<bool> IsStudentCodeTakenAsync(string studentCode, int? excludeStudentId = null, CancellationToken cancellationToken = default);
        Task<bool> IsMobileNoTakenAsync(string mobileNo, int? excludeStudentId = null, CancellationToken cancellationToken = default);
        Task<string> GenerateNextStudentCodeAsync(CancellationToken cancellationToken = default);
    }

    public class StudentService : IStudentService
    {
        private readonly LibraryDbContext _dbContext;

        public StudentService(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<StudentMaster>> GetAllStudentsAsync(string? searchTerm = null, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Students.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim().ToLower();
                query = query.Where(s =>
                    s.FullName.ToLower().Contains(term) ||
                    s.StudentCode.ToLower().Contains(term) ||
                    s.MobileNo.Contains(term) ||
                    (s.CollegeName != null && s.CollegeName.ToLower().Contains(term)) ||
                    (s.CourseName != null && s.CourseName.ToLower().Contains(term)) ||
                    (s.Email != null && s.Email.ToLower().Contains(term)));
            }

            return await query
                .OrderByDescending(s => s.StudentId)
                .ToListAsync(cancellationToken);
        }

        public async Task<StudentMaster?> GetByIdAsync(int studentId, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Students
                .FirstOrDefaultAsync(s => s.StudentId == studentId, cancellationToken);
        }

        public async Task<(bool IsSuccess, string Message, StudentMaster? Student)> AddStudentAsync(StudentMaster student, CancellationToken cancellationToken = default)
        {
            var validation = await ValidateStudentAsync(student, isNew: true, cancellationToken);
            if (!validation.IsValid)
            {
                return (false, validation.Message, null);
            }

            try
            {
                student.StudentCode = student.StudentCode.Trim().ToUpperInvariant();
                student.FullName = student.FullName.Trim();
                student.MobileNo = student.MobileNo.Trim();
                student.Email = string.IsNullOrWhiteSpace(student.Email) ? null : student.Email.Trim();
                student.Address = string.IsNullOrWhiteSpace(student.Address) ? null : student.Address.Trim();
                student.CollegeName = string.IsNullOrWhiteSpace(student.CollegeName) ? null : student.CollegeName.Trim();
                student.CourseName = string.IsNullOrWhiteSpace(student.CourseName) ? null : student.CourseName.Trim();
                student.CreatedDate = DateTime.UtcNow;

                _dbContext.Students.Add(student);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return (true, $"Student '{student.FullName}' registered successfully with code {student.StudentCode}.", student);
            }
            catch (Exception ex)
            {
                return (false, $"Database error while saving student: {ex.Message}", null);
            }
        }

        public async Task<(bool IsSuccess, string Message)> UpdateStudentAsync(StudentMaster student, CancellationToken cancellationToken = default)
        {
            var existing = await _dbContext.Students
                .FirstOrDefaultAsync(s => s.StudentId == student.StudentId, cancellationToken);

            if (existing == null)
            {
                return (false, $"Student with ID {student.StudentId} was not found.");
            }

            var validation = await ValidateStudentAsync(student, isNew: false, cancellationToken);
            if (!validation.IsValid)
            {
                return (false, validation.Message);
            }

            try
            {
                existing.StudentCode = student.StudentCode.Trim().ToUpperInvariant();
                existing.FullName = student.FullName.Trim();
                existing.MobileNo = student.MobileNo.Trim();
                existing.Email = string.IsNullOrWhiteSpace(student.Email) ? null : student.Email.Trim();
                existing.Address = string.IsNullOrWhiteSpace(student.Address) ? null : student.Address.Trim();
                existing.CollegeName = string.IsNullOrWhiteSpace(student.CollegeName) ? null : student.CollegeName.Trim();
                existing.CourseName = string.IsNullOrWhiteSpace(student.CourseName) ? null : student.CourseName.Trim();
                existing.IsActive = student.IsActive;
                existing.ModifiedDate = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return (true, $"Student '{existing.FullName}' updated successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Database error while updating student: {ex.Message}");
            }
        }

        public async Task<(bool IsSuccess, string Message, bool NewStatus)> ToggleActiveStatusAsync(int studentId, CancellationToken cancellationToken = default)
        {
            var student = await _dbContext.Students
                .FirstOrDefaultAsync(s => s.StudentId == studentId, cancellationToken);

            if (student == null)
            {
                return (false, "Student was not found.", false);
            }

            try
            {
                student.IsActive = !student.IsActive;
                student.ModifiedDate = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync(cancellationToken);

                var statusText = student.IsActive ? "Activated" : "Deactivated";
                return (true, $"Student '{student.FullName}' has been {statusText}.", student.IsActive);
            }
            catch (Exception ex)
            {
                return (false, $"Database error while changing student status: {ex.Message}", student.IsActive);
            }
        }

        public async Task<bool> IsStudentCodeTakenAsync(string studentCode, int? excludeStudentId = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(studentCode)) return false;
            var code = studentCode.Trim().ToLower();

            return await _dbContext.Students
                .AnyAsync(s => s.StudentCode.ToLower() == code && (!excludeStudentId.HasValue || s.StudentId != excludeStudentId.Value), cancellationToken);
        }

        public async Task<bool> IsMobileNoTakenAsync(string mobileNo, int? excludeStudentId = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(mobileNo)) return false;
            var mobile = mobileNo.Trim();

            return await _dbContext.Students
                .AnyAsync(s => s.MobileNo == mobile && (!excludeStudentId.HasValue || s.StudentId != excludeStudentId.Value), cancellationToken);
        }

        public async Task<string> GenerateNextStudentCodeAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var total = await _dbContext.Students.CountAsync(cancellationToken);
                var code = $"STU{(total + 1):D4}";

                // Ensure uniqueness
                int attempt = 1;
                while (await IsStudentCodeTakenAsync(code, null, cancellationToken))
                {
                    code = $"STU{(total + 1 + attempt):D4}";
                    attempt++;
                }

                return code;
            }
            catch
            {
                return $"STU{new Random().Next(1000, 9999)}";
            }
        }

        private async Task<(bool IsValid, string Message)> ValidateStudentAsync(StudentMaster student, bool isNew, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(student.StudentCode))
            {
                return (false, "Student Code is required.");
            }

            if (student.StudentCode.Trim().Length < 2)
            {
                return (false, "Student Code must be at least 2 characters.");
            }

            int? excludeId = isNew ? null : student.StudentId;
            if (await IsStudentCodeTakenAsync(student.StudentCode, excludeId, cancellationToken))
            {
                return (false, $"Student Code '{student.StudentCode.Trim()}' is already assigned to another student.");
            }

            if (string.IsNullOrWhiteSpace(student.FullName))
            {
                return (false, "Student Full Name is required.");
            }

            if (student.FullName.Trim().Length < 2)
            {
                return (false, "Full Name must be at least 2 characters.");
            }

            if (string.IsNullOrWhiteSpace(student.MobileNo))
            {
                return (false, "Mobile Number is required.");
            }

            var cleanMobile = student.MobileNo.Trim();
            if (!Regex.IsMatch(cleanMobile, @"^[0-9]{10,15}$"))
            {
                return (false, "Mobile Number must contain 10 to 15 digits (numeric only).");
            }

            if (await IsMobileNoTakenAsync(cleanMobile, excludeId, cancellationToken))
            {
                return (false, $"Mobile Number '{cleanMobile}' is already registered with another student.");
            }

            if (!string.IsNullOrWhiteSpace(student.Email))
            {
                var cleanEmail = student.Email.Trim();
                if (!Regex.IsMatch(cleanEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    return (false, "Please provide a valid email address format (e.g., student@example.com).");
                }
            }

            return (true, string.Empty);
        }
    }
}
