import { CdkTableModule } from '@angular/cdk';
import { RouterModule } from '@angular/router';
import { ApiModule } from './../api.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from './../../shared/shared.module';
import { MaterialModule, MdTableModule } from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ReviewsListComponent } from './reviews-list.component';
import { ReviewsListService } from './reviews-list.service';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReviewDetailsDialogComponent } from "./review-details-dialog/review-details-dialog.component";
import { ReviewsDetailsDialogService } from "./review-details-dialog/review-details-dialog.service";
import { ReviewInformationDialogComponent } from "./review-informations/review-information.component";

@NgModule({
  imports: [
    FlexLayoutModule, MaterialModule, SharedModule, CommonModule,
    FormsModule, ApiModule,  RouterModule, MdTableModule, CdkTableModule, ReactiveFormsModule
  ],
  declarations: [ReviewsListComponent, ReviewDetailsDialogComponent, ReviewInformationDialogComponent],
  providers: [ReviewsListService, ReviewsDetailsDialogService],
  entryComponents: [ReviewDetailsDialogComponent, ReviewInformationDialogComponent]
})
export class ReviewsListModule { }
