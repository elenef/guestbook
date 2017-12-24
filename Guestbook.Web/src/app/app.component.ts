import { Component } from '@angular/core';
import { BaseApiService } from "./api/base-api.service";
import { ApiService } from "./api/api.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';

  get inProgressState(): boolean {
    return (<BaseApiService>this.apiService).loadInProgress;
  }

    constructor(
    private apiService: ApiService,
  ) {
  }
}
