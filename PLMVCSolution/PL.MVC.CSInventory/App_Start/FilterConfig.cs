﻿using System.Web;
using System.Web.Mvc;

namespace PL.MVC.CSInventory
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}