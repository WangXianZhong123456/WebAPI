using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Security;
using Yaxon.ErpAPI.Common;
using Yaxon.ErpAPI.Models;

namespace Yaxon.ErpAPI.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]
        public JsonResult<BasicReturnMessage> Login(string userName, string password)
        {
            //实际场景应该到数据库进行校验
            if (userName != ConfigurationUtil.UserName || password != ConfigurationUtil.PassWord)
            {
                return Json(JsonHandler.CreateMessage(0, "用户名或密码错误"));
            }
            FormsAuthenticationTicket token = new FormsAuthenticationTicket(0, userName, DateTime.Now,
                            DateTime.Now.AddHours(1), true, string.Format("{0}&{1}", userName, password),
                            FormsAuthentication.FormsCookiePath);
            //返回登录结果、用户信息、用户验证票据信息
            var Token = FormsAuthentication.Encrypt(token);
            //将身份信息保存在session中，验证当前请求是否是有效请求
            HttpContext.Current.Session[userName] = Token;

            return Json(JsonHandler.CreateMessage(1, Token));
        }
    }
}
