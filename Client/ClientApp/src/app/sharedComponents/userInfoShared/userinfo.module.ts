import { NgModule } from '@angular/core';
import { UserApiKeyComponent } from "./apikey/apikey.component";
import { UserInfoService } from "app/Services/user/userdata.service";
import { FormsModule } from '@angular/forms'; 
import { ServerInfoComponent } from "./server/server.component";
import { DashboardInfoService } from 'app/Services/dashboard/dashboardInfo.service';

@NgModule({
    imports: [FormsModule],
    exports: [
        UserApiKeyComponent,
        ServerInfoComponent
    ],
    declarations: [
        UserApiKeyComponent,
        ServerInfoComponent
    ],
    providers: [
        UserInfoService,
        DashboardInfoService
    ],
})
export class UserInfoModule { }