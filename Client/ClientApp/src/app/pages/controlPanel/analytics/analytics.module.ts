import { NgModule } from '@angular/core';
import { AnalyticsComponent } from './analytics.component';
import { NotDevelopedModule } from "../../../../app/sharedComponents/noDeveloped/notDeveloped.module";
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {path: "", component: AnalyticsComponent}
]

@NgModule({
    imports: [
        NotDevelopedModule,
        RouterModule.forChild(routes)
    ],
    exports: [AnalyticsComponent],
    declarations: [AnalyticsComponent],
    providers: [],
})
export class AnalyticsModule { }
