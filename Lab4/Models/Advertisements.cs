using Assignment02NET.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment02NET.Models
{


    public class Advertisements
    {
        [Required]
        [Key]
        public int advertisementID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "File Name")]
        public string FileName { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        public Brokerage Brokerage { get; set; }
        public string BrokerageId { get; set; }
    }
}
