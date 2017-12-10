import { Review } from './../contracts/review';
import { ReviewsListService } from './reviews-list.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-reviews-list',
  templateUrl: './reviews-list.component.html',
  styleUrls: ['./reviews-list.component.css']
})
export class ReviewsListComponent implements OnInit {
  colors = ['#FF2B95', '#2BF1aF', '#F8D407', '#8C16FC', '#1EF71E', '#1EDEF7'];
  reviews: Review[];
  constructor(
    private reviewsListService: ReviewsListService
  ) { }

  ngOnInit() {
    this.reviews = [
      new Review({comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
      new Review({comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
      new Review({comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
      new Review({comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
      new Review({comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
      new Review({comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
    ];
    /*this.reviewsListService.getReviewsList().subscribe((res) => {
      this.reviews = res.data;
    });*/
  }

  getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
  }

}
