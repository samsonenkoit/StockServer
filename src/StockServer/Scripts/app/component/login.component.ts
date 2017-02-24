import { Component } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { AuthService } from "../service/auth.service";

@Component({
    selector: "login",
    templateUrl: "./app/view/login.template.html"
})

export class LoginComponent {
    loginForm = null;

    constructor(
        private fb: FormBuilder,
        private router: Router, private authService: AuthService) 
    {
        this.loginForm = fb.group({
            username: ["", Validators.required],
            password: ["", Validators.required]
        })
    }

    performLogin(e) {
        let userName = this.loginForm.value.username;
        let password = this.loginForm.value.password;

        this.authService.login(userName, password).subscribe(response => {
            let auth = this.authService.getAuth();
            this.router.navigate([""]);
        })
    }
}