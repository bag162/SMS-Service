import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { UserInfoService } from 'app/services/user/userdata.service';

@Component({
    selector: 'apikey-form',
    templateUrl: 'apikey.component.html'
})

export class UserApiKeyComponent {
    public apiKey: string;
    
    constructor(public userData: UserInfoService,
        public httpClient: HttpClient) 
    {
        if (this.userData.user.apiKey == null) {
            this.apiKey = "not assigned";
        }else{
            this.apiKey = this.userData.user.apiKey;
        }
    }

    public UpdateApiKey()
    {
        this.httpClient.get(window.location.origin + "cp/apikey").subscribe((data:any) => this.apiKey = data.apiKey);
    }
}