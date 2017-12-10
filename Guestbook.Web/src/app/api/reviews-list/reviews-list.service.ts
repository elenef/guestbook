import { Review } from './../contracts/review';
import { ItemList } from './../contracts/item-list';
import { ApiEndpoints } from './../api-endpoints';
import { Observable } from 'rxjs';

import { ListItem } from './../../shared/models/list-item';
import { Injectable } from '@angular/core';
import { ApiService } from '../api.service';

@Injectable()
export class ReviewsListService {

  constructor(
    private apiService: ApiService
  ) { }

  getReviewsList(): Observable<ItemList<Review>> {
    const url = ApiEndpoints.reviews().listUrl();
    return this.apiService.get<ItemList<Review>>(url);
  }

}
