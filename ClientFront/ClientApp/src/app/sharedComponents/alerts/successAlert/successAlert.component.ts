import { Component, Input } from "@angular/core";

@Component({
    selector: 'alert-suc',
    templateUrl: 'successAlert.component.html'
})

export class SuccessAlertComponent
{
    @Input() message: string = "";
}