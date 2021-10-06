import { Component, OnInit } from "@angular/core";
import { UserInfoService } from "app/Services/user/data.service";
import { SettingUserService, UpdatedUserInfo } from "app/services/user/settings.service";


@Component({
    selector: 'updateProperties-form',
    templateUrl: 'updateProperties.component.html',
    providers: [SettingUserService]
})

export class UpdatePropertiesComponent {
    constructor(
        private settingsService: SettingUserService,
        public updatedUserInfo: UpdatedUserInfo,
        public userInfo: UserInfoService) {
    }

    ngOnInit() {
        this.updatedUserInfo.EmailAddress = this.userInfo.emailAddress
        this.updatedUserInfo.Telegram = this.userInfo.telegram
        this.updatedUserInfo.Username = this.userInfo.username
    }

    public updateUserInfo() {
        var resultRequest = this.settingsService.updateUserInfo(this.updatedUserInfo);
        if (resultRequest.success) {
            $("#propertyServerAlerts").append(`<div class="alert alert-success mt-1 py-1" id="alertServer" role="alert">${resultRequest.message}</div>`);
        }
        else {
            $("#propertyServerAlerts").append(`<div class="alert alert-danger mt-1 py-1" id="alertServer" role="alert">${resultRequest.message}</div>`);
        }
        $("#alertServer").delay(5000).slideUp(300);
    }

}