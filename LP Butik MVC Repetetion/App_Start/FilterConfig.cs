using System.Web;
using System.Web.Mvc;

namespace LP_Butik_MVC_Repetetion
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
