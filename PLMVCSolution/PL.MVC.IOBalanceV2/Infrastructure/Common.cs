using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Business
using PL.Business.Common;
using PL.Business.Dto.IOBalanceV2;
using PL.Business.Interface.IOBalanceV2;

//MVC
using PL.MVC.IOBalanceV2.Areas.AdminManagement.Models;

using PL.MVC.IOBalanceV2.Infrastructure;
using Infrastructure.Utilities.Extensions;

namespace PL.MVC.IOBalanceV2.Infrastructure
{
    public static class Common
    {
        public static bool IsValidProfilePictureFileType(this HttpPostedFileBase file)
        {
            string fileType = file.ContentType;
            return fileType == "image/jpeg" || fileType == "image/pjpeg" || fileType == "image/png";
        }

        public static bool IsValidExcelFile(this HttpPostedFileBase file)
        {
            string fileType = file.ContentType;
            return fileType == "application/vnd.ms-excel" || fileType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        }
    }
}