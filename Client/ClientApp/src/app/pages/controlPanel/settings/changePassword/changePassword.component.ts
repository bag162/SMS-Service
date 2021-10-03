import { SettingUserService } from "../../../../../../src/app/services/user/settings.service";
import { Component } from '@angular/core';

@Component({
    selector: 'changePassword-form',
    templateUrl: 'changePassword.component.html'
})

export class ChangePasswordComponent {
    constructor(private settingsService: SettingUserService) { }
    public newPassword: string;
    public repeatPassword: string;
    public oldPassword: string;
    public changePassword() {
        //смотреть что бы пароли были равны
        var requestResult = this.settingsService.changePassword(this.oldPassword, this.newPassword);
        
        if (requestResult.success) {
            $("#alertMessages").append(`<div class="alert alert-success mt-1 py-1" id="alertServer" role="alert">${requestResult.message}</div>`);
        }
        else {
            $("#alertMessages").append(`<div class="alert alert-danger mt-1 py-1" id="alertServer" role="alert">${requestResult.message}</div>`);
        }
        $("#alertServer").delay(5000).slideUp(300);
    };
}