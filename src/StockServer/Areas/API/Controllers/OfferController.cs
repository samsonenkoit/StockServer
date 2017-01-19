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
using StockServer.Models.Common;
using StockServer.Models.Common.Error;

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
            try
            {
                var offerPoints = await _offerProvider.GetOffersAsync(null,new Area()
                {
                    Latitude = lat,
                    Longitude = lon,
                    Radius = radius
                }, null, true, 1,limit);

                var areaItems = new AreaItemsList<Offer>()
                {
                    Latitude = lat,
                    Longitude = lon,
                    Radius = radius,
                    Items = offerPoints.ToList()
                };

                return new ObjectResult(areaItems);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Buy(BuyOfferViewModel buyViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var userId = _userManager.GetUserId(User);

                var buyResult = await _offerProvider.BuyAsync(userId, buyViewModel.OfferId, buyViewModel.Amount);

                if (buyResult == BuyOfferProcedureResult.Success)
                    return Ok();
                else
                    return BadRequest(new ErrorViewModel((int)buyResult));
                
            }
            catch
            {
                return BadRequest();
            }

        }

        /*[HttpGet]
        public async Task<IActionResult> PurchasedOffers()
        {
            try
            {
                var userId = _userManager.GetUserId(User);

                var purchase = await _offerProvider.GetPurchaseAsync(userId, null);

                return new ObjectResult(purchase);
            }
            catch
            {
                return BadRequest();
            }
        }*/

    }
}
