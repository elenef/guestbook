import { UserDetailsService } from './../user-details/user-details.service';
import { UserDetailsComponent } from './../user-details/user-details.component';
import { AppModule } from '../../app.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'
import { FormsModule, ReactiveFormsModule } from '@angular/forms'
import { MaterialModule } from '@angular/material'
import { SharedModule } from '../../shared/shared.module'
import { ApiModule, BaseApiService, ApiService,} from '../../api'
import { FlexLayoutModule } from '@angular/flex-layout'
import {MdTableModule} from '@angular/material';
import { CdkTableModule } from '@angular/cdk';
import { MessageService } from "../../shared/services/message.service";


@NgModule({
  imports:      [  FlexLayoutModule, MaterialModule,
   SharedModule, CommonModule, FormsModule, ReactiveFormsModule, ApiModule, MdTableModule, CdkTableModule],
  declarations: [ UserDetailsComponent],
  providers:    [UserDetailsService
    ],
})
export class UserModule { }