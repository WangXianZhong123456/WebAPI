using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yaxon.ErpAPI.Models
{
    /// <summary>
    /// 基础返回model
    /// </summary>
    public class BasicReturnMessage
    {
        /// <summary>
        ///-3:秘钥异常,-2:秘钥未授权,-1:秘钥过期,1:调用接口成功
        /// </summary>
        public int type { get; set; }
        public string message { get; set; }
        public string value { get; set; }
    }
}