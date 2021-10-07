import { NgModule } from "@angular/core";
import { AlertsModule } from "./alerts/alerts.module";

@NgModule({
    imports: [
        AlertsModule,
    ],
    declarations: [
    ],
    exports: [
        AlertsModule,
    ]

})
export class SharedComponentsModule { }