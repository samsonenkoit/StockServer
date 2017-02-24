import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { Place } from "../model/place.model";

@Injectable()

export class OfferService {
    constructor(private http: Http) { }

    private maxReturnItems = 1000;
    private baseUrl = "api/v1/offer/";

    getByPoint(lat: number, lon: number, radius: number, limit?: number) {
        if (limit == null || limit >= this.maxReturnItems) {
            limit = this.maxReturnItems;
        }

        var url = this.baseUrl + "GetByPoint?" +
            "lat=" + lat + "&" +
            "lon=" + lon + "&" +
            "radius=" + radius + "&" +
            "limit=" + limit;

        return this.http.get(url).map(response => response.json());

    }

    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || "Server error");
    }
}


