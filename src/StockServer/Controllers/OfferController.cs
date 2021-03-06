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
    [Authorize(ActiveAuthenticationSchemes = "CookiesAuth", Roles = "Partner,Admin")]
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
        public async Task<IActionResult> PlaceOffers(int id)
        {
            string userId = _userManager.GetUserId(User);

            var offers = await _offerProvider.GetOffersAsync(userId, null, id, null, null, null);

            var offersVm = offers.Select(t => _mapper.Map<OfferViewModel>(t)).ToList();

            PlaceOffersViewModel plOffers = new PlaceOffersViewModel()
            {
                PlaceId = id,
                Offers = offersVm
            };

            return View(plOffers);
        }


        public async Task<IActionResult> DeliverPurchase(int offerTransactionId)
        {
            var purchase = await _offerProvider.GetPurchaseAsync(offerTransactionId);
            return View(purchase);
        }

        [HttpPost]
        public async Task<IActionResult> DeliverPurchase(Purchase purchase)
        {
            string userId = _userManager.GetUserId(User);
            await _offerProvider.DeliverPurchaseAsync(userId, purchase.OfferTransactionId);

            return RedirectToAction("PlacePurchase", new { id = purchase.Offer.PlaceId, area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> PlacePurchase(int id)
        {
            string userId = _userManager.GetUserId(User);

            var purhase = await _offerProvider.GetPurchasesAsync(userId, null, id, true);

            return View(purhase);
        }

        [HttpGet]
        public IActionResult Create(int placeId)
        {
            return View(new OfferViewModel() { Id = placeId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfferViewModel offerVm)
        {
            if (ModelState.IsValid)
            {
                Offer offer = _mapper.Map<Offer>(offerVm);
                string userId = _userManager.GetUserId(User);

                await _offerProvider.CreateAsync(offer);
                return RedirectToAction(nameof(PlaceOffers), new { id = offer.PlaceId, area = "" });
            }
            else
            {
                return View(offerVm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var offer = await _offerProvider.GetAsync(id);
            var offerVm = _mapper.Map<OfferViewModel>(offer);

            return View(offerVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(OfferViewModel offerVm)
        {
            if (!ModelState.IsValid)
                return View(offerVm);

            var offer = _mapper.Map<Offer>(offerVm);
            await _offerProvider.UpdateAsync(offer);

            return RedirectToAction(nameof(PlaceOffers), new { id = offer.PlaceId, area = "" });
        }

        [HttpGet]
        public IActionResult AddOfferItems(int offerId, int placeId)
        {
            var viewModel = new AddOfferItemsViewModel() { OfferId = offerId, PlaceId = placeId };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddOfferItems(AddOfferItemsViewModel addVm)
        {
            if (ModelState.IsValid)
            {
                string userId = _userManager.GetUserId(User);

                OfferTransaction tr = new OfferTransaction()
                {
                    CreateUserId = userId,
                    OfferId = addVm.OfferId,
                    Amount = addVm.Amount,
                    Type = OfferTransactionType.Supply
                };

                await _offerProvider.AddTransactionAsync(tr);
                
                return RedirectToAction(nameof(PlaceOffers), new { id = addVm.PlaceId, area = "" });
            }
            else
            {
                return View(addVm);
            }
        }

    }
}