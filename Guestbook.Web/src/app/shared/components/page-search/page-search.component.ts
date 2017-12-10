import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { Subject } from 'rxjs/Subject';
import 'rxjs/add/operator/debounceTime';

@Component({
    selector: 'page-search',
    templateUrl: "./page-search.component.html",
    styleUrls: ['./page-search.component.css'],
    moduleId: module.id
})
export class PageSearchComponent {
    private debouncer: Subject<string> = new Subject<string>();

    @Input() search: string;

    @Output() searchChange: EventEmitter<string>;

    @Input() placeholder  = 'Поиск';


    constructor() {
        this.searchChange = new EventEmitter<string>();
    }

    onChange(search: string) {
        this.searchChange.emit(search);

    }

}