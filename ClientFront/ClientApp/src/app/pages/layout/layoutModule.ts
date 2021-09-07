import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { PrivateLayoutComponent } from "./private-layout/layout.component";
import { PublicLayoutComponent } from "./public-layout/layout.component";
import { PrivateSitebarLayoutComponent } from "./private-layout/private-sitebar/sitebar.component";
import { PublicFooterLayoutComponent } from "./public-layout/public-footer/footer.component";
import { PublicHeaderLayoutComponent } from "./public-layout/public-header/header.component";
import { FormsModule } from "@angular/forms";
@NgModule({

    imports:      [
        RouterModule,
        FormsModule
    ],

    declarations: [
        PublicLayoutComponent,
        PrivateLayoutComponent,
        PrivateSitebarLayoutComponent,
        PublicHeaderLayoutComponent,
        PublicFooterLayoutComponent
    ],

    exports: [
        PublicLayoutComponent,
        PrivateLayoutComponent
    ],

    providers: [
    ]
})
export class LayoutModule { }