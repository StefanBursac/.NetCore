using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPI.Controllers
{
    public class MojController : Controller
    {

        [HttpGet("Logedin")]

        public ActionResult ShowPage()
        {

            return View("Views/LogedIn.cshtml");
        }

    }
}
