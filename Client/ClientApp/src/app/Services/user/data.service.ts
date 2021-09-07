import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root',
  })
export class DataHomeService {
    constructor(userInfo: UserInfoService) 
    {
        this.userInfo = userInfo;
    }
    
    public userInfo: UserInfoService;
}

@Injectable({
    providedIn: 'root',
  })
export class UserInfoService {

    public id: number
    public login: string
    public username: string
    public password: string
    public emailAddress: string
    public apiKey: string
    public telegram: string
    public authorized: boolean
    public idrole: string

    constructor() {
        var result = this.GetUserInfo();
        if (result.success == false) 
        {
            this.authorized = false;
        }
        else
        {
            this.authorized = true;
            this.id = result.data.id;
            this.login = result.data.login;
            this.username = result.data.username;
            this.password = result.data.password;
            this.emailAddress = result.data.emailAddress;
            this.telegram = result.data.telegram;
            this.idrole = result.data.idRole;
        }
    }

    public GetUserInfo() {
        var result;
        $.ajax({
            type: "GET",
            url: window.location.origin + "/controlPanel/Home/getUserInfo",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false
        })
            .done(function (successDate) {
                result = successDate;
            });
        return result;
    }
}

export class UserGlobalInfo{
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