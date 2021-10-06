import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { AppComponent }   from './app.component';
import { LayoutModule } from "./pages/layout/layoutModule";
import { NotFoundComponent } from "./pages/notFound/notfound.component";
import { RoutingModule } from "./routing.module";
import { AuthenticateGuard } from './services/authenticate.guardService';
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
        AuthenticateGuard
    ],

    bootstrap:    [ 
        AppComponent
     ] 
})
export class AppModule { }