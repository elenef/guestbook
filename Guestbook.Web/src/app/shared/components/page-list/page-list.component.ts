import { MessageService } from './../../services/message.service';
import { WordUtils } from './../../word-utils';
import { ItemList } from './../../../api/contracts/item-list';
import { FilterService } from './../../services/filter.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscriber } from 'rxjs/Rx';
import { ApiEndpoint } from './../../../api/api-endpoint';
import { SimpleDataFilter } from './../../../api/contracts/simple-data-filter';
import { Component, OnInit, ViewChild, Input, EventEmitter, Output, ElementRef } from '@angular/core';
import { MdSort, PageEvent, MdPaginator, MdDialog } from '@angular/material';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/startWith';
import 'rxjs/add/observable/merge';
import 'rxjs/add/operator/map';
import { DataSource } from '@angular/cdk';
import { ApiService } from '../../../api/api.service';
import { TableDataSource } from './table-data-source';
import { ApiEndpoints } from './../../../api/api-endpoints';
import { ConfirmationService } from '../../services/confirmation.service';





export class PageListComponent<TContract> implements OnInit {
  dataSource: TableDataSource<TContract>;
  model = new ItemList<TContract>({});
  filterParams = new SimpleDataFilter({ page: 0, pageSize: 25 });
  direction: string;
  pageSize = 25;
  selectedItem: TContract;
  checkInit = false;
  loading = true;


  constructor(
    protected apiService: ApiService,
    protected endpoint: ApiEndpoint,
    protected route: ActivatedRoute,
    protected filterService: FilterService,
    protected router: Router,
    protected baseSubHeader: string,
    protected endings: string[],
    protected confirmationService: ConfirmationService,
    protected messageService: MessageService,
    protected routeName: string,
    protected routeDetailsName: string,
    protected dialog?: MdDialog,

  ) {
  }

  get subHeader(): string {
    return this.model && this.checkInit ?
      WordUtils.GetForm(this.model.total, this.baseSubHeader, this.endings) : null;
  }

  @ViewChild(MdSort) sort: MdSort;


  onAdd() {
    this.router.navigate([this.routeDetailsName]);
  }

  onRedirect() {
    Object.keys(this.filterParams).map(key => {
      if (!this.filterParams['orderBy']) {
        delete this.filterParams['orderAsc'];
      }
      if (!this.filterParams[key] && this.filterParams[key] !== 0) {
        delete this.filterParams[key];
      }
    });
    this.router.navigate([this.routeName], { queryParams: this.filterParams });
    console.log(this.filterParams);
  }

  onEditItemModel(item: any) {
    this.router.navigate([this.routeDetailsName, item.id]);
  }


  onSearch(event: string) {
    this.filterParams.search = event;
    this.onRedirect();
    this.onRefreshData();
  }

  onChangePage(event: PageEvent) {
    this.filterParams.page = event.pageIndex;
    this.filterParams.pageSize = event.pageSize;
    this.pageSize = this.filterParams.pageSize;
    this.onRedirect();
    this.onRefreshData();
  }



  onRefreshData() {
    this.dataSource.refreshData(this.filterParams).subscribe((res) => {
      this.model = res;
    });
  }


  onSortData(event: MdSort) {
    if (!event.direction) {
      this.filterParams.orderBy = '';
      this.filterParams.orderDesc = false;
    } else {
      this.filterParams.orderBy = event.active;
      this.filterParams.orderDesc = event.direction == 'asc' ? false : true;
    }
    this.onRedirect();
    this.onRefreshData();
  }


  ngOnInit() {
    this.route.queryParams
      .subscribe((queryParams: Object) => {
        this.filterParams = this.filterService.init(queryParams, this.filterParams);
        this.direction = this.filterParams.orderDesc + '' == 'true' ? 'desc' : 'asc';
        const pageSize = queryParams['pageSize'];
        if (pageSize) {
          // tslint:disable-next-line:radix
          this.filterParams.pageSize = parseInt(pageSize);
        }
        const page = queryParams['page'];
        if (page) {
          // tslint:disable-next-line:radix
          this.filterParams.page = parseInt(page);
        }
      });
    this.onRedirect();
    this.dataSource =
      new TableDataSource<TContract>(this.endpoint, this.apiService, this.filterParams);
    this.dataSource.refreshData(this.filterParams).subscribe((res) => {
      this.model = res;
      this.checkInit = true;
    });
  }

  onRemove() {
    this.dataSource.itemRemove = this.selectedItem;
    this.dataSource.mapList();
    const mdSnackBarRef = this.confirmationService.confirmation('Отменить удаление ?');

    mdSnackBarRef.afterOpened().subscribe(() => {
      let cancelDelete = false;
      mdSnackBarRef.onAction().subscribe(() => {
        cancelDelete = true;
        this.loading = false;
        this.dataSource.refreshData(this.filterParams).subscribe((res) => {
          this.model = res;
          this.loading = true;
        });

      });

      mdSnackBarRef.afterDismissed().subscribe(() => {
        if (!cancelDelete) {
          this.dataSource.delete().subscribe(() => {
            this.selectedItem = null;
            this.dataSource.refreshData(this.filterParams).subscribe((res) => {
              this.model = res;
              this.loading = true;
            });
          }, error => {
            this.messageService.error(error);
            this.dataSource.refreshData(this.filterParams).subscribe((res) => {
              this.model = res;
              this.loading = true;
            });
          });
        }
        this.loading = false;
        cancelDelete = false;
      });

    });

  }

}


