System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var UserInfoAggregate;
    return {
        setters:[],
        execute: function() {
            UserInfoAggregate = (function () {
                function UserInfoAggregate(user, purchase) {
                    this.user = user;
                    this.purchase = purchase;
                }
                return UserInfoAggregate;
            }());
            exports_1("UserInfoAggregate", UserInfoAggregate);
        }
    }
});
