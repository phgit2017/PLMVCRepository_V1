using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Business.Common
{
    public static class BusinessSettings
    {
        public static string CreatePOSONumber(Enums.OrderTypeAbbrev orderTypeAbbrev)
        {
            string POSONumber = string.Empty;

            if (orderTypeAbbrev == Enums.OrderTypeAbbrev.PO)
            {
                POSONumber = string.Format("PO-{0}", System.DateTime.Now.ToString("yyyyMMdd"), 1);
            }

            if (orderTypeAbbrev == Enums.OrderTypeAbbrev.SO)
            {
                POSONumber = string.Format("SO-{0}", System.DateTime.Now.ToString("yyyyMMdd"), 1);
            }

            return POSONumber;
        }

    }
}
