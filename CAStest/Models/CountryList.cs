using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAStest.Models
{
    public static class Country
    {
        public static List<string> CountryList { get; set; }
       

        public static List<string> GetCountryList()
        {
            CountryList = new List<string>() {
                "Азербайджан",
                "Армения",
                "Белоруссия",
                "Грузия",
                "Казахстан",
                "Кыргызстан",
                "Молдавия",
                "Россия",
                "Таджикистан",
                "Туркмения",
                "Узбекистан",
                "Украина"
            };
            return CountryList;
        }
    }
}
