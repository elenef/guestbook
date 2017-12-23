import { UserRegistrationComponent } from './user-registration/user-registration.component';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard, AdminGuard } from './api/guards/index';
import { PageDetailsComponent } from './shared/components/page-details/page-details.component';
import { UserDetailsComponent } from './users/user-details/user-details.component';
import { ReviewsListComponent } from './api/reviews-list/reviews-list.component';


const appRoutes: Routes = [
  { path: 'user', component: UserDetailsComponent },
  { path: 'user/:id', component: UserDetailsComponent},

  { path: 'reviews', component: ReviewsListComponent },
  { path: 'registration', component: UserRegistrationComponent },

  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: 'reviews' }

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
