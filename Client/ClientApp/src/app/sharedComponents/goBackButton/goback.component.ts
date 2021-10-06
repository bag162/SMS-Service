import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'goback-button',
    templateUrl: 'goback.component copy.html'
})

export class GoBackButtonComponent {
    @Input() link: string = "cp";

    constructor(public router: Router){}

    public redirect()
    {
        this.router.navigate([this.link]);
    }
}