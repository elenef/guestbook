
import { IModelService } from './model.service';
import { IModel } from './../models/model';
import { Observable } from 'rxjs/Observable';
import { ApiEndpoint } from './../../api/api-endpoint';
import { Router, ActivatedRoute } from '@angular/router';
import { ListItem } from './../models/list-item';
import { Component, OnInit } from '@angular/core';
import { ApiService } from "../../api/api.service";
import * as _ from 'underscore'

export class ModelService<TModel extends IModel> implements IModelService<TModel>, OnInit {
    roles: ListItem[];
    model: TModel;
    newItem: boolean;


    getRoles() { //TODO: Move to user service
        return [
            new ListItem({ id: 'admin', name: 'Администратор' }),
            new ListItem({ id: 'operator', name: 'Оператор' })
        ]
    }

    constructor(
        protected apiService: ApiService,
        protected router: Router,
        protected route: ActivatedRoute,
        protected endpoint: ApiEndpoint,
        protected modelFactoryFunc: (model?: TModel) => TModel,
    ) {

    }

    ngOnInit(): void {
        this.route.params
            .subscribe(params => {
                let id = params['id'];
                this.init(id).subscribe(model => {
                });
            });
    }

    init(modelId: string, params?: string): Observable<TModel> {
        let url = params ? this.endpoint.listUrl(params) : this.endpoint.itemUrl(modelId);
        let ctx = this;
        return new Observable<TModel>(observable => {
            ctx.newItem = !modelId;
            if (modelId) {
                ctx.apiService.get<TModel>(url)
                    .subscribe(model => {
                        ctx.model = ctx.modelFactoryFunc(model);
                        observable.next(model);
                        observable.complete();
                    },
                    error => ctx.processError(observable, error))
            } else {
                ctx.model = ctx.modelFactoryFunc();
                observable.next(this.model);
                observable.complete();
            }
        });
    }

    saveChanges(isNewItem?: boolean) {
        this.newItem = isNewItem ? true : this.newItem;
        let request = this.newItem
            ? this.apiService.post(this.endpoint.listUrl(), this.dataTransferObject)
            : this.apiService.put(this.endpoint.itemUrl(this.model.id), this.dataTransferObject);

        let ctx = this;
        return new Observable<TModel>(observable => {
            request.subscribe(model => {
                ctx.model = ctx.modelFactoryFunc(model);
                ctx.newItem = false;

                observable.next(model);
                observable.complete();
            },
                error => ctx.processError(observable, error));
        })
    }

    delete(): Observable<void> {
        let ctx = this;
        return new Observable<void>(observable => {
            if (ctx.model.id) {
                let url = ctx.endpoint.itemUrl(ctx.model.id);
                ctx.apiService.delete(url)
                    .subscribe(id => {
                        observable.next();
                        observable.complete();
                    },
                    error => ctx.processError(observable, error));
            } else {
                observable.next();
                observable.complete();
            }
        });
    }

    protected get dataTransferObject(): any {
        var data: any = this.model;
        return data.serialize != null ? data.serialize() : data;
    }

    protected get modelObject() {
        return this.model;
    }

    protected processError(observable: any, error: any): void {
        observable.error(error);
        observable.complete();
    }

}
