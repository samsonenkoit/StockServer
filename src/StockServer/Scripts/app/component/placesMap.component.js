System.register(["@angular/core", "../service/place.service"], function(exports_1, context_1) {
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
    var core_1, place_service_1;
    var PlacesMapComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (place_service_1_1) {
                place_service_1 = place_service_1_1;
            }],
        execute: function() {
            PlacesMapComponent = (function () {
                function PlacesMapComponent(placeService) {
                    this.placeService = placeService;
                    this.title = "Stock Server";
                }
                PlacesMapComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    var directionsService = new google.maps.DirectionsService;
                    var directionsDisplay = new google.maps.DirectionsRenderer;
                    this.map = new google.maps.Map(document.getElementById('map'), {
                        zoom: 7,
                        center: { lat: 41.85, lng: -87.65 }
                    });
                    directionsDisplay.setMap(this.map);
                    var placesPr = this.placeService.getByPoint(54.994254, 73.355315, 1000, 500);
                    placesPr.subscribe(function (responseItems) {
                        _this.places = responseItems.items;
                        _this.setMapPlaces(_this.places);
                    });
                };
                PlacesMapComponent.prototype.setMapPlaces = function (items) {
                    console.info(items);
                    for (var i = 0; i < items.length; i++) {
                        var place = items[i];
                        var marker = new google.maps.Marker({
                            position: { lat: place.geoLocation.latitude, lng: place.geoLocation.longitude },
                            map: this.map,
                            title: place.name
                        });
                    }
                };
                PlacesMapComponent = __decorate([
                    core_1.Component({
                        selector: "placesmap",
                        templateUrl: "./app/view/placesMap.template.html"
                    }), 
                    __metadata('design:paramtypes', [place_service_1.PlaceService])
                ], PlacesMapComponent);
                return PlacesMapComponent;
            }());
            exports_1("PlacesMapComponent", PlacesMapComponent);
        }
    }
});
