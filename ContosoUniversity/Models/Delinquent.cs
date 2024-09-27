using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{

    public enum RecentViolation
    {
        None, violations, Expelled

    }
        

    public class Delinquent
    {
        [Key]
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public RecentViolation? RecentViolation { get; set; }

    }
}
