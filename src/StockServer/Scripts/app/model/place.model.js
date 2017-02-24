System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Place;
    return {
        setters:[],
        execute: function() {
            Place = (function () {
                function Place(id, name, geoLocation) {
                    this.id = id;
                    this.name = name;
                    this.geoLocation = geoLocation;
                }
                return Place;
            }());
            exports_1("Place", Place);
        }
    }
});
