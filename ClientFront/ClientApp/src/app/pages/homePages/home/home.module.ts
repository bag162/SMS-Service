import { NgModule } from "@angular/core";
import { HomeComponent } from "./homeComponent/home.component";
import { RouterModule, Routes } from "@angular/router";

const routes: Routes = [
    { path: "", component: HomeComponent }
]

@NgModule({

    imports: [
        RouterModule.forChild(routes)
    ],

    declarations: [
        HomeComponent
    ],

    exports: [
    ],

    providers: [
    ]
})
export class HomeModule { }