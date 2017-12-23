import { Restaurant } from './../contracts/restaurant';
import { Review } from './../contracts/review';
import { Router, ActivatedRoute } from '@angular/router';
import { ReviewsListService } from './reviews-list.service';
import { Component, OnInit } from '@angular/core';

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
    private router: Router,
    private route: ActivatedRoute,
  ) { }

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
    review.like = review.like ? ++review.like : 1;
    this.updateReview(review);
  }

}
