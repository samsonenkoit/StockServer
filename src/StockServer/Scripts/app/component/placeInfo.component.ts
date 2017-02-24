import { Component, OnInit } from "@angular/core";
import { PlaceService } from "../service/place.service";
import { Offer } from "../model/offer.model";
import { Place } from "../model/place.model";
import { ActivatedRoute } from '@angular/router';
import { PlaceInfoAggregate } from "../model/placeInfoAggregate.model";

@Component({
    selector: "place-info",
    templateUrl: "./app/view/placeInfo.template.html"
})

export class PlaceInfoComponent implements OnInit {

    place: Place;
    offers: Offer[];

    constructor(private placeService: PlaceService, private route: ActivatedRoute) {

    }

    ngOnInit() {
        var id = this.route.snapshot.params["id"];
        
        var placeInfoObs = this.placeService.info(id);

        placeInfoObs.subscribe(response => {
            var info = <PlaceInfoAggregate>response;

            this.place = info.place;
            this.offers = info.offers;
            
        });
    }
}