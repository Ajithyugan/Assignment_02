using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
namespace Assignment02NET.Models
{
    public class Client
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }



        public String FullName
        {
            get { return LastName + " " + FirstName; }
        }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
