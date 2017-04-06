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
    [Route("Auth")]
    public class AuthController : Controller
    {
    private SignInManager<User> _signInManager;

    public AuthController(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }
    
    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View();
    }
       
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginViewModel vm)
        {
        if (ModelState.IsValid)
        {
            var signInResults = await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, true, false);


            if (signInResults.Succeeded)
            {
                    RedirectToAction("ShowPage","Moj");      //return View("Views/Auth/LogedIn.cshtml");
            }
        }
        return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }

            return RedirectToAction("Login","Auth" );
        }

    }
}
