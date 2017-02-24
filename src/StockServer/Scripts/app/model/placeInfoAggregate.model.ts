import { Place } from "../model/place.model";
import { Offer } from "../model/offer.model";

export class PlaceInfoAggregate {
    constructor(
        public place: Place,
        public offers: Offer[]
    ){}
}