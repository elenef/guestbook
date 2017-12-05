import { ReviewsListService } from './reviews-list.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reviews-list',
  templateUrl: './reviews-list.component.html',
  styleUrls: ['./reviews-list.component.css']
})
export class ReviewsListComponent implements OnInit {
  example = ['item 1', 'item 2'];
  constructor(
    private reviewsListService: ReviewsListService
  ) { }

  ngOnInit() {
    this.reviewsListService.getReviewsList().subscribe((res) => {
      console.log(res);
      console.log("1");
    });
  }

}
