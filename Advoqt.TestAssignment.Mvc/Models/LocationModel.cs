namespace Advoqt.TestAssignment.Mvc.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LocationModel
    {
        public int Id { get; set; }

        [Display(Name="City")]
        public string Name { get; set; }

        public string ImageUrlSuffix { get; set; }

        public string ImageDescription { get; set; }
    }
}