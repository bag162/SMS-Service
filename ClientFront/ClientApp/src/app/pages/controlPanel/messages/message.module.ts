import { NgModule } from '@angular/core';
import { MessagesComponent } from './messages.component';
import { NotDevelopedModule } from 'src/app/sharedComponents/noDeveloped/notDeveloped.module';
import { Router, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    {path: "", component: MessagesComponent}
]

@NgModule({
    imports: [
        NotDevelopedModule,
        RouterModule.forChild(routes)
    ],
    exports: [
        MessagesComponent
    ],
    declarations: [
        MessagesComponent
    ],
    providers: [],
})
export class MessagesModule { }
