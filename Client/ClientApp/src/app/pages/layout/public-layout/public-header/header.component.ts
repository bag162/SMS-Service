import { Component, Input } from '@angular/core';


@Component({
    selector: 'public-header',
    templateUrl: `header.component.html`,
    styleUrls: ['header.component.css']
})

@Input()
export class HeaderComponent {
}
