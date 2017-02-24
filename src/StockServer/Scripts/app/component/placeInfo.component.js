System.register(["@angular/core", "../service/place.service", '@angular/router'], function(exports_1, context_1) {
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
    var core_1, place_service_1, router_1;
    var PlaceInfoComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (place_service_1_1) {
                place_service_1 = place_service_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            }],
        execute: function() {
            PlaceInfoComponent = (function () {
                function PlaceInfoComponent(placeService, route) {
                    this.placeService = placeService;
                    this.route = route;
                }
                PlaceInfoComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    var id = this.route.snapshot.params["id"];
                    var placeInfoObs = this.placeService.info(id);
                    placeInfoObs.subscribe(function (response) {
                        var info = response;
                        _this.place = info.place;
                        _this.offers = info.offers;
                    });
                };
                PlaceInfoComponent = __decorate([
                    core_1.Component({
                        selector: "place-info",
                        templateUrl: "./app/view/placeInfo.template.html"
                    }), 
                    __metadata('design:paramtypes', [place_service_1.PlaceService, router_1.ActivatedRoute])
                ], PlaceInfoComponent);
                return PlaceInfoComponent;
            }());
            exports_1("PlaceInfoComponent", PlaceInfoComponent);
        }
    }
});
