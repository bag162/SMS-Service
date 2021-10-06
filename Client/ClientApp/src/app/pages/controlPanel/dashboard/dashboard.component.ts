import { Component} from '@angular/core';
import { UserInfoService } from "app/Services/user/userdata.service";
import { Router } from "@angular/router";


@Component({
    selector: 'dashboard-page',
    templateUrl: 'dashboard.component.html',
    styleUrls: ['dashboard.component.css']
})


export class DashboardPanelComponent {
    constructor ( 
        public userInfoService: UserInfoService,
        public router: Router){}
        
        public redirect(link: string)
        {
            this.router.navigate([link]);
        }
}