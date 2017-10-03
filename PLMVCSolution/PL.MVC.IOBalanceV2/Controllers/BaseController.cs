using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PL.MVC.IOBalanceV2.Infrastructure;

namespace PL.MVC.IOBalanceV2.Controllers
{
    public partial class BaseController : Controller
    {
        public void Success(string message, bool dismissable = true, bool showFirstOnly = true)
        {
            AddAlert(AlertTypes.Success, message, dismissable, showFirstOnly);
        }

        public void Success(List<string> messages, bool dismissable = true, bool showFirstOnly = true)
        {
            AddAlert(AlertTypes.Success, messages, dismissable, showFirstOnly);
        }

        public void Information(string message, bool dismissable = true, bool showFirstOnly = true)
        {
            AddAlert(AlertTypes.Information, message, dismissable, showFirstOnly);
        }

        public void Information(List<string> messages, bool dismissable = true, bool showFirstOnly = true)
        {
            AddAlert(AlertTypes.Information, messages, dismissable, showFirstOnly);
        }

        public void Warning(string message, bool dismissable = true, bool showFirstOnly = true)
        {
            AddAlert(AlertTypes.Warning, message, dismissable, showFirstOnly);
        }

        public void Warning(List<string> messages, bool dismissable = true, bool showFirstOnly = true)
        {
            AddAlert(AlertTypes.Warning, messages, dismissable, showFirstOnly);
        }

        public void Danger(string message, bool dismissable = true, bool showFirstOnly = true)
        {
            AddAlert(AlertTypes.Danger, message, dismissable, showFirstOnly);
        }

        public void Danger(List<string> messages, bool dismissable = true, bool showFirstOnly = true)
        {
            AddAlert(AlertTypes.Danger, messages, dismissable, showFirstOnly);
        }

        private void AddAlert(AlertTypes alertStyle, string message, bool dismissable, bool showFirstOnly = true)
        {

            List<string> messages = new List<string>();
            messages.Add(message);

            AddAlert(alertStyle, messages, dismissable, showFirstOnly);
        }

        private void AddAlert(AlertTypes alertStyle, List<string> messages, bool dismissable, bool showFirstOnly = true)
        {
            TempData[Alert.TempDataCreateDate] = DateTime.Now;

            var alerts = TempData.ContainsKey(Alert.TempDataKey) ? (List<Alert>)TempData[Alert.TempDataKey] : new List<Alert>();

            alerts.Add(new Alert
            {
                AlertStyle = alertStyle,
                Messages = messages,
                Dismissable = dismissable
            });
            TempData[Alert.TempDataKey] = alerts;
            TempData[Alert.TempDataDisplay] = showFirstOnly;
        }
    }
}