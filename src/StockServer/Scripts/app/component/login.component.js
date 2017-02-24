System.register(["@angular/core", "@angular/forms", "@angular/router", "../service/auth.service"], function(exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, forms_1, router_1, auth_service_1;
    var LoginComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (auth_service_1_1) {
                auth_service_1 = auth_service_1_1;
            }],
        execute: function() {
            LoginComponent = (function () {
                function LoginComponent(fb, router, authService) {
                    this.fb = fb;
                    this.router = router;
                    this.authService = authService;
                    this.loginForm = null;
                    this.loginForm = fb.group({
                        username: ["", forms_1.Validators.required],
                        password: ["", forms_1.Validators.required]
                    });
                }
                LoginComponent.prototype.performLogin = function (e) {
                    var _this = this;
                    var userName = this.loginForm.value.username;
                    var password = this.loginForm.value.password;
                    this.authService.login(userName, password).subscribe(function (response) {
                        var auth = _this.authService.getAuth();
                        _this.router.navigate([""]);
                    });
                };
                LoginComponent = __decorate([
                    core_1.Component({
                        selector: "login",
                        templateUrl: "./app/view/login.template.html"
                    }), 
                    __metadata('design:paramtypes', [forms_1.FormBuilder, router_1.Router, auth_service_1.AuthService])
                ], LoginComponent);
                return LoginComponent;
            }());
            exports_1("LoginComponent", LoginComponent);
        }
    }
});
