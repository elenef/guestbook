
import { CdkTableModule } from '@angular/cdk';
import { PageDetailsComponent } from './shared/components/page-details/page-details.component';
import { SharedModule } from './shared/shared.module';
import { MessageService } from './shared/services/message.service';
import { ConfirmationService } from './shared/services/confirmation.service';
import { FilterService } from './shared/services/filter.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MaterialModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import 'hammerjs';
import { ApiModule } from './api/api.module';
import {MdDatepickerModule, MdNativeDateModule } from '@angular/material';
import { MdSortModule, MdSidenavModule } from '@angular/material';
import { ApiService } from './api/api.service';
import { UserModule } from "./users/user-details/user.module";
import { ReviewsListModule } from "./api/reviews-list/reviews-list.module";


@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    MaterialModule,
    FlexLayoutModule,
    ReactiveFormsModule,
    AppRoutingModule,
    SharedModule,
    CdkTableModule,
    ApiModule,
    MdSortModule,
    SharedModule,
    MdSidenavModule,
    MdDatepickerModule,
    MdNativeDateModule,
    UserModule,
    ReviewsListModule

  ],
  providers: [
    MessageService, ConfirmationService, FilterService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
