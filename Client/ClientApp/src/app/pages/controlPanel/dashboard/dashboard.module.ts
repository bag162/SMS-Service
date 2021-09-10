import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { DashboardPanelComponent } from "./dashboard.component";

const routes: Routes = [
    { path: "", component: DashboardPanelComponent }
]

@NgModule({

    imports: [
        RouterModule.forChild(routes)
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