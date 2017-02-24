System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var PlaceInfoAggregate;
    return {
        setters:[],
        execute: function() {
            PlaceInfoAggregate = (function () {
                function PlaceInfoAggregate(place, offers) {
                    this.place = place;
                    this.offers = offers;
                }
                return PlaceInfoAggregate;
            }());
            exports_1("PlaceInfoAggregate", PlaceInfoAggregate);
        }
    }
});
