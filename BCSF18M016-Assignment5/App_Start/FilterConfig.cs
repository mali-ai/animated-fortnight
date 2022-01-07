using System.Web;
using System.Web.Mvc;

namespace BCSF18M016_Assignment5
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
