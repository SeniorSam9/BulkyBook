using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BulkyBookWeb.Models
{
    public class Category
    {
        // property is something that is in between the methods and the data fields
        // {get; set;} are used when there is no additional logic to be added in a getter or a setter method
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        public string DisplayOrder { get; set; }
        // a default value to the date
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;


    }
}
