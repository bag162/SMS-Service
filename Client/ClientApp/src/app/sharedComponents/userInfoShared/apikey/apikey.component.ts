import { Component } from '@angular/core';
import { UserInfoService } from 'app/services/user/userdata.service';

@Component({
    selector: 'apikey-form',
    templateUrl: 'apikey.component.html'
})

export class UserApiKeyComponent {
    constructor(public userData: UserInfoService) 
    {
        if (this.userData.user.apiKey == null) {
            this.apiKey = "not assigned";
        }else{
            this.apiKey = this.userData.user.apiKey;
        }
    }
    public apiKey: string;
}