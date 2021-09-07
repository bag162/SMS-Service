import { Component } from '@angular/core';
import { AutorizationService } from "src/app/services/authentication.service";
import { UserInfoService } from 'src/app/services/user/data.service';
import { Router } from "@angular/router";
declare var $: any;

@Component({
    selector: 'login-page',
    templateUrl: './autorization.component.html',
    styleUrls: ['./autorization.component.css']
})
export class AutorizationComponent{
    constructor(
        private userInfoService: UserInfoService,
        private router: Router){
        if (userInfoService.authorized) {
            router.navigate(['controlpanel']);
        }
    }
    changeHideOnLogin()
    {
        $("#registrationForm").attr("hidden", true);
        $("#loginForm").attr("hidden", false);
    }
    changeHideOnRegistration()
    {
        $("#loginForm").attr("hidden", true);
        $("#registrationForm").attr("hidden", false);
    }
}