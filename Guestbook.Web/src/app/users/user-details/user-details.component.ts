import { Config } from './../../api/config';
import { FormGroup } from '@angular/forms/src/model';
import { ListItem } from './../../shared/models/list-item';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService } from './../../shared/services/confirmation.service';
import { MessageService } from './../../shared/services/message.service';
import { UserDetailsService } from './user-details.service';
import { User } from './../../api/contracts/user';
import { PageDetailsComponent } from './../../shared/components/page-details/page-details.component';
import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { FormControl, Validators, ReactiveFormsModule, NgModel } from '@angular/forms';
import * as _ from 'underscore';

const EMAIL_REGEX = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css'],
  providers: [MessageService]
})
export class UserDetailsComponent extends PageDetailsComponent<User> implements OnInit {
  @Output() validModelSubmitted: EventEmitter<User>;
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern(EMAIL_REGEX)]);
  passwordConfirmation: string;
  disabled = true;

  @Input() title = 'Детали пользователя';

  @Input() textButton = 'Сохранить';

  @Input() styleComponent;
  @Input() styleTitle;

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
    this.validModelSubmitted = new EventEmitter<User>();
  }

  onShowPasswordFiels() {
    this.model.password = null;
    this.passwordButtonVisible = false;
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
      if (this.isRegistration) {
        this.validModelSubmitted.emit(this.model);
      } else {
        super.onSave(form);
      }
    }
  }


}
