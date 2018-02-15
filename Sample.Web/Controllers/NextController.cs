using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class NextController : Controller
    {

[HttpPost]
        public ActionResult Index(string viewState)
        {
            Models.Person model;

            try
            {
                //Any attempt to modify the string will result in the value failing to be decrypted
                model = EVL.Web.Mvc.Shared.StateManager.DecryptFromViewState<Models.Person>(viewState);
            }
            catch (CryptographicException)
            {
                throw new Exception("Uh Oh! Looks like the client modified the secret!");
            }
            catch (Exception)
            {
                throw new Exception("Uh Oh! Something else went wrong!");
            }

            return View("Index", null, model);
        }
    }
}