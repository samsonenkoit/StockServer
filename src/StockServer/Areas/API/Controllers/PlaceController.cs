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

        public PlaceController(IPlaceProvider placeProvider, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            if (placeProvider == null) throw new ArgumentNullException(nameof(placeProvider));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));

            _placeProvider = placeProvider;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetByPoint(double lat, double lon, double radius, int limit = 1000)
        {
            var placePoints = await _placeProvider.GetShortPlaceInAreaAsync(new Geolocation(lat, lon), radius, limit);

            return new ObjectResult(placePoints);
        }
    }
}