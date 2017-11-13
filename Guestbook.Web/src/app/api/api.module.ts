import { AdminGuard } from './guards/admin-guard.service';
import { AuthGuard } from './guards/auth-guard.service';
import { PermissionService } from './permission.service';
import { CompApiService } from './comp-api.service';
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
        AuthorizationService,
        { provide: ApiService, useClass: CompApiService },
        CompApiService,
        AuthGuard,
        AdminGuard,
        PermissionService,
    ],
    exports: [],
})
export class ApiModule { }
