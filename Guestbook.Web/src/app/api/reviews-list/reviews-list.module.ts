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

@NgModule({
  imports: [
    FlexLayoutModule, MaterialModule, SharedModule, CommonModule,
    FormsModule, ApiModule,  RouterModule, MdTableModule, CdkTableModule, ReactiveFormsModule
  ],
  declarations: [ReviewsListComponent],
  providers: [ReviewsListService],
})
export class ReviewsListModule { }