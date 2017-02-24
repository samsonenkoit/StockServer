System.register(["@angular/router", "./component/offersList.component", "./component/placesMap.component", "./component/userInfo.component", "./component/placeInfo.component", "./component/login.component"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var router_1, offersList_component_1, placesMap_component_1, userInfo_component_1, placeInfo_component_1, login_component_1;
    var appRoutes, AppRoutingProviders, AppRouting;
    return {
        setters:[
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (offersList_component_1_1) {
                offersList_component_1 = offersList_component_1_1;
            },
            function (placesMap_component_1_1) {
                placesMap_component_1 = placesMap_component_1_1;
            },
            function (userInfo_component_1_1) {
                userInfo_component_1 = userInfo_component_1_1;
            },
            function (placeInfo_component_1_1) {
                placeInfo_component_1 = placeInfo_component_1_1;
            },
            function (login_component_1_1) {
                login_component_1 = login_component_1_1;
            }],
        execute: function() {
            appRoutes = [
                {
                    path: "",
                    component: placesMap_component_1.PlacesMapComponent
                },
                {
                    path: "Places",
                    redirectTo: ""
                },
                {
                    path: "Offers",
                    component: offersList_component_1.OffersListComponent
                },
                {
                    path: "UserInfo",
                    component: userInfo_component_1.UserInfoComponent
                },
                {
                    path: "PlaceInfo/:id",
                    component: placeInfo_component_1.PlaceInfoComponent
                },
                {
                    path: "Login",
                    component: login_component_1.LoginComponent
                }
            ];
            exports_1("AppRoutingProviders", AppRoutingProviders = []);
            exports_1("AppRouting", AppRouting = router_1.RouterModule.forRoot(appRoutes));
        }
    }
});
