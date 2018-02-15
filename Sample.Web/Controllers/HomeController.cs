using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //By using the viewstate manager we can send data to the client and have them hold them in hidden fields on the page

            //This can save us round-trips to back end services or databases

            Models.Person model = new Models.Person();
            model.Name = "Elon Musk";
            model.Age = 46;
            model.Hobbies = new List<string>() { "Cars", "Rockets", "Tunnels" };

            //Here, we're sending a complex datatype, but you can send any type to the GetEncryptedViewState method
            string valueToSendToClient = EVL.Web.Mvc.Shared.StateManager.GetEncryptedViewState(model);

            //We then send that string to the client where it can be held in a hidden field
            return View("Index", null, valueToSendToClient);     
        }

    }
}