using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yaxon.ErpAPI.Models;

namespace Yaxon.ErpAPI.Common
{
    public class JsonHandler
    {
        public static BasicReturnMessage CreateMessage(int ptype, string pmessage, string pvalue)
        {
            BasicReturnMessage json = new BasicReturnMessage()
            {
                type = ptype,
                message = pmessage,
                value = pvalue
            };
            return json;
        }
        public static BasicReturnMessage CreateMessage(int ptype, string pmessage)
        {
            BasicReturnMessage json = new BasicReturnMessage()
            {
                type = ptype,
                message = pmessage,
            };
            return json;
        }
    }
}