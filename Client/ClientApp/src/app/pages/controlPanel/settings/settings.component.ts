import { Component} from '@angular/core'
import { SettingUserService } from 'app/services/user/settings.service';

@Component({
    selector: 'settings-page',
    templateUrl: 'settings.component.html',
    providers: [SettingUserService]
})
export class SettingsPanelComponent {
}