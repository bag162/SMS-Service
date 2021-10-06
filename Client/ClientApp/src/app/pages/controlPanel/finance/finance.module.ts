import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { FinancePanelComponent } from "./finance.component"
import { NotDevelopedModule } from "app/sharedComponents/noDeveloped/notDeveloped.module";

const routes: Routes = [
    { path: "", component: FinancePanelComponent }
]

@NgModule({
    imports: [
        NotDevelopedModule,
        RouterModule.forChild(routes)
    ],
    declarations: [
        FinancePanelComponent
    ],
    exports: [
        FinancePanelComponent
    ]

})
export class FinanceModule { }