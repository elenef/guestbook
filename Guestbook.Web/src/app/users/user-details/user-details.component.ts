import { Config } from './../../api/config';
import { FormGroup } from '@angular/forms/src/model';
import { ListItem } from './../../shared/models/list-item';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from './../../shared/services/confirmation.service';
import { MessageService } from './../../shared/services/message.service';
import { UserDetailsService } from './user-details.service';
import { User } from './../../api/contracts/user';
import { PageDetailsComponent } from './../../shared/components/page-details/page-details.component';
import { Component, OnInit } from '@angular/core';
import { FormControl, Validators, ReactiveFormsModule, NgModel } from '@angular/forms';
import * as _ from 'underscore';

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  providers: [MessageService]
})
export class UserDetailsComponent extends PageDetailsComponent<User> implements OnInit {
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern(EMAIL_REGEX)]);
  passwordConfirmation: string;
  disabled = true;


  constructor(
    private service: UserDetailsService,
    messageService: MessageService,
    confirmationService: ConfirmationService,
    route: ActivatedRoute,
    router: Router,
  ) {
    super(
      messageService,
      confirmationService,
      route,
      router,
      service,
      '/users',
    );
  }

  onSave(form: FormGroup) {
    if (form.valid &&
      !this.emailFormControl.hasError('pattern') &&
      !this.emailFormControl.hasError('required')) {
      if ((this.model.password || this.passwordConfirmation) &&
        (this.model.password !== this.passwordConfirmation)) {
        this.messageService.error('Пароль и подтверждение не совпадают');
        return;
      }
      super.onSave(form);
    }
  }


}
