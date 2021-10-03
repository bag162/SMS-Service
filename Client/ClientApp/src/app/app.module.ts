import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { AppComponent }   from './app.component';
import { LayoutModule } from "./pages/layout/layoutModule";
import { NotFoundComponent } from "../../src/app/pages/notFound/notfound.component";
import { RoutingModule } from "./routing.module";
import { AuthenticateGuard } from './services/authenticate.guardService';
import { DataHomeService, UserInfoService } from './services/user/data.service';
import { SharedComponentsModule } from "./sharedComponents/sharedComponents.module";
import { HttpClientModule } from "@angular/common/http";

@NgModule({

    imports:      [
        BrowserModule, 
        FormsModule, 
        LayoutModule,
        SharedComponentsModule,
        HttpClientModule,
        RoutingModule
    ],

    exports: [
    ],

    declarations: [ 
        AppComponent,
        NotFoundComponent
    ],

    providers: [
        UserInfoService,
        AuthenticateGuard,
        DataHomeService
    ],

    bootstrap:    [ 
        AppComponent
     ] 
})
export class AppModule { }