using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Yaxon.ErpAPI.Core;
using Yaxon.ErpAPI.Models;
using Yaxon.ErpAPI.U8API.ArrivedGoods;

namespace Yaxon.ErpAPI.Controllers
{
    /// <summary>
    /// 采购订单
    /// </summary>
    public class ArrivedGoodsController : ApiController
    {
        [HttpGet]
        [SupportFilter]
        public JsonResult<U8ReturnMessage> Save()
        {
            VoucherSave obj = new VoucherSave();
            U8ReturnMessage result = obj.Save();
            return Json(result);
        }
    }
}
