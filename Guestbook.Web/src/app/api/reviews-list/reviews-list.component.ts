import { Review } from './../contracts/review';
import { ReviewsListService } from './reviews-list.service';
import { Component, OnInit } from '@angular/core';
import { MdDialog } from '@angular/material';
import { ReviewDetailsDialogComponent } from "./review-details-dialog/review-details-dialog.component";
import { ReviewInformationDialogComponent } from "./review-informations/review-information.component";
import { Restaurant } from "../contracts/restaurant";
import { Router, ActivatedRoute } from '@angular/router';
import { DateTime } from "../../shared/utils/date-time";
import { RestaurantDetailsDialogComponent } from "./restaurant-details-dialog/restaurant-details-dialog.component";
import { PermissionService } from "../permission.service";
import { AuthorizationService } from "../authorization.service";

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
  isLike: boolean = false;

  dateFrom: string = '';
  dateTo: string = '';
  startTimeStamp: number = 0;
  endTimeStamp: number = 0;
  unixDay: number = 86399;

  orderBy: string = 'created';
  orderDesc: boolean = false;


  typesSort = [
    { value: 'asc', viewValue: 'возрастанию даты' },
    { value: 'desc', viewValue: 'убыванию даты' },
  ];

  typeSort: string = "";


  constructor(
    private reviewsListService: ReviewsListService,
    protected dialog: MdDialog,
    private router: Router,
    private route: ActivatedRoute,
    private permissionService: PermissionService,
    private authorizationService: AuthorizationService
  ) { }


  onCreateReview() {
    const dialogRef = this.dialog.open(ReviewDetailsDialogComponent);
    dialogRef.afterClosed().subscribe(() => {
      this.getReviewsList();
    });
  }

  onCreateRestaurant() {
    const dialogRef = this.dialog.open(RestaurantDetailsDialogComponent);
    dialogRef.afterClosed().subscribe(() => {
      this.reviewsListService.getRestaurantsList().subscribe((res) => {
        this.restaurants = res.data;
      });
    });
  }

  onShowReview(review: Review) {
    if (!this.isLike) {

      const dialogRef = this.dialog.open(ReviewInformationDialogComponent);
      dialogRef.componentInstance.review = review;
    }
    this.isLike = false;
  }

  isAvailable(action: string) {
    return this.permissionService.isAvailable(action);
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
        let dateFrom = queryParams['dateFrom'];
        if (dateFrom) {
          this.startTimeStamp = dateFrom;
          this.dateFrom = new DateTime(this.startTimeStamp).formatLocal('DD-MM-YYYY');
        }
        let dateTo = queryParams['dateTo'];
        if (dateTo) {
          this.endTimeStamp = dateTo;
          this.dateTo = new DateTime(this.endTimeStamp).formatLocal('DD-MM-YYYY');
        }
        let orderBy = queryParams['orderBy'];
        if (orderBy) {
          this.orderBy = orderBy;
        }
        let orderDesc = queryParams['orderDesc'];
        if (orderDesc) {
          this.typeSort = 'desc';
          this.orderDesc = orderDesc;
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
      'dateFrom': this.startTimeStamp,
      'dateTo': this.endTimeStamp,
      'orderBy': this.orderBy,
      'orderDesc': this.orderDesc,
    };

    for (let parameter in parameters) {
      if (!parameters[parameter]) {
        delete parameters[parameter];
      }
    }



    this.router.navigate(['reviews'], { queryParams: parameters });
  }


  getReviewsList() {
    this.reviewsListService.getReviewsList(this.restaurantName, this.search,
      this.startTimeStamp, this.endTimeStamp, this.orderBy, this.orderDesc).subscribe((res) => {
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

  onStartDateChange() {
    if (this.dateFrom !== null) {
      this.startTimeStamp = new Date(this.dateFrom).getTime() / 1000;
    } else {
      this.startTimeStamp = 0;
    }
    this.onFiltrationReviews();
  }

  onEndDateChange() {
    if (this.dateTo !== null) {
      this.endTimeStamp = new Date(this.dateTo).getTime() / 1000 + this.unixDay;
    } else {
      this.endTimeStamp = 0;
    }
    this.onFiltrationReviews();
  }

  onSort() {
    let ascTypeSort = 'asc';
    this.orderDesc = this.typeSort === ascTypeSort ? false : true;
    this.onFiltrationReviews();
  }

}
