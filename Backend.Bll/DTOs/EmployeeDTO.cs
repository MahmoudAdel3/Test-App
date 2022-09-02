using System.ComponentModel.DataAnnotations;

namespace Backend.Bll.DTOs
{
    public class EmployeeDTO
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
