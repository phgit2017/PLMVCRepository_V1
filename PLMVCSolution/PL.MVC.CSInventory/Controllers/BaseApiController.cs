using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Http;
using System.IO;
using System.Net.Http.Headers;
using Infrastructure.Utilities.Extensions;

namespace PL.MVC.CSInventory.Controllers
{
    public class BaseApiController : ApiController
    {
        #region Public Methods

        protected IHttpActionResult Unauthorized()
        {
            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        protected IHttpActionResult Success(string message = "")
        {
            var result = new
            {
                success = true,
                message = message.ReplaceIfEmpty("Success.")
            };

            return Ok(result);
        }

        protected IHttpActionResult Error(string message = "")
        {
            var result = new
            {
                success = false,
                message = message.ReplaceIfEmpty("Error processing the request.")
            };

            return Ok(result);
        }

        protected IHttpActionResult ValidationErrors(bool isMultiple = false, string modelName = "")
        {
            string message = string.Empty;
            var errorList = new List<JsonValidationError>();
            var strError = new StringBuilder("[{");
            List<string> Keys = new List<string>();

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(s => s.Value.Errors.Count > 0)
                    .Select(s => new KeyValuePair<string, string>(s.Key, s.Value.Errors.First().ErrorMessage))
                    .ToList();

                errors.Add(new KeyValuePair<string, string>("IsValidationError", "true"));

                if (errors.Count > 0)
                {
                    var index = 0;
                    foreach (var error in errors)
                    {
                        index++;

                        var sep = Keys.Count == 0 ? string.Empty : ",";
                        var key = error.Key.ReplaceIfEmpty(string.Format("Field_{0}", index));

                        if (!modelName.IsEmptyString())
                        {
                            var replaceModelString = string.Format("{0}.", modelName);
                            key = key.Replace(replaceModelString, string.Empty);
                        }

                        strError.Append(string.Format("{0}\"{1}\":\"{2}\"", sep, RemoveFirstPart(key, isMultiple), error.Value.Replace(System.Environment.NewLine, " ").Replace(':', '_').Replace('\\', '_')));
                        Keys.Add(key);
                    }

                    strError.Append("}]");
                }

            }
            else
            {
                return Success();
            }

            return BadRequest(strError.ToString());
        }


        #endregion Public Methods

        #region Private Methods

        private string RemoveFirstPart(string str, bool willRetainIndex = false)
        {
            string result = string.Empty;

            if (!str.IsEmptyString())
            {
                var splittedStr = willRetainIndex ? str.Split('[') : str.Split('.');

                result = string.Join(".", splittedStr.Skip(1));

                if (willRetainIndex)
                {
                    result = result.Replace("]", "_");
                    result = result.Replace("_.", "|");

                    var newSplittedStr = result.Split('|');

                    result = string.Join("", newSplittedStr.Skip(1));

                    result += newSplittedStr[0];

                }

                if (result.IsEmptyString())
                {
                    result = str;
                }

            }

            return result;

        }

        #endregion Private Methods

        #region Helpers

        internal class JsonValidationError
        {
            public string Key { get; set; }
            public string Message { get; set; }
        }

        #endregion Helpers
    }
}