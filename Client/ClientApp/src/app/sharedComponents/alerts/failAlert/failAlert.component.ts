import { Component, Input } from "@angular/core";

@Component({
    selector: 'alert-fail',
    templateUrl: 'failAlert.component.html'
})

export class FailAlertComponent
{
    @Input() message: string = "";
}