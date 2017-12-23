import { NgModule }      from '@angular/core';
import { CommonModule } from '@angular/common'
import { MaterialModule } from '@angular/material'

import { FormsModule } from '@angular/forms'
import { FlexLayoutModule } from '@angular/flex-layout'

import { SharedModule } from '../shared/shared.module'

import { LoginComponent } from './login.component'
import { LoginService } from './login.service';

@NgModule({
  imports: [ CommonModule, MaterialModule, SharedModule, FlexLayoutModule, FormsModule  ],
  declarations: [ LoginComponent ],
  exports:      [ LoginComponent ],
  providers: [ LoginService ]
})
export class LoginModule { }