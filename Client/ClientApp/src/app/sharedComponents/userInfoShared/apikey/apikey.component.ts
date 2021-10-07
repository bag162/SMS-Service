import { Component } from '@angular/core';
import { UserInfoService } from 'app/services/user/userdata.service';

@Component({
    selector: 'apikey-form',
    templateUrl: 'apikey.component.html',
    providers: [UserInfoService]
})

export class UserApiKeyComponent {
    constructor(public userData: UserInfoService) {}
    public apiKey: string;

    ngOnInit()
    {
        if (this.userData.user.apiKey == null) {
            this.apiKey = "not assigned";
        }else{
            this.apiKey = this.userData.user.apiKey;
        }
    }
}