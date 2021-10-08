import { Component } from '@angular/core';
import { DashboardInfoModel, DashboardInfoService } from 'app/Services/dashboard/dashboardInfo.service';

@Component({
    selector: 'server-form',
    templateUrl: 'server.component.html'
})

export class ServerInfoComponent {
    constructor(public dashboardDataService: DashboardInfoService) 
    {
        this.ipAddress = dashboardDataService.DashboardInfo.BalancerAddress;
    }

    public ipAddress: string;
}