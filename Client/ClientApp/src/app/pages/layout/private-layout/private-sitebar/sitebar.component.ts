import { Component } from '@angular/core';
import { UserInfoService } from 'src/app/services/user/data.service';
declare var $: any;
@Component({
    selector: 'private-sitebar',
    templateUrl: `sitebar.component.html`,
    styleUrls: ['sitebar.component.css']
})


export class PrivateSitebarLayoutComponent{
    constructor(public userinfoservice: UserInfoService){}
// открытие или зактырие меню
    public menuBtnChange() {
        $(".sidebar").toggleClass("open");
        if ($(".sidebar").hasClass("open")) {
            $("#btn").removeClass("bx-menu-alt-right").addClass("bx-menu");
            $(".home-section").css("left", "250px").css("width", "calc(100% - 250px)");
        } else {
            $("#btn").removeClass("bx-menu").addClass("bx-menu-alt-right");
            $(".home-section").css("left", "78px").css("width", "calc(100% - 78px)");
        }
    }
}
