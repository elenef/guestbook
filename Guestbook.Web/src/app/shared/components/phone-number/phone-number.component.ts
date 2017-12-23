import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'phone-number',
    templateUrl: './phone-number.component.html',
    styleUrls: ['./phone-number.component.css'],
    moduleId: module.id
})
export class PhoneNumberComponent {
    @Input() phoneField: string;

    @Input() showPhoneField: string;

    @Input() placeholder: string;

    @Input() disabled: string;

    @Input() phoneName: string;

    @Input() fieldSize: string;

    @Input() registrationColor: string;

    @Output() phoneFieldChange = new EventEmitter<string>();
    onPhoneChange(model: string) {
        this.phoneField = model;
        this.phoneFieldChange.emit(model);
    }

    constructor() {
        this.phoneField = '';
        this.showPhoneField = '';
    }

}