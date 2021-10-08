import { Injectable } from "@angular/core";

@Injectable({ providedIn: 'root' })
export class UserInfoService {
    public user: UserInfoModel
    
    constructor() {
        this.user = this.GetUserInfo();;
    }

    protected GetUserInfo(): UserInfoModel {
        var userInfo: UserInfoModel = new UserInfoModel;
        $.ajax({
            type: "GET",
            url: window.location.origin + "/cp/user",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false
        })
            .done(function (Data) {
                if (Data.success == false) {
                    userInfo.authorized = false;
                }
                else {
                    userInfo.authorized = true;
                    userInfo.id = Data.data.id;
                    userInfo.login = Data.data.login;
                    userInfo.username = Data.data.username;
                    userInfo.password = Data.data.password;
                    userInfo.emailAddress = Data.data.emailAddress;
                    userInfo.telegram = Data.data.telegram;
                    userInfo.idrole = Data.data.idRole;
                }
            });
        return userInfo;
    }
}

export class UserInfoModel {
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