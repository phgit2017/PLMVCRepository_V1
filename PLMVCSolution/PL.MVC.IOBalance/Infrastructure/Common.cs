using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//Business
using PL.Business.Common;
using PL.Business.Dto.IOBalance;
using PL.Business.Interface.IOBalance;

//MVC
using PL.MVC.IOBalance.Areas.AdminManagement.Models;

using PL.MVC.IOBalance.Infrastructure;
using Infrastructure.Utilities.Extensions;
namespace PL.MVC.IOBalance.Infrastructure
{
    public static class Common
    {
        public static int? GetUserIdFromSession(this object session)
        {

            AuthenticationDto detail = null;

            if (!session.IsNull())
            {
                detail = (AuthenticationDto)session;

                if (!detail.IsNull())
                {
                    return detail.UserID;
                }
            }

            return null;
        }

        public static int? GetBranchIdFromSession(this object session)
        {

            AuthenticationDto detail = null;

            if (!session.IsNull())
            {
                detail = (AuthenticationDto)session;

                if (!detail.IsNull())
                {
                    return detail.BranchId;
                }
            }

            return null;
        }

        public static string GetUserNameFromSession(this object session)
        {

            AuthenticationDto detail = null;

            if (!session.IsNull())
            {
                detail = (AuthenticationDto)session;

                if (!detail.IsNull())
                {
                    return detail.UserName;
                }
            }

            return null;
        }

        public static string GetUserTypeFromSession(this object session)
        {
            AuthenticationDto detail = null;

            if (!session.IsNull())
            {
                detail = (AuthenticationDto)session;

                if (!detail.IsNull())
                {
                    return detail.UserTypeName;
                }
            }

            return null;
        }

        public static int? GetUserTypeIdFromSession(this object session)
        {
            AuthenticationDto detail = null;

            if (!session.IsNull())
            {
                detail = (AuthenticationDto)session;

                if (!detail.IsNull())
                {
                    return detail.UserTypeID;
                }
            }

            return null;
        }

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