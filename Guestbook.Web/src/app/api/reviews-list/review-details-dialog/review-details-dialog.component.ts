
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { MdDialogRef } from '@angular/material';
import { MessageService } from '../../../shared/services/message.service';
import { Review } from '../../contracts/review';

@Component({
  selector: 'app-review-details-dialog',
  templateUrl: './review-details-dialog.component.html',
  styleUrls: ['./review-details-dialog-dialog.component.css']
})
export class ReviewDetailsDialogComponent {
    review: Review = new Review();
    constructor(
    private dialogRef: MdDialogRef<ReviewDetailsDialogComponent>,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService,
  ) {
  }


  onCancel() {
    this.dialogRef.close();
  }
}
