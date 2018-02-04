using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Security;
using Yaxon.ErpAPI.Common;
using Yaxon.ErpAPI.Models;

namespace Yaxon.ErpAPI.Core
{
    public class SupportFilter : AuthorizeAttribute
    {
        //重写基类的验证方式，加入我们自定义的Ticket验证
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string apiUri = actionContext.Request.RequestUri.AbsolutePath;
            try
            {
                //url获取token
                var content = actionContext.Request.Properties["MS_HttpContext"] as HttpContextBase;
                var token = content.Request.Headers["Token"].ToString();
                //var token = content.Request.QueryString["Token"];
                if (!string.IsNullOrEmpty(token))
                {
                    //解密用户ticket,并校验用户名密码是否匹配
                    if (ValidateTicket(token))
                    {
                        base.IsAuthorized(actionContext);
                    }
                    else
                    {
                        HandleUnauthorizedRequest(actionContext);
                        var response = actionContext.Response = actionContext.Response ?? new HttpResponseMessage();
                        response.StatusCode = HttpStatusCode.Forbidden;
                        string result = Json.Encode(new BasicReturnMessage { type = -1, message = "秘钥过期" });
                        LogHelper.ErrorLog("【" + apiUri + "】秘钥异常：" + result, null);
                        response.Content = new StringContent(result, Encoding.UTF8, "application/json");
                    }
                }
                //如果取不到身份验证信息，并且不允许匿名访问，则返回未验证401
                else
                {
                    HandleUnauthorizedRequest(actionContext);
                    var response = actionContext.Response = actionContext.Response ?? new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.Forbidden;
                    string result = Json.Encode(new BasicReturnMessage { type = -2, message = "秘钥未授权" });
                    LogHelper.ErrorLog("【" + apiUri + "】秘钥异常：" + result, null);
                    response.Content = new StringContent(result, Encoding.UTF8, "application/json");
                    /*var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().OfType<AllowAnonymousAttribute>();
                    bool isAnonymous = attributes.Any(a => a is AllowAnonymousAttribute);
                    if (isAnonymous) base.OnAuthorization(actionContext);
                    else HandleUnauthorizedRequest(actionContext);*/
                }
            }
            catch (System.Exception ex)
            {
                HandleUnauthorizedRequest(actionContext);
                var response = actionContext.Response = actionContext.Response ?? new HttpResponseMessage();
                response.StatusCode = HttpStatusCode.Forbidden;
                string result = Json.Encode(new BasicReturnMessage { type = -3, message = "" + ex.Message + "" });
                LogHelper.ErrorLog("【" + apiUri + "】秘钥异常：" + ex.Message, ex);
                response.Content = new StringContent(result, Encoding.UTF8, "application/json");
            }
        }

        //校验用户名密码（对Session匹配，或数据库数据匹配）
        private bool ValidateTicket(string encryptToken)
        {
            //解密Ticket
            var strTicket = FormsAuthentication.Decrypt(encryptToken).UserData;

            //从Ticket里面获取用户名和密码
            var index = strTicket.IndexOf("&");
            string userName = strTicket.Substring(0, index);
            string password = strTicket.Substring(index + 1);
            //取得session，不通过说明用户退出，或者session已经过期
            var token = HttpContext.Current.Session[userName];
            if (token == null)
            {
                return false;
            }
            //对比session中的令牌
            if (token.ToString() == encryptToken)
            {
                return true;
            }

            return false;

        }
    }
}