import { NgModule } from "@angular/core";
import { AlertsModule } from "./alerts/alerts.module";
import { NotDeveloped } from "./noDeveloped/notDeveloped.component";
@NgModule({
    imports: [
        AlertsModule
    ],
    declarations: [
    ],
    exports: [
        AlertsModule
    ]

})
export class SharedComponentsModule { }