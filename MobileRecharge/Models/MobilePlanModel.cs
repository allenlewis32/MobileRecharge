using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MobileRecharge.Models
{
    public class MobilePlanModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Plan Name")]
        public string PlanName { get; set; }
        [Required]
        [DisplayName("Service Provider")]
        public string ServiceProvider { get; set; }
        [Required]
        [DisplayName("Validity")]
        public int MonthsOfValidity { get; set; }
        [Required]
        [DisplayName("Amount")]
        public float AmountToBePaid { get; set; }
        [Required]
        [DisplayName("Data")]
        public string DataPlanCovered { get; set; }

    }
}
