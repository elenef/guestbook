// tslint:disable-next-line:max-line-length
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard, AdminGuard } from './api/guards/index';
import { PageDetailsComponent } from "./shared/components/page-details/page-details.component";
import { UserDetailsComponent } from "./users/user-details/user-details.component";
// tslint:disable-next-line:max-line-length

const appRoutes: Routes = [
  { path: 'user', component: UserDetailsComponent },

  { path: 'user/:id', component: UserDetailsComponent},

  { path: '**', redirectTo: '' }

];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
