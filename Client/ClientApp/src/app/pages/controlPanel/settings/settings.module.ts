import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { SettingUserService, UpdatedUserInfo } from "app/services/user/settings.service";
import { SharedComponentsModule } from "app/sharedComponents/sharedComponents.module";

import { SettingsPanelComponent } from "./settings.component"
import { ChangePasswordComponent } from "./changePassword/changePassword.component";
import { UpdatePropertiesComponent } from "./changeProperties/updateProperties.component";
import { UserInfoService } from "app/services/user/userdata.service";
const routes: Routes = [
    { path: "", component: SettingsPanelComponent }
]

@NgModule({
    imports: [
        FormsModule,
        RouterModule.forChild(routes),
        SharedComponentsModule
    ],
    declarations: [
        SettingsPanelComponent,
        UpdatePropertiesComponent,
        ChangePasswordComponent
    ],
    
    exports: [
    ],

    providers:[
        SettingUserService,
        UpdatedUserInfo,
        UserInfoService
    ]

})
export class SettingsModule { }