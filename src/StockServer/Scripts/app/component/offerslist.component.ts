import { Component, OnInit, Input } from "@angular/core";
import { OfferService } from "../service/offer.service";
import { Offer } from "../model/offer.model";

@Component({
    selector: "offers-list",
    templateUrl: "./app/view/offersList.template.html"
})

export class OffersListComponent implements OnInit {
    title = "OffersList";
    offers: Offer[];


    constructor(private offerService: OfferService) {

    }

    ngOnInit() {

        var offersObs = this.offerService.getByPoint(54.994254, 73.355315, 1000, 500);

        offersObs.subscribe(response => {
            this.offers = <Offer[]>response.items;
        })
    }


}