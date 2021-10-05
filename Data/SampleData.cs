using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Province_City.Models;

namespace Province_City.Data
{
    public class SampleData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.EnsureCreated();

                // Look for any Provinces.
                if (context.Provinces.Any())
                {
                    return;   // DB has already been seeded
                }

                var Provinces = GetProvinces().ToArray();
                context.Provinces.AddRange(Provinces);
                context.SaveChanges();

                var Cities = GetCities(context).ToArray();
                context.Cities.AddRange(Cities);
                context.SaveChanges();
            }
        }

        public static List<Province> GetProvinces()
        {
            List<Province> Provinces = new List<Province>() {
            new Province() {
                ProvinceName="British Columbia",
                ProvinceCode="BC",
            },
            new Province() {
                ProvinceName="Alberta",
                ProvinceCode="AB",
            },
            new Province() {
                ProvinceName="Saskatchewan",
                ProvinceCode="SK",
            }
        };

            return Provinces;
        }

        public static List<City> GetCities(ApplicationDbContext context)
        {
            List<City> Cities = new List<City>() {
            new City {
                CityName = "Vancouver",
                Population = 10000,
                ProvinceCode = context.Provinces.Find("BC").ProvinceCode,
            },
            new City {
                CityName = "Edmonton",
                Population = 5000,
                ProvinceCode = context.Provinces.Find("AB").ProvinceCode,
            },
            new City {
                CityName = "Saskatoon",
                Population = 100,
                ProvinceCode = context.Provinces.Find("SK").ProvinceCode,
            }
        };

            return Cities;
        }
    }
}