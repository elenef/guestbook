import { Component, OnInit } from '@angular/core';
import { BaseApiService } from "./api/base-api.service";
import { ApiService } from "./api/api.service";
import { AuthorizationService } from "./api/authorization.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'app';

  get inProgressState(): boolean {
    return (<BaseApiService>this.apiService).loadInProgress;
  }

    constructor(
    private apiService: ApiService,
    private authorizationService: AuthorizationService
  ) {
  }

  ngOnInit() {
    if (!this.authorizationService.authorizationRequired) {
      this.authorizationService.fillUserProfile()
        .subscribe(() => {
        });
    }
  }

}
