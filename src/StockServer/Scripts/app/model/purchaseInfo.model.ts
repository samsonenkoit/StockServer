import { Offer } from "../model/offer.model";

export class Purchase {
    constructor(
        public offerTransactionId: number,
        public amount: number,
        public offer: Offer
    ){}
}