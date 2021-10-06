import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class UserInfoService {
    public user: UserInfo;

    constructor() {
        this.user = this.GetUserInfo();;
    }

    protected GetUserInfo(): UserInfo {
        var userInfo: UserInfo = new UserInfo;
        $.ajax({
            type: "GET",
            url: window.location.origin + "/cp/user",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false
        })
            .done(function (successDate) {
                if (successDate.success == false) {
                    userInfo.authorized = false;
                }
                else {
                    userInfo.authorized = true;
                    userInfo.id = successDate.data.id;
                    userInfo.login = successDate.data.login;
                    userInfo.username = successDate.data.username;
                    userInfo.password = successDate.data.password;
                    userInfo.emailAddress = successDate.data.emailAddress;
                    userInfo.telegram = successDate.data.telegram;
                    userInfo.idrole = successDate.data.idRole;
                }
            });
        return userInfo;
    }
}

export class UserInfo {
    public id: number
    public login: string
    public username: string
    public password: string
    public emailAddress: string
    public apiKey: string
    public telegram: string
    public authorized: boolean
    public idrole: string
}