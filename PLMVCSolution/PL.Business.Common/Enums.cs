using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PL.Business.Common
{
    public class Enums
    {
        public enum OrderType
        {
            SalesOrder,
            PurchaseOrder,
            Correction,
            Transfer
        }

        public enum OrderTypeAbbrev
        {
            SO,
            PO,
            CR,
            T
        }
    }
}
