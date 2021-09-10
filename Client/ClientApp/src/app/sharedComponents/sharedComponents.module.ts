import { NgModule } from "@angular/core";
import { AlertsModule } from "./alerts/alerts.module";
import { NotDeveloped } from "./noDeveloped/notDeveloped.component";
import { GoBackButtonComponent } from "./goBackButton/goback.component"

@NgModule({
    imports: [
        AlertsModule,
    ],
    declarations: [
        GoBackButtonComponent
    ],
    exports: [
        AlertsModule,
        GoBackButtonComponent
    ]

})
export class SharedComponentsModule { }