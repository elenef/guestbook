import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Subject } from 'rxjs/Subject';
import { ListItem } from './../../models/list-item';
import { Subscriber } from 'rxjs/Subscriber';
import { ItemList } from './../../../api/contracts/item-list';
import { SimpleDataFilter } from './../../../api/contracts/simple-data-filter';
import { ApiEndpoint } from './../../../api/api-endpoint';
import { Observable } from 'rxjs';
import { MdSort, MdPaginator } from '@angular/material';
import { ApiService } from '../../../api/api.service';
import * as _ from 'underscore'
import { ApiEndpoints } from "../../../api/api-endpoints";
import {DataSource} from '@angular/cdk';


export class TableDataSource<TContract> extends DataSource<any> {
  private dataChange: BehaviorSubject<any> = new BehaviorSubject<any>([]);

  //itemsCount: number;
  //page: number;
  //pageSize: number;
  filterParameters: any;
  itemRemove: any;


  get data(): any[] { return this.dataChange.value; }


  constructor(
    private endpoint: ApiEndpoint,
    private apiService: ApiService,
    private filterParams?: any,
    private mapData?: (data: TContract[]) => any[]) {
    super();
 
  }

  refreshData(filterParameters: any): Observable<any> {
    let ctx = this;
    return new Observable<any>((sub: Subscriber<any>) => {
      let url = ctx.endpoint.listUrl(filterParameters);
      ctx.apiService.get<ItemList<TContract>>(url)
        .subscribe(res => {
          let mappedData = ctx.mapData ? ctx.mapData(res.data) : res.data;
          ctx.dataChange.next(mappedData);
          sub.next(res);
          sub.complete();
        }, error => {
          ctx.dataChange.error(error);
          sub.error(error);
          sub.complete();
        })
    })
  }

  connect(): Observable<any> {
    return this.dataChange;
  }

  disconnect() {
    this.dataChange.complete();
    this.dataChange.unsubscribe();
  }

  mapList() {
    const mapList = _.without(this.data, this.itemRemove);
    this.dataChange.next(mapList);
  }


  delete(): Observable<void> {
    let ctx = this;
    return new Observable<void>(observable => {
      if (this.itemRemove.id) {
        let url = this.endpoint.itemUrl(this.itemRemove.id);
        ctx.apiService.delete(url)
          .subscribe(id => {
            observable.next();
            observable.complete();
          },
          error => {
            observable.error(error);
            observable.complete();
          });
      } else {
        observable.next();
        observable.complete();
      }
    });
  }


  protected processError(observable: any, error: any): void {
    observable.error(error);
    observable.complete();
  }

}
