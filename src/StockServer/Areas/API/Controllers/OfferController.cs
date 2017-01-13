using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StockServer.BL.DataProvider.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StockServer.Models;
using StockServer.BL.Model;
using StockServer.Models.OfferViewModels;
using System.IdentityModel.Tokens.Jwt;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace StockServer.Areas.API.Controllers
{
    [Area("api")]
    [Produces("application/json")]
    [Authorize(ActiveAuthenticationSchemes = "JwtAuth")]
    public class OfferController : Controller
    {
        private readonly IOfferProvider _offerProvider;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public OfferController(IOfferProvider offerProvider, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            if (offerProvider == null) throw new ArgumentNullException(nameof(offerProvider));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));

            _offerProvider = offerProvider;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetByPoint(double lat, double lon, double radius, int limit = 1000)
        {
            var placePoints = await _offerProvider.GetOffersInAreaAsync(new Geolocation(lat, lon), radius, limit);

            return new ObjectResult(placePoints);   
        }

        [HttpPost]
        public async Task<IActionResult> Buy(BuyOfferViewModel buyViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var userId = _userManager.GetUserId(User);
            //string userId = User.Claims.First(t => t.Type == JwtRegisteredClaimNames.NameId).Value;
            var buyResult = await _offerProvider.BuyAsync(userId, buyViewModel.OfferId, buyViewModel.Amount);

            return new ObjectResult((int)buyResult);

        }

        [HttpGet]
        public async Task<IActionResult> PurchasedOffers()
        {
            var userId = _userManager.GetUserId(User);

            var purchase = await _offerProvider.GetPurchaseAsync(userId, null);

            return new ObjectResult(purchase);
        }

    }
}
