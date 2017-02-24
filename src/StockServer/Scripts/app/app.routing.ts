import { ModuleWithProviders } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

import { OffersListComponent } from "./component/offersList.component";
import { PlacesMapComponent } from "./component/placesMap.component";
import { UserInfoComponent } from "./component/userInfo.component";
import { PlaceInfoComponent } from "./component/placeInfo.component";
import { LoginComponent } from "./component/login.component";

const appRoutes: Routes = [

    {
        path: "",
        component: PlacesMapComponent
    },
    {
        path: "Places",
        redirectTo: ""
    },
    {
        path: "Offers",
        component: OffersListComponent
    },
    {
        path: "UserInfo",
        component: UserInfoComponent
    },
    {
        path: "PlaceInfo/:id",
        component: PlaceInfoComponent

    },
    {
        path: "Login",
        component: LoginComponent
    }
];

export const AppRoutingProviders: any[] = [];

export const AppRouting: ModuleWithProviders = RouterModule.forRoot(appRoutes);