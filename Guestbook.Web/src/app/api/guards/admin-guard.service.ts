import { Observable } from 'rxjs/Rx';
import { Injectable } from "@angular/core";
import { CanActivate, CanActivateChild, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router'
import { UserRoles } from '../user-roles'

import { AuthorizationService } from '../authorization.service'

@Injectable()
export class AdminGuard implements CanActivate, CanActivateChild {

    constructor(
        private router: Router,
        private authService: AuthorizationService,
    ) {

    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        //TODO
        /*if() {
            return true;
        }*/

        this.router.navigate(['/login']);
        return false;
    }

    canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.canActivate(route, state);
    }
}