import { NgModule } from '@angular/core';
import { UserApiKeyComponent } from "./apikey/apikey.component";
import { UserInfoService } from "app/Services/user/userdata.service";
import { FormsModule } from '@angular/forms';

@NgModule({
    imports: [FormsModule],
    exports: [UserApiKeyComponent],
    declarations: [UserApiKeyComponent],
    providers: [UserInfoService],
})
export class UserInfoModule { }
