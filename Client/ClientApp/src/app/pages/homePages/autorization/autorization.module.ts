//pages --shared --component
import { AutorizationComponent } from "app/pages/homePages/autorization/autorizationComponent/autorization.component";
import { LoginComponent } from "../autorization/autorizationComponent/loginComponent/login.component";
import { RegistrationComponent } from "../autorization/autorizationComponent/registrationComponent/registration.component";
//modules
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { AutorizationService } from "app/Services/user/authentication.service";
import { AuthenticateGuard } from "app/services/authenticate.guardService";
import { RouterModule, Routes } from "@angular/router";
import { SharedComponentsModule } from "app/sharedComponents/sharedComponents.module";

const routes: Routes = [
    {path: "", component: AutorizationComponent}
]

@NgModule({

    imports:      [
        FormsModule,
        RouterModule.forChild(routes),
        SharedComponentsModule
    ],

    declarations: [
        AutorizationComponent,
        LoginComponent,
        RegistrationComponent
    ],

    exports: [
    ],

    providers: [
        AutorizationService,
        AuthenticateGuard
    ]
})
export class AutorizationModule { }