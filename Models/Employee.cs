using System.ComponentModel.DataAnnotations;

namespace Net6_Middlewares.Models
{
    public class Employee
    {
        [Required(ErrorMessage ="EmpNo is Required")]
        public int EmpNo { get; set; } = 0;
        [Required(ErrorMessage = "EmpName is Required")]
        public string EmpName { get; set; } = string.Empty;
    }
}
