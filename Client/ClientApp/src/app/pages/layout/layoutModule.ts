import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { PublicLayoutComponent } from "./public-layout/layout.component";
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
        PublicHeaderLayoutComponent,
        PublicFooterLayoutComponent
    ],

    exports: [
        PublicLayoutComponent,
    ],

    providers: [
    ]
})
export class LayoutModule { }