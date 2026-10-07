using Talaqah.Domain.Common;
using Talaqah.Domain.Enums;

namespace Talaqah.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public UserType? UserType { get; set; } // ( CollegeStudent, SchoolStudent, Public, null admin)
        public string NationalId { get; set; } = string.Empty;
        public string? UniversityCode { get; set; }
        public int? CollegeId { get; set; }
        public int? SchoolId { get; set; }

        // Bitmask Integer Flags
        public int PermissionMask { get; set; } = 0;
        public int? PermissionGrantedBySuperAdminId { get; set; }
        public virtual College? College { get; set; }
        public virtual School? School { get; set; }
        public virtual User? PermissionGrantedBySuperAdmin { get; set; }

        public virtual ICollection<UserExamPolicy> AssignedPolicies { get; set; } = new List<UserExamPolicy>();
        public virtual ICollection<ExamAttempt> ExamAttempts { get; set; } = new List<ExamAttempt>();
        public virtual ICollection<AdminAuditLog> AdminAuditLogs { get; set; } = new List<AdminAuditLog>();
        public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public virtual ICollection<UserExamPolicy> CreatedPolicies { get; set; }
            = new List<UserExamPolicy>();

        
    }
}
