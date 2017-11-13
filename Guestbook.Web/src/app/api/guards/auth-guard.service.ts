import { Observable } from 'rxjs/Rx';
import { Injectable } from "@angular/core";
import { CanActivate, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

import { AuthorizationService } from '../authorization.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(
        private router: Router,
        private authService: AuthorizationService,
    ) {

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        if (!this.authService.authorizationRequired) {
            return true;
        }
        this.router.navigate(['/login']);
        return false;
    }

}
