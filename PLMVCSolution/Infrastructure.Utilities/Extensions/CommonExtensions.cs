using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.IO;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Web.Mvc;

namespace Infrastructure.Utilities.Extensions
{
    public static class CommonExtensions
    {
        /// <summary>
        /// Checks if the specified object is null.
        /// </summary>
        /// <param name="obj">THe object to check.</param>
        /// <returns>(Boolean) True if null, False if not null.</returns>
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static bool IsObjectNullOrEmpty(this object obj)
        {
            return obj.IsNull() || obj.ToString().IsEmptyString();
        }


        /// <summary>
        /// Replace the specified string with replacement string when the specified string is empty.
        /// </summary>
        /// <param name="str">The string to be checked</param>
        /// <param name="replaceStr">The string which will be used as replacement.</param>
        /// <returns>(String) The replacement string when the specified string is empty, or the specified string when not.</returns>
        public static string ReplaceIfEmpty(this string str, string replaceStr = "")
        {
            return str.IsEmptyString() ? replaceStr : str;
        }

        /// <summary>
        /// Checks if the specified string is empty.
        /// </summary>
        /// <param name="str">The string to chec.k</param>
        /// <returns>(Bool) True if empty, False if not.</returns>
        public static bool IsEmptyString(this string str)
        {
            return str == string.Empty || str == null || String.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Returns Razor view as string
        /// </summary>
        /// <param name="controller">Controller that uses the action</param>
        /// <param name="viewName">Name of the view to be converted as string</param>
        /// <param name="model">The model of the view</param>
        /// <returns>(Strng) Razor View as string</returns>
        public static string RenderRazorViewToString(this Controller controller, string viewName, object model)
        {
            try
            {
                controller.ViewData.Model = model;
                using (var sw = new StringWriter())
                {
                    var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                    var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
                    return sw.GetStringBuilder().ToString();
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        /// <summary>
        /// Returns Razor view as string
        /// </summary>
        /// <param name="controllerContext">Controller context that uses the action</param>
        /// <param name="viewName">Name of the view to be converted as string</param>
        /// <param name="model">The model of the view</param>
        /// <returns>(Strng) Razor View as string</returns>
        public static string RenderRazorViewToString(this ControllerContext controllerContext, string viewName, object model)
        {
            try
            {
                controllerContext.Controller.ViewData.Model = model;

                using (var sw = new StringWriter())
                {
                    var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                    var viewContext = new ViewContext(controllerContext, viewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                    viewResult.View.Render(viewContext, sw);
                    viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                    return sw.GetStringBuilder().ToString();
                }

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        //public static Image Resize(this Image imageToResize, Size size)
        //{
        //    return (Image)(new Bitmap(imageToResize, size));
        //}

        //public static Image Resize(this Image imageToResize, int width, int height)
        //{
        //    return Resize(imageToResize, new Size(width, height));
        //}

        public static bool HasAnyValue(this object obj)
        {
            var type = obj.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var hasProperty = properties.Select(x => x.GetValue(obj, null))
                                        .Any(x => !x.IsObjectNullOrEmpty());

            return hasProperty;
        }


    }
}
