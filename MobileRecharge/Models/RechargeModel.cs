using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace MobileRecharge.Models
{
    public class RechargeModel
    {
        [Key]
        public int Id { get; set; }
        public String UserId { get; set; }
        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }
        public int PlanId { get; set; }
        [ForeignKey("PlanId")]
        public MobilePlanModel MobilePlan { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
