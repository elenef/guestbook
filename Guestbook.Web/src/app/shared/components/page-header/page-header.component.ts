import { Router } from '@angular/router';
import { AuthorizationService } from './../../../api/authorization.service';
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'page-header',
    templateUrl: "./page-header.component.html",
    styleUrls: ['./page-header.component.css'],
    moduleId: module.id
})
export class PageHeaderComponent {
    logout = './../../../../../assets/img/logout.png';

    @Input() header: string;

    @Input() subHeader: string;

    @Input() searchVisible: boolean;

    @Input() buttonVisible: boolean;

    @Input() buttonPlaceholder: string;

    @Input() buttonLogoutVisible: boolean;

    @Input() search: string;

    @Input() textStyles: string;

    @Input() fxLayoutAlignHeader;

    @Input() searchPlaceholder = 'Поиск';

    @Output() headerClick: EventEmitter<void>;

    @Output() searchChange: EventEmitter<string>;

    @Output() addButtonClick: EventEmitter<void>;




    get allowHeaderClick(): boolean {
        return this.subHeader != null;
    }

    constructor(
        private router: Router,
        private authorizationService: AuthorizationService,

    ) {
        this.headerClick = new EventEmitter<void>();
        this.searchChange = new EventEmitter<string>();
        this.addButtonClick = new EventEmitter<void>();
    }

    onHeaderClick() {
        if (this.allowHeaderClick) {
            this.headerClick.emit();
        }
    }

    onSearch(text: string) {
        this.searchChange.emit(text);
    }

    onAdd() {
        this.addButtonClick.emit();
    }

}
