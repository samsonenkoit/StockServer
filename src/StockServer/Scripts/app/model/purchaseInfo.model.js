System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var Purchase;
    return {
        setters:[],
        execute: function() {
            Purchase = (function () {
                function Purchase(offerTransactionId, amount, offer) {
                    this.offerTransactionId = offerTransactionId;
                    this.amount = amount;
                    this.offer = offer;
                }
                return Purchase;
            }());
            exports_1("Purchase", Purchase);
        }
    }
});
