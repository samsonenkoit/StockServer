using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StockServer.BL.DataProvider.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using StockServer.Models;
using StockServer.BL.Model;
using StockServer.Models.Common;

namespace StockServer.Areas.API.Controllers
{
    [Area("api")]
    [Produces("application/json")]
    [Authorize(ActiveAuthenticationSchemes = "JwtAuth")]
    public class PlaceController : Controller
    {
        private readonly IPlaceProvider _placeProvider;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOfferProvider _offerProvider;

        public PlaceController(IPlaceProvider placeProvider, IOfferProvider offerProvider, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            if (placeProvider == null) throw new ArgumentNullException(nameof(placeProvider));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            if (offerProvider == null) throw new ArgumentNullException(nameof(offerProvider));

            _placeProvider = placeProvider;
            _mapper = mapper;
            _userManager = userManager;
            _offerProvider = offerProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetByPoint(double lat, double lon, double radius, int limit = 1000)
        {
            try
            {
                var placeInfos = await _placeProvider.GetShortPlaceInAreaAsync(new Geolocation(lat, lon), radius, limit).
                    ConfigureAwait(false);

                var areaItems = new AreaItemsList<PlaceInfo>()
                {
                    Latitude = lat,
                    Longitude = lon,
                    Radius = radius,
                    Items = placeInfos.ToList()
                };

                return new ObjectResult(areaItems);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public async Task<IActionResult> Info(int id)
        {
            try
            {
                var placeInfo = await _placeProvider.GetAsync(id).ConfigureAwait(false);
                var offers = await _offerProvider.GetOffersAsync(null, id);

                var aggregate = new PlaceInfoAggregate()
                {
                    Place = _mapper.Map<PlaceViewModel>(placeInfo),
                    Offers = offers.Cast<OfferInfo>().ToList()
                };

                return new ObjectResult(aggregate);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}