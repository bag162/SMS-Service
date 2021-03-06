import { Component } from '@angular/core';
import { UserInfoService } from 'app/Services/user/userdata.service';
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
        if (userInfoService.user.authorized) {
            router.navigate(['cp']);
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