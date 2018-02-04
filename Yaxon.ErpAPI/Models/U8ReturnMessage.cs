using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Yaxon.ErpAPI.Models
{
    public class U8ReturnMessage : BasicReturnMessage
    {
        public string curID { get; set; }

        public U8ReturnMessage() { }
        public U8ReturnMessage(int type, string message, string value, string curID)
        {
            base.type = type;
            base.message = message;
            base.value = value;
            this.curID = curID;
        }
    }
}