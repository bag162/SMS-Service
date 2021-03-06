import { Component } from '@angular/core';
import { AutorizationService, UserRegistration } from "app/Services/user/authentication.service";
import { Router } from '@angular/router';
import { UserInfoService } from 'app/Services/user/userdata.service';
import { MessageResponse } from "app/Infrastructure/Models/Response/message.model";
declare var $: any;

@Component({
    selector: 'registration-form',
    templateUrl: './registration.component.html',
    styleUrls: ['./registration.component.css'],
    providers: [AutorizationService]
})
export class RegistrationComponent {

    constructor(
        private authService: AutorizationService, 
        private router: Router,
        private userInfoService: UserInfoService) { }
    userRegistration = new UserRegistration;
    confirmPassword: string;

    registerUser() {
        let requestResult: MessageResponse = this.authService.registerNewUser(this.userRegistration);
        if (requestResult.success) {
            $("#submitRegisterButton").after(`<div class="alert alert-success" id="alert-submit" role="alert">${requestResult.message}</div>`);
        } else {
            $("#submitRegisterButton").after(`<div class="alert alert-danger py-1 my-1" id="alert-submit">${requestResult.message}</div>`);
        }
        $("#alert-submit").delay(5000).slideUp(300);
        if (requestResult.success) {
            this.userInfoService.user.authorized = true;
            setTimeout(() => {
                this.router.navigate(['cp']);
            }, 3000);
        }
    }
}

