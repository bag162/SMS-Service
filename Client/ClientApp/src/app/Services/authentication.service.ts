import { Injectable } from "@angular/core";
declare var $: any;

import { MessageResponse } from "src/app/Infrastructure/Models/Response/message.model";

@Injectable()
export class AutorizationService {
    private readonly registerUri: string = window.location.origin + "/authorization/registration/newUserRegistration";
    private readonly loginUri: string = window.location.origin + "/authorization/login/loginUser";

    public registerNewUser(user: UserRegistration): MessageResponse {
        let returnedData: MessageResponse;
        $.ajax({
            type: "POST",
            url: this.registerUri,
            data: JSON.stringify(user),
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8"
        })
            .done(function (successDate) {
                returnedData = successDate;
            });
        return returnedData;
    }

    public loginUser(user: UserLogin): MessageResponse {
        let returnedData: MessageResponse;
        $.ajax({
            type: "POST",
            url: this.loginUri,
            data: JSON.stringify(user),
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8"
        })
            .done(function (successDate) {
                returnedData = successDate;
            });
        return returnedData;
    }
}

export class UserLogin {
    login: string;
    password: string;
}

export class UserRegistration {
    login: string;
    email: string;
    password: string;
    telegram: string;
    name: string;
}