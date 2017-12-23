import { AuthorizationService } from './../api/authorization.service';
import { Observable, Subject } from 'rxjs/Rx';
import { Injectable, EventEmitter, Output, Input } from '@angular/core';
import { Router } from '@angular/router';
import * as _ from 'underscore';

@Injectable()
export class LoginService {
    constructor(
        private authorizationService: AuthorizationService,
        private router: Router,
    ) {

    }


    login(username: string, password: string) {
        this.authorizationService.authorize(username, password);
    }
}
