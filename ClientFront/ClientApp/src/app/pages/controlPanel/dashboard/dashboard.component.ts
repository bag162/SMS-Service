import { Component} from '@angular/core';
import { DataHomeService } from "src/app/services/user/data.service";
import { Router } from "@angular/router";


@Component({
    selector: 'dashboard-page',
    templateUrl: 'dashboard.component.html',
    styleUrls: ['dashboard.component.css']
})


export class DashboardPanelComponent {
    constructor ( 
        public dataService: DataHomeService,
        public router: Router){}
        
}