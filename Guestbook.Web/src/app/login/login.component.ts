import { Component, EventEmitter, OnInit, Input, Output } from '@angular/core';
import { LoginService } from './login.service';
import { FormGroup } from '@angular/forms'
import { Router } from '@angular/router'

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css'],
    moduleId: module.id
})
export class LoginComponent implements OnInit {

    username: string;
    password: string;


    constructor(
        private loginService: LoginService,
        private router: Router
    ) {
    }

    ngOnInit() {
    }

    login(form: FormGroup) {
        if (form.valid) {
            this.loginService.login(this.username, this.password);
        }
    }



}
