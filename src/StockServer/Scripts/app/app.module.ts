///<reference path="../../typings/index.d.ts"/>
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { HttpModule } from "@angular/http";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";

import "rxjs/Rx";

import { AppRouting } from "./app.routing";

import { AppComponent } from "./component/app.component";
import { OffersListComponent } from "./component/offersList.component";
import { PlacesMapComponent } from "./component/placesMap.component";
import { UserInfoComponent } from "./component/userInfo.component";
import { PlaceInfoComponent } from "./component/placeInfo.component";
import { LoginComponent } from "./component/login.component";

import { Place } from "./model/place.model";
import { Offer } from "./model/offer.model";
import { PlaceInfoAggregate } from "./model/placeInfoAggregate.model";
import { UserInfoAggregate } from "./model/userInfoAggregate.model";

import { AuthHttp } from "./service/auth.http";
import { UserService } from "./service/user.service";
import { PlaceService } from "./service/place.service";
import { OfferService } from "./service/offer.service";
import { AuthService } from "./service/auth.service";

@NgModule({
    // directives, components, and pipes
    declarations: [
        OffersListComponent,
        PlacesMapComponent,
        UserInfoComponent,
        PlaceInfoComponent,
        LoginComponent
    ],
    // modules
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        RouterModule,
        AppRouting,
        ReactiveFormsModule
    ],
    // providers
    providers: [
        AuthHttp,
        PlaceService,
        OfferService,
        AuthService,
        UserService
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }