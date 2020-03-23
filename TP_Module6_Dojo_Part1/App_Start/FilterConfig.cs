using System.Web;
using System.Web.Mvc;

namespace TP_Module6_Dojo_Part1
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
