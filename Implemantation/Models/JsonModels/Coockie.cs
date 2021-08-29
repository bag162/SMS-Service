using System.Collections.Generic;

namespace Implemantation.Models.JsonModels
{
    public class Cookie
    {
        public class CookieModel
        {
            public string domain { get; set; }
            public double expires { get; set; }
            public bool httpOnly { get; set; }
            public string name { get; set; }
            public string path { get; set; }
            public string priority { get; set; }
            public bool secure { get; set; }
            public bool session { get; set; }
            public int size { get; set; }
            public string value { get; set; }
            public string sameSite { get; set; }
        }

        public class CookieRootModel
        {
            public List<CookieModel> cookies { get; set; }
        }
    }
}
