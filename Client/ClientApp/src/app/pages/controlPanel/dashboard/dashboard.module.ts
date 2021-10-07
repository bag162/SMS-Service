import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { UserInfoModule } from "app/sharedComponents/userInfoShared/userinfo.module";

import { DashboardPanelComponent } from "./dashboard.component";

const routes: Routes = [
    { path: "", component: DashboardPanelComponent }
]

@NgModule({

    imports: [
        RouterModule.forChild(routes),
        UserInfoModule
    ],

    declarations: [
        DashboardPanelComponent
    ],
    exports: [
        DashboardPanelComponent,
        RouterModule
    ]

})
export class DashboardModule { }