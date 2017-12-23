import { Observable } from 'rxjs';

import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { Restaurant } from "../../contracts/restaurant";
import { ApiEndpoints } from "../../api-endpoints";
import { ItemList } from "../../index";
import { Review } from "../../contracts/review";

@Injectable()
export class ReviewsDetailsDialogService {

    constructor(
        private apiService: ApiService
    ) { }

    getRestaurantList(): Observable<ItemList<Restaurant>> {
        const url = ApiEndpoints.restaurants().listUrl();
        return this.apiService.get<ItemList<Restaurant>>(url);
    }

    createReview(review: Review) {
        const url = ApiEndpoints.reviews().listUrl();
        return this.apiService.post<Review>(url, review);

    }

}
