System.register([], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var UserInfo;
    return {
        setters:[],
        execute: function() {
            UserInfo = (function () {
                function UserInfo(id, login, pointsAmount) {
                    this.id = id;
                    this.login = login;
                    this.pointsAmount = pointsAmount;
                }
                return UserInfo;
            }());
            exports_1("UserInfo", UserInfo);
        }
    }
});
