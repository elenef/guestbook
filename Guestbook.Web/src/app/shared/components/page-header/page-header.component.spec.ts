import { async, inject, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterModule, Routes } from '@angular/router';

import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { NO_ERRORS_SCHEMA } from '@angular/core';

import { PageHeaderComponent } from './page-header.component';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthorizationService } from './../../../api/authorization.service';

/*describe('PageHeaderComponent', () => {

    let comp: PageHeaderComponent;
    let fixture: ComponentFixture<PageHeaderComponent>;
    let de: DebugElement;
    let el: HTMLElement;



    beforeEach( async(() => {
        TestBed.configureTestingModule({
            imports: [
                RouterTestingModule.withRoutes([])
              ],
            declarations: [ PageHeaderComponent ],
            schemas: [NO_ERRORS_SCHEMA]
        })
        .compileComponents()
        .then(() => {
            fixture = TestBed.createComponent(PageHeaderComponent);
            comp = fixture.componentInstance;
        })
    }));

    beforeEach(() => {
        fixture = TestBed.createComponent(PageHeaderComponent);
        comp = fixture.componentInstance;
        fixture.detectChanges();
        de = fixture.debugElement.query(By.css('.page-header-container'));
        el = de.nativeElement;
    });

    it('should be created', () => {
        expect(comp).toBeTruthy();
    });

    it ('should display a different test header and subHeader', () => {
        comp.header = 'Invoices';
        comp.subHeader = 'Invoice №1';
        fixture.detectChanges();
        expect(el.textContent).toContain(comp.header);
        expect(el.textContent).toContain(comp.subHeader);
    });

});*/
