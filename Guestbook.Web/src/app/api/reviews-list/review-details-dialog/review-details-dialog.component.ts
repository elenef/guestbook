
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { MdDialogRef } from '@angular/material';
import { MessageService } from '../../../shared/services/message.service';
import { Review } from '../../contracts/review';
import { ReviewsDetailsDialogService } from "./review-details-dialog.service";
import { Restaurant } from "../../contracts/restaurant";

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

@Component({
  selector: 'app-review-details-dialog',
  templateUrl: './review-details-dialog.component.html',
  styleUrls: ['./review-details-dialog-dialog.component.css']
})
export class ReviewDetailsDialogComponent implements OnInit {
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern(EMAIL_REGEX)]);
  review: Review = new Review();
  restaurantList: Restaurant[];

  constructor(
    private dialogRef: MdDialogRef<ReviewDetailsDialogComponent>,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService,
    private service: ReviewsDetailsDialogService,
  ) {
  }


  ngOnInit() {
    this.service.getRestaurantList().subscribe(res => this.restaurantList = res.data);
    this.review.reviewRating = 0;
  }

  onCreateReview(form: FormGroup) {
    if (form.valid &&
      !this.emailFormControl.hasError('pattern') &&
      !this.emailFormControl.hasError('required')) {
        if(this.review.reviewRating == 0){
          this.messageService.error('Вы не поставили рейтинг ресторану.');
        } else {
         this.service.createReview(this.review)
        .subscribe(m => {
          this.messageService.success('Отзыв о ресторане успешно создан.');
          this.onCancel();
        }, (error) => {
          this.messageService.error(error.json());
          this.onCancel();
        });
        }     
    }
  }


  onCancel() {
    this.dialogRef.close();
  }
}
