using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using StockServer.BL.DataProvider.Interface;
using StockServer.Models;
using StockServer.Models.Common;
using StockServer.BL.Model;
using StockServer.Models.OfferViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StockServer.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        private readonly IOfferProvider _offerProvider;
        private readonly IPlaceProvider _placeProvider;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public OfferController(IOfferProvider offerProvider, IPlaceProvider placeProvider,
            IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            if (offerProvider == null) throw new ArgumentNullException(nameof(offerProvider));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            if (placeProvider == null) throw new ArgumentNullException(nameof(placeProvider));

            _offerProvider = offerProvider;
            _mapper = mapper;
            _userManager = userManager;
            _placeProvider = placeProvider;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User);

            var offers = await _offerProvider.GetAllForUserAsync(userId).ConfigureAwait(false);

            var offersViewModels = offers.Select(t => _mapper.Map<OfferViewModel>(t)).ToList();

            return View(offersViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string userId = _userManager.GetUserId(User);
            var places = await _placeProvider.GetShortPlaceForUserAsync(userId);

            var createVm = new CreateEditOfferView()
            {
                Offer = new OfferViewModel(),
                Places = new SelectList(places, "Id", "Name")
            };

            return View(createVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(Prefix = "Offer")] OfferViewModel offerVm)
        {
            if (ModelState.IsValid)
            {
                Offer offer = _mapper.Map<Offer>(offerVm);
                string userId = _userManager.GetUserId(User);

                await _offerProvider.CreateAsync(offer);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(offerVm);
            }
        }
    }
}