using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebAPI.Models;
using MyWebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebAPI.Controllers 
{
    public class AuthController : Controller
    {
    private SignInManager<User> _signInManager;

    public AuthController(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpGet ("login")]
    public ActionResult ShowView()
    {
        return View("Views/Login.cshtml");
    }

       
        public async Task<ActionResult> Login(LoginViewModel vm)
    {
        if (ModelState.IsValid)
        {
            var signInResults = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);


            if (signInResults.Succeeded)
            {
                return RedirectToAction("Views/LogedIn.cshtml");
            }
        }
        return View("Views/LogedIn.cshtml");

    }

}
}
