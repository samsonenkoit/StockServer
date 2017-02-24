System.register(["@angular/core", "../service/offer.service"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, offer_service_1;
    var OffersListComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (offer_service_1_1) {
                offer_service_1 = offer_service_1_1;
            }],
        execute: function() {
            OffersListComponent = (function () {
                function OffersListComponent(offerService) {
                    this.offerService = offerService;
                    this.title = "OffersList";
                }
                OffersListComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    var offersObs = this.offerService.getByPoint(54.994254, 73.355315, 1000, 500);
                    offersObs.subscribe(function (response) {
                        _this.offers = response.items;
                    });
                };
                OffersListComponent = __decorate([
                    core_1.Component({
                        selector: "offers-list",
                        templateUrl: "./app/view/offersList.template.html"
                    }), 
                    __metadata('design:paramtypes', [offer_service_1.OfferService])
                ], OffersListComponent);
                return OffersListComponent;
            }());
            exports_1("OffersListComponent", OffersListComponent);
        }
    }
});
