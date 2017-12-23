import { Review } from './../contracts/review';
import { ReviewsListService } from './reviews-list.service';
import { Component, OnInit } from '@angular/core';
import { MdDialog } from '@angular/material';
import { ReviewDetailsDialogComponent } from "./review-details-dialog/review-details-dialog.component";
import { ReviewInformationDialogComponent } from "./review-informations/review-information.component";
import { Restaurant } from "../contracts/restaurant";
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-reviews-list',
  templateUrl: './reviews-list.component.html',
  styleUrls: ['./reviews-list.component.css']
})
export class ReviewsListComponent implements OnInit {
  colors = ['#2BF1aF', '#1EF71E', '#1EDEF7'];
  reviews: Review[];
  restaurants: Restaurant[];
  restaurantName: string;
  search: string = '';
  constructor(
    private reviewsListService: ReviewsListService,
    protected dialog: MdDialog,
    private router: Router,
    private route: ActivatedRoute,
  ) { }

 /*ngOnInit() {
    this.reviews = [
      new Review({ userName: 'Tommy', restaurantName: 'KFC', 
      comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
      new Review({ comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
      new Review({ comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
      new Review({ comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
      new Review({ comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
      new Review({ comment: 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris vitae dictum sapien, eget dapibus tortor. Phasellus maximus egestas quam id convallis. Nulla fermentum facilisis erat sit amet faucibus. Nullam' }),
    ];
    /*this.reviewsListService.getReviewsList().subscribe((res) => {
      this.reviews = res.data;
    });*/
//}

  onCreateReview() {
    const dialogRef = this.dialog.open(ReviewDetailsDialogComponent);
  }

  onShowReview() {
    const dialogRef = this.dialog.open(ReviewInformationDialogComponent);
    dialogRef.componentInstance.review = this.reviews[0];
  }


  
  ngOnInit() {
    this.route.queryParams
      .subscribe((queryParams: any) => {
        let restaurantName = queryParams['restaurantName'];
        if (restaurantName) {
          this.restaurantName = restaurantName;
        }
        let search = queryParams['search'];
        if (search) {
          this.search = search;
        }
        this.getReviewsList();
        this.reviewsListService.getRestaurantsList().subscribe((res) => {
          this.restaurants = res.data;
        });
      });
  }

  onFiltrationReviews() {
    let parameters = {
      'search': this.search,
      'restaurantName': this.restaurantName,
    };

    for (let parameter in parameters) {
      if (!parameters[parameter]) {
        delete parameters[parameter];
      }
    }

    this.router.navigate(['reviews'], { queryParams: parameters });
  }


  getReviewsList() {
    this.reviewsListService.getReviewsList(this.restaurantName, this.search).subscribe((res) => {
      this.reviews = res.data;
    });
  }

  updateReview(review: Review) {
    this.reviewsListService.updateReview(review).subscribe(() => {
        this.getReviewsList();
    });
  }

  getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min)) + min;
  }

  onSearch(search: string) {
    this.search = search;
    this.onFiltrationReviews();
  }

  onLikeReview(review: Review) {
    /*
     * while backend not update
    */
    //review.like = review.like ? review.like++ : 1;
    this.updateReview(review);
  }

}
