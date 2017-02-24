import { Injectable, EventEmitter } from "@angular/core";
import { Http, Headers, Response, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";

@Injectable()

export class AuthService {
    private _authKey = "auth";

    constructor(private http: Http) {

    }

    login(userName: string, password: string) {
        let url = "/token";

        let data = {
            userName: userName,
            password: password,
            grand_type: "password"
        };

        return this.http.post(
            url,
            this.toUrlEncodedString(data),
            new RequestOptions({
                headers: new Headers({
                    "Content-Type": "application/x-www-form-urlencoded"
                })
            })
        ).map(response => {
            let auth = response.json();
            console.log("Token: " + auth);
            this.setAuth(auth);
            return auth;
        });
    }

    logout(): boolean {
        this.setAuth(null);
        return true;
    }

    toUrlEncodedString(data: any) {
        let body = "";

        for (let key in data) {
            if (body.length) {
                body += "&";
            }
            body += key + "=";
            body += encodeURIComponent(data[key]);
        }

        return body;
    }


    setAuth(auth: any): boolean {
        if (auth) {
            localStorage.setItem(this._authKey, JSON.stringify(auth));
        }
        else {
            localStorage.removeItem(this._authKey);
        }

        return true;
    }

    getAuth() {
        let i = localStorage.getItem(this._authKey);

        if (i) {
            return JSON.parse(i);
        }
        else {
            return null;
        }
    }

    isLoggedIn(): boolean {
        return localStorage.getItem(this._authKey) != null;
    }


}