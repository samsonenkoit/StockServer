import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";

import { AuthHttp } from "../service/auth.http";
import { UserInfoAggregate } from "../model/userInfoAggregate.model";

@Injectable()

export class UserService {

    private baseUrl = "api/v1/account/";

    constructor(private http: Http, private authHttp: AuthHttp) {
    }

    getInfo() {
        let url = this.baseUrl + "Info";

        return this.authHttp.get(url).map(response => response.json());
    }
}