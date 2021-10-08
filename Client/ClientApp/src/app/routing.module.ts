import { Router, RouterModule, Routes } from "@angular/router";
import { AuthenticateGuard } from './services/authenticate.guardService';
import { NotFoundComponent } from "./pages/notFound/notfound.component";
import { PublicLayoutComponent } from "./pages/layout/public-layout/layout.component";
import { NgModule } from "@angular/core";
import { PrivateNavbarComponent } from "./pages/layout/private-layout/private-navbar.component";

const appRoutes: Routes = [
  {
    path: 'cp',
    component: PrivateNavbarComponent,
    children: [
      { path: "", loadChildren: () => import('./pages/controlPanel/dashboard/dashboard.module').then(m => m.DashboardModule), canActivate: [AuthenticateGuard] },
      { path: "main", redirectTo: "" },
      { path: "settings", loadChildren: () => import('./pages/controlPanel/settings/settings.module').then(m => m.SettingsModule), canActivate: [AuthenticateGuard] },
      { path: "finance", loadChildren: () => import('./pages/controlPanel/finance/finance.module').then(m => m.FinanceModule), canActivate: [AuthenticateGuard] },
      { path: "support", loadChildren: () => import('./pages/controlPanel/support/message.module').then(m => m.MessagesModule), canActivate: [AuthenticateGuard] },
      { path: "analytics", loadChildren: () => import('./pages/controlPanel/analytics/analytics.module').then(m => m.AnalyticsModule), canActivate: [AuthenticateGuard] },
    ]
  },
  
  {
    path: '',
    component: PublicLayoutComponent,
    children: [
      { path: "", loadChildren: () => import('./pages/homePages/home/home.module').then(m => m.HomeModule) },
      { path: "login", loadChildren: () => import('./pages/homePages/autorization/autorization.module').then(m => m.AutorizationModule) },
      { path: "**", component: NotFoundComponent }
    ]
  }
];

@NgModule({

  imports:[
    RouterModule.forRoot(appRoutes)
  ],

  exports: [
    RouterModule
  ],
})

export class RoutingModule {}