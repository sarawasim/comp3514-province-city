using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Province_City.Models
{
    public class Province
    {
        [Key]
        public string ProvinceCode { get; set; }

        [Display(Name = "Province")]

        public string ProvinceName { get; set; }
        public Collection<City> Cities { get; set; }
    }
}