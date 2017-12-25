import { Component, OnInit } from '@angular/core';
import { PageDetailsComponent } from "../shared/components/page-details/page-details.component";
import { User } from "../api/contracts/user";
import { UserDetailsService } from "../users/user-details/user-details.service";
import { MessageService } from "../shared/services/message.service";
import { ConfirmationService } from "../shared/services/confirmation.service";
import { ActivatedRoute, Router } from "@angular/router";
import { LoginService } from "../login/login.service";
import { AuthorizationService } from "../api/authorization.service";
import { PermissionService } from "../api/permission.service";

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent  extends PageDetailsComponent<User> implements OnInit {

  constructor(
    private service: UserDetailsService,
    messageService: MessageService,
    confirmationService: ConfirmationService,
    route: ActivatedRoute,
    router: Router,
    loginService: LoginService,
    authorizationService: AuthorizationService,
     permissionService: PermissionService,    
  ) {
    super(
      messageService,
      confirmationService,
      route,
      router,
      service,
      '/users',
      authorizationService,
      permissionService,    
      loginService,
    );
  }

  ngOnInit() {
  }

  onRegistration(validProvider: any) {
    super.onRegistration(validProvider.login, validProvider.password);
  }

}
