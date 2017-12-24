import { ApiEndpoints } from './../../api/api-endpoints';
import { MessageService } from './../../shared/services/message.service';
import { ConfirmationService } from './../../shared/services/confirmation.service';
import { FilterService } from './../../shared/services/filter.service';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from './../../api/contracts/user';
import { Component, OnInit } from '@angular/core';
import { PageListComponent } from '../../shared/components/page-list/page-list.component';
import { ApiService } from '../../api/api.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent extends PageListComponent<User> {
  displayedColumns = ['name', 'login', 'email', 'itemsAction'];

  constructor(
    apiService: ApiService,
    route: ActivatedRoute,
    filterService: FilterService,
    router: Router,
    confirmationService: ConfirmationService,
    messageService: MessageService
  ) {
    super(
      apiService,
      ApiEndpoints.users(),
      route,
      filterService,
      router,
      'пользовател',
      ['ь', 'я', 'ей'],
      confirmationService,
      messageService,
      'users',
      'user');
  }
}
