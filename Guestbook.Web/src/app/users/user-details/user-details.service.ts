import { User } from './../../api/contracts/user';
import { ActivatedRoute, Router } from '@angular/router';
import { ModelService } from './../../shared/services/base-model.service';
import { Observable, Subscriber } from 'rxjs/Rx';
import { Injectable } from '@angular/core';
import { ApiService, ApiEndpoints, ItemList } from '../../api';
import { ListItem } from '../../shared/models';

@Injectable()
export class UserDetailsService  extends ModelService<User> {
    constructor(
        apiService: ApiService,
        route: ActivatedRoute,
        router: Router
    ) {
        super(apiService, router, route,
            ApiEndpoints.users(),
            (data) => new User(data));
    }


}
