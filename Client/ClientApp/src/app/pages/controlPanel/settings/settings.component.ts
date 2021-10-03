import { Component} from '@angular/core'
import { SettingUserService, UpdatedUserInfo } from "../../../../../src/app/services/user/settings.service";
@Component({
    selector: 'settings-page',
    templateUrl: 'settings.component.html',
    providers: [SettingUserService, UpdatedUserInfo]
})
export class SettingsPanelComponent {
    constructor(){}
}