import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { PublicLayoutComponent } from "./public-layout/layout.component";
import { FooterComponent } from "./public-layout/public-footer/footer.component";
import { HeaderComponent } from "./public-layout/public-header/header.component";
import { NavbarFooterComponent } from "./private-layout/footer/footer.component";
import { PrivateNavbarComponent } from "./private-layout/private-navbar.component";
import { FormsModule } from "@angular/forms";

@NgModule({

    imports:      [
        RouterModule,
        FormsModule
    ],

    declarations: [
        PublicLayoutComponent,
        HeaderComponent,
        FooterComponent,

        PrivateNavbarComponent,
        NavbarFooterComponent
    ],

    exports: [
        PublicLayoutComponent,
        PrivateNavbarComponent
    ],

    providers: [
    ]
})
export class LayoutModule { }