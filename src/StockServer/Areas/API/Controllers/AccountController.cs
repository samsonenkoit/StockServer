using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockServer.Models.AccountViewModels;
using StockServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StockServer.Areas.API.Controllers
{
    [Area("api")]
    [Produces("application/json")]
    [Authorize(ActiveAuthenticationSchemes = "JwtAuth")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));

            _userManager = userManager;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerVm)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = new ApplicationUser { UserName = registerVm.Email, Email = registerVm.Email };
                var result = await _userManager.CreateAsync(user);

                if (result.Succeeded)
                    return Ok();

                AddErrors(result);
                return BadRequest(ModelState);

            }
            catch
            {
                return BadRequest();
            }

        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("regerror", error.Description);
            }
        }
    }
}
