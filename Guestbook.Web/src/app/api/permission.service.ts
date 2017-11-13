import * as _ from 'underscore'
import { Injectable } from '@angular/core'
import { AuthorizationService } from './authorization.service'
import { UserRoles } from './user-roles'

@Injectable()
export class PermissionService {
    private static adminRole = UserRoles.admin;

    private static permissionsMap = {

    };

    constructor(
        private authService: AuthorizationService
    ) { }

    isAvailable(action: string) {
        //User is not authorized
        if (this.authService.authorizationRequired) {
            return false;
        }

        var userRole = this.authService.userProfileRole;

        if (userRole == PermissionService.adminRole) {
            return true;
        }

        //Roles is not in list
        if (!PermissionService.permissionsMap[userRole]) {
            return false;
        }

        return _.some(PermissionService.permissionsMap[userRole], permission => permission == action);
    }
}
