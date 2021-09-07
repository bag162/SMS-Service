import { NgModule } from "@angular/core";
import { SuccessAlertComponent } from "./successAlert/successAlert.component";
import { FailAlertComponent } from "./failAlert/failAlert.component";
@NgModule({

    declarations: [
        SuccessAlertComponent,
        FailAlertComponent
    ],
    exports: [
        SuccessAlertComponent,
        FailAlertComponent
    ]

})
export class AlertsModule { }