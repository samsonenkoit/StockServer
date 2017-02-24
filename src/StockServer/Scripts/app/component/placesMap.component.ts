import { Component, OnInit, Input } from "@angular/core";
import { PlaceService } from "../service/place.service";
import { Place } from "../model/place.model";

declare var google: any;

@Component({
    selector: "placesmap",
    templateUrl: "./app/view/placesMap.template.html"

})

export class PlacesMapComponent implements OnInit {

    title = "Stock Server";
    places: Place[];
    private map: any;

    constructor(private placeService: PlaceService) {

    }

    ngOnInit() {
        var directionsService = new google.maps.DirectionsService;
        var directionsDisplay = new google.maps.DirectionsRenderer;
        this.map = new google.maps.Map(document.getElementById('map'), {
            zoom: 7,
            center: { lat: 41.85, lng: -87.65 }
        });
        directionsDisplay.setMap(this.map);

        var placesPr = this.placeService.getByPoint(54.994254, 73.355315, 1000, 500);

        placesPr.subscribe(responseItems => {
            this.places = <Place[]>responseItems.items;
            this.setMapPlaces(this.places);
        });

    }

    private setMapPlaces(items: Place[]) {

        console.info(items);

        for (var i = 0; i < items.length; i++) {

            var place = items[i];

            var marker = new google.maps.Marker({
                position: { lat: place.geoLocation.latitude, lng: place.geoLocation.longitude },
                map: this.map,
                title: place.name
            });
        }
    }
}