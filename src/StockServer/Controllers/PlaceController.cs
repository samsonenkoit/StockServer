using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StockServer.BL.DataProvider.Interface;
using StockServer.BL.Model;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using StockServer.Models;
using Microsoft.AspNetCore.Identity;
using StockServer.Models.Common;

namespace StockServer.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = "CookiesAuth")]
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

        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User);
            var placesModel = await _placeProvider.GetForUserAsync(userId);
            var placesVm = placesModel.Select(t => _mapper.Map<PlaceViewModel>(t)).ToList();
            return View(placesVm);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlaceViewModel placeVm)
        {
            if (ModelState.IsValid)
            {
                Place pl = _mapper.Map<Place>(placeVm);
                string userId = _userManager.GetUserId(User);               

                await _placeProvider.CreateAsync(userId,pl);
                return RedirectToAction("Index", new { area = ""});
            }
            else
            {
                return View(placeVm);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var place = await _placeProvider.GetAsync(id);
            var plVm = _mapper.Map<PlaceViewModel>(place);

            return View(plVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(PlaceViewModel placeVm)
        {
            if (!ModelState.IsValid)
                return View(placeVm);

            var place = _mapper.Map<Place>(placeVm);
            await _placeProvider.UpdateAsync(place);

            return RedirectToAction("Index", new { area = "" });
        }
    }
}