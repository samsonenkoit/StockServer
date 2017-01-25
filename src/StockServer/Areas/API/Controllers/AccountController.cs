using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockServer.Models.AccountViewModels;
using StockServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using StockServer.BL.DataProvider.Interface;
using StockServer.Models.Common;
using StockServer.BL.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StockServer.Areas.API.Controllers
{
    [Area("api")]
    [Produces("application/json")]
    [Authorize(ActiveAuthenticationSchemes = "JwtAuth")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
       // private readonly RoleManager<Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole> _roleManager;
        private readonly IUserProvider _userProvider;
        private readonly IOfferProvider _offerProvider;

        public AccountController(UserManager<ApplicationUser> userManager,
            IUserProvider userProvider, IOfferProvider offerProvider)
        {
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            if (userProvider == null) throw new ArgumentNullException(nameof(userProvider));
            if (offerProvider == null) throw new ArgumentNullException(nameof(offerProvider));
         //   if (roleManager == null) throw new ArgumentNullException(nameof(roleManager));

            _userManager = userManager;
            _userProvider = userProvider;
            _offerProvider = offerProvider;
           // _roleManager = roleManager;
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
                var result = await _userManager.CreateAsync(user, registerVm.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    return Ok();
                }

                AddErrors(result);
                return BadRequest(ModelState);

            }
            catch
            {
                return BadRequest();
            }

        }

        public async Task<IActionResult> Info()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var userInfo = await _userProvider.GetInfoAsync(userId);
                var purchase = await _offerProvider.GetPurchasesAsync(null, userId, null, true);

                UserInfoAggregate info = new UserInfoAggregate()
                {
                    User = userInfo,
                    Purchases = purchase.Cast<PurchaseInfo>().ToList()
                };

                return new ObjectResult(info);
            }
            catch
            {
                return BadRequest();
            }
        }

        #region Help

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("regerror", error.Description);
            }
        }

        #endregion
    }
}
