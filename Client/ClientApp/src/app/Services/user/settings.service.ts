import { Injectable } from "@angular/core";
import { MessageResponse } from "app/Infrastructure/Models/Response/message.model";
@Injectable()
export class SettingUserService {
    private readonly changePasswordUri: string = "/settings/setting/ChangePassword";
    private readonly updateUserInfoUri: string = "/settings/setting/UpdateUserInfo";

    public changePassword(oldpassword: string, newpassword: string): MessageResponse {
        let returnedResponse: MessageResponse;
        $.ajax({
            type: "POST",
            url: this.changePasswordUri,
            data: JSON.stringify({ oldpassword, newpassword }),
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8"
        })
            .done(function (successDate) {
                returnedResponse = successDate;
            })
        return returnedResponse;
    };

    public updateUserInfo(userInfo: UpdatedUserInfo): MessageResponse {
        let returnedResponse: MessageResponse;
        $.ajax({
            type: "POST",
            url: this.updateUserInfoUri,
            data: JSON.stringify(userInfo),
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8"
        })
            .done(function (successDate) {
                returnedResponse = successDate;
            })
        return returnedResponse;
    };
}


@Injectable()
export class UpdatedUserInfo {
    public EmailAddress: string;
    public Telegram: string;
    public Username: string;

}