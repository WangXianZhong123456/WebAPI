using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Yaxon.ErpAPI.Common;
using Yaxon.ErpAPI.Core;

namespace Yaxon.ErpAPI
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    //http://www.cnblogs.com/Leo_wl/p/5734716.html

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            LogHelper.SetConfig(new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config"));
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Filters.Add(new JsonCallbackAttribute());

        }
        protected void Application_PostAuthorizeRequest()
        {
            HttpContext.Current.SetSessionStateBehavior(System.Web.SessionState.SessionStateBehavior.Required);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            string strError;
            strError = Server.GetLastError().ToString();
            if (Context != null)
                Context.ClearError();
            LogHelper.ErrorLog("请求地址：" + Context.Request.Url + "\r\nApplicationError：" + strError, null);
        }
    }
}