using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Yaxon.ErpAPI.Common
{
    public class ConfigurationUtil
    {
        public static readonly string UserName = ConfigurationManager.AppSettings["UserName"].ToString();
        public static readonly string PassWord = ConfigurationManager.AppSettings["PassWord"].ToString();

    }
}