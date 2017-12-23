import { CdkTableModule } from '@angular/cdk';
import { RouterModule } from '@angular/router';
import { ApiModule } from './../api.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../../shared/shared.module';
import { MaterialModule, MdTableModule, DateAdapter, MdDatepickerModule, MdNativeDateModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ReviewsListComponent } from './reviews-list.component';
import { ReviewsListService } from './reviews-list.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReviewDetailsDialogComponent } from './review-details-dialog/review-details-dialog.component';
import { ReviewsDetailsDialogService } from './review-details-dialog/review-details-dialog.service';
import { ReviewInformationDialogComponent } from './review-informations/review-information.component';
import { CustomDateAdapter } from "./custom-adapter-date";

@NgModule({
  imports: [
    FlexLayoutModule, MaterialModule, SharedModule, CommonModule,
    FormsModule, ApiModule,  RouterModule, MdTableModule, CdkTableModule, ReactiveFormsModule, MdDatepickerModule, MdNativeDateModule
  ],
  declarations: [ReviewsListComponent, ReviewDetailsDialogComponent, ReviewInformationDialogComponent],
  providers: [ReviewsListService, ReviewsDetailsDialogService, { provide: DateAdapter, useClass: CustomDateAdapter }],
  entryComponents: [ReviewDetailsDialogComponent, ReviewInformationDialogComponent]
})
export class ReviewsListModule { }
