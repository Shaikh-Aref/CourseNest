using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseNest.Models
{
    [Table("EnrollmentCart")]
    public class EnrollmentCart
    {
        public int Id { get; set; }
        [Required]
        public string UserId{ get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<EnrollmentCartDetail> EnrollmentCartDetails { get; set; }
        
    }
}
