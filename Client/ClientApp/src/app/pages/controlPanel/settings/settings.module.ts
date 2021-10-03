import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { SettingUserService } from "../../../../app/services/user/settings.service";
import { SharedComponentsModule } from "../../../../../src/app/sharedComponents/sharedComponents.module";

import { SettingsPanelComponent } from "./settings.component"
import { ChangePasswordComponent } from "./changePassword/changePassword.component";
import { UpdatePropertiesComponent } from "./changeProperties/updateProperties.component";
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
        SettingUserService
    ]

})
export class SettingsModule { }