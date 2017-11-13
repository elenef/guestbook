import { MdDialog } from '@angular/material';
import { ConfirmationService } from './../../services/confirmation.service';
import { PermissionService } from './../../../api/permission.service';
import { MessageService } from './../../services/message.service';
import { IModelService } from './../../services/model.service';
import { IModel } from './../../models/model';
import { Observable } from 'rxjs/Observable';
import { ApiEndpoint } from './../../../api/api-endpoint';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, Input } from '@angular/core';
import { ApiService } from '../../../api/api.service';
import * as _ from 'underscore';
import { FormGroup } from "@angular/forms";
import { AuthorizationService } from "../../../api/authorization.service";

export class PageDetailsComponent<TModel extends IModel> implements OnInit {
  protected modelId: string;
  protected checkInit = false;

  textSnackBar: string = "Сохранено успешно";
  protected acceptConditions: boolean = false;


  @Input() pageHeaderVisible: boolean = true;
  @Input() passwordButtonVisible: boolean = true;
  @Input() isRegistration: boolean = false;

  @Input() registrationColor: string;
  @Input() registrationForm: string;
  @Input() registrationFormBtn: string;


  @Input() viewModel: TModel;
  @Input() isProviderRegistrationRequest;

  get successResult(): string {
    if (this.isRegistration) {
      return 'Регистрация';
    }
    if (!this.newItem) {
      return 'Сохранить';
    }
    if (this.newItem && !this.isRegistration) {
      return 'Создать';
    }
  }

  

  get newItem(){
    return this.modelService.newItem;
  }

  get successBtnDisabled() {
    return this.acceptConditions ? false : true;
  }

  get showPasswordFields(): boolean {
    if (!this.newItem || this.isRegistration) {
      return !this.passwordButtonVisible;
    }
    if (!this.isRegistration && this.newItem) {
      return true;
    }
  }

  get model() {
    return this.modelService.model;
  }

  set model(model: TModel) {
    this.modelService.model = model;
  }

  constructor(
    protected messageService: MessageService,
    protected confirmationService: ConfirmationService,
    protected route: ActivatedRoute,
    protected router: Router,
    protected modelService: IModelService<TModel>,
    protected parentRouteName: string,
    protected authorizationService?: AuthorizationService,
    private permissionService?: PermissionService,
    protected dialog?: MdDialog,
  ) {

  }

  ngOnInit() {
    this.route.params
      .subscribe(params => {
        const id = params['id'];
        this.modelId = id != null ? id.trim() : id;
        this.loadModel();
      });
  }


  protected loadModel() {
    this.modelService.init(this.modelId)
      .subscribe(model => {
        console.log(model);
        this.checkInit = true;
      }, error => this.processError(error, this));
  }

  onSave(form: FormGroup) {
    if (form.valid == true) {
      this.modelService.saveChanges()
        .subscribe(m => {
          this.messageService.success(this.textSnackBar);
          this.router.navigate([this.parentRouteName]);
        }, (error: String) =>
          this.processError(error, this));
    }
  }




  protected processError(error: any, ctx: PageDetailsComponent<TModel>) {
    return Observable.throw(error);
  }

  returnBack(ev: any) {
    this.router.navigate([this.parentRouteName]);
  }

  isAvailable(action: string) {
    return this.permissionService.isAvailable(action);
  }

}
