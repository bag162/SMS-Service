import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class DashboardInfoService {
    public DashboardInfo: DashboardInfoModel
    
    constructor() {
        this.DashboardInfo = this.GetDashBoardInfo();
    }

    protected GetDashBoardInfo() : DashboardInfoModel
    {
        var dashboardInfo: DashboardInfoModel = new DashboardInfoModel;
        $.ajax({
            type: "GET",
            url: window.location.origin + "/cp/dashboard",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            async: false
        })
            .done(function (Data) {
                if (Data.success == false) {
                    dashboardInfo = null;
                }
                else {
                    dashboardInfo.BalancerAddress = Data.balancerAddress
                }
            });
        return dashboardInfo;
    }
}

export class DashboardInfoModel {
    constructor() { }
    public BalancerAddress: string
}