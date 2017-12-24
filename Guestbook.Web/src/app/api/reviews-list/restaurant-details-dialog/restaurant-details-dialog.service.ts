import { Observable } from 'rxjs';

import { Injectable } from '@angular/core';
import { ApiService } from '../../api.service';
import { Restaurant } from "../../contracts/restaurant";
import { ApiEndpoints } from "../../api-endpoints";
import { ItemList } from "../../index";
import { Review } from "../../contracts/review";

@Injectable()
export class RestaurantDetailsDialogService {

    constructor(
        private apiService: ApiService
    ) { }

    createRestaurant(r: Restaurant) {
        const url = ApiEndpoints.restaurants().listUrl();
        return this.apiService.post<Restaurant>(url, r);

    }

}
