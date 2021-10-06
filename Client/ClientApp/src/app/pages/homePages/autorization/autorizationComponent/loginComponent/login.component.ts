import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AutorizationService, UserLogin } from "app/Services/user/authentication.service";
import { UserInfoService } from 'app/Services/user/userdata.service';
import { MessageResponse } from "app/Infrastructure/Models/Response/message.model";
declare var $: any;

@Component({
    selector: 'login-form',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})

export class LoginComponent {

    constructor(
        private authService: AutorizationService,
        private userInfoService: UserInfoService,
        private router: Router) { }
    userLogin = new UserLogin;

    loginUser() {
        let resultRequest: MessageResponse = this.authService.loginUser(this.userLogin);
        if (resultRequest.success) {
            $("#submitLoginButton").after(`<div class="alert alert-success py-1 my-1" id="alert-submit" role="alert">${resultRequest.message}</div>`);
        } else {
            $("#submitLoginButton").after(`<div class="alert alert-danger py-1 my-1" id="alert-submit" role="alert">${resultRequest.message}</div>`);
        }
        $("#alert-submit").delay(5000).slideUp(300);
        if (resultRequest.success) {
            this.userInfoService.user.authorized = true;
            setTimeout(() => {
                this.router.navigate(['cp']);
            }, 3000);
        }
    }
}