import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { UserInfoService } from "./user/data.service";

@Injectable()
export class AuthenticateGuard implements CanActivate{
    constructor(
        protected router: Router, 
        private userInfoService: UserInfoService) {
    }

    public canActivate() : boolean
    {
        return this.userInfoService.authorized;
    }
}
