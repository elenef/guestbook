import { AuthorizationService } from './authorization.service';
import { NgModule } from '@angular/core';
import { HttpModule } from '@angular/http';
import { ApiService } from './api.service';
import { BaseApiService } from './base-api.service';



@NgModule({
    imports: [HttpModule],
    declarations: [],
    providers: [
        BaseApiService,
        AuthorizationService
    ],
    exports: [],
})
export class ApiModule { }
