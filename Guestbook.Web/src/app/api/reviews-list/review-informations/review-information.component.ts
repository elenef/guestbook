import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { MdDialogRef } from '@angular/material';
import { MessageService } from '../../../shared/services/message.service';
import { Review } from '../../contracts/review';
import * as _ from 'underscore';


@Component({
  selector: 'app-review-information',
  templateUrl: './review-information.component.html',
  styleUrls: ['./review-information.component.css']
})
export class ReviewInformationDialogComponent implements OnInit {
  review: Review;
  colors = ['#FF2B95', '#2BF1aF', '#F8D407', '#8C16FC', '#1EF71E', '#1EDEF7'];

  constructor(
    private dialogRef: MdDialogRef<ReviewInformationDialogComponent>,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService,
  ) {
  }


  ngOnInit() {
      console.log(this.review);
  }

  onCancel() {
    this.dialogRef.close();
  }

   getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
  }
}