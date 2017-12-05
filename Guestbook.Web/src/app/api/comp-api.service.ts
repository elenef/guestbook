import { Observable, Subscriber } from 'rxjs/Rx';
import { AuthorizationService } from './authorization.service';
import { Http, Response, RequestOptionsArgs } from "@angular/http";
import { Injectable, Optional } from "@angular/core";
import { Headers, RequestOptions } from '@angular/http';
import { ErrorResponse } from './contracts'
import { ApiService } from './api.service'
import { BaseApiService } from './base-api.service'
import { MessageService } from "../shared/services/message.service";

@Injectable()
/**
 * Service allow interacts with api and displays error through toaster
 */
export class CompApiService implements ApiService {
    private HTTP_NOT_FOUND = 404;
    private requestsInProgress: number = 0;
    id = Math.random(); 

    get loadInProgress(): boolean {
        return this.requestsInProgress > 0;
    }
    lastError: ErrorResponse; 

    constructor(
        private apiService: BaseApiService,
        private messageService: MessageService
    ) { }

    public get<TContract>(url: string): Observable<TContract> {
        this.requestsInProgress++;

        let ctx = this;
        return new Observable<TContract>((obs: Subscriber<TContract>) => {
            ctx.apiService.get<TContract>(url)
                .subscribe(res => {
                    ctx.requestsInProgress--;
                    obs.next(res);
                    obs.complete();
                },
                error => {
                    ctx.requestsInProgress--;
                    ctx.handleError(error);
                    obs.error(error);
                    obs.complete();
                });
        });
    }

    public post<TContract>(url: string, item: TContract): Observable<TContract> {
        this.requestsInProgress++;

        let ctx = this;
        return new Observable<TContract>((obs: Subscriber<TContract>) => {
            ctx.apiService.post<TContract>(url, item)
                .subscribe(res => {
                    ctx.requestsInProgress--;
                    obs.next(res);
                    obs.complete();
                },
                error => {
                    ctx.requestsInProgress--;
                    ctx.handleError(error);
                    obs.error(error);
                    obs.complete();
                });
        });
    }

    public put<TContract>(url: string, item: TContract): Observable<TContract> {
        this.requestsInProgress++;

        let ctx = this;
        return new Observable<TContract>((obs: Subscriber<TContract>) => {
            return ctx.apiService.put<TContract>(url, item)
                .subscribe(res => {
                    ctx.requestsInProgress--
                    obs.next(res);
                    obs.complete();
                },
                error => {
                    ctx.requestsInProgress--;
                    ctx.handleError(error);
                    obs.error(error);
                    obs.complete();
                });
        });
    }

    public delete(url: string): Observable<Response> {
        this.requestsInProgress++;

        let ctx = this;
        return new Observable<Response>((obs: Subscriber<Response>) => {
            return ctx.apiService.delete(url)
                .subscribe(res => {
                    ctx.requestsInProgress--;
                    obs.next(res);
                    obs.complete();
                },
                error => {
                    ctx.requestsInProgress--;
                    ctx.handleError(error);
                    obs.error(error);
                    obs.complete();
                });
        });
    }

    private handleError(response: Response) {
        this.showErrorMessage(response);
    }

    private handleErrorAndThrow(response: Response): Observable<any> {
        this.showErrorMessage(response);
        return Observable.throw(this.lastError);
    }

    private showErrorMessage(response: Response) {
        let error = null;
        if (response != null) {
            if(response.json) {
                error = <ErrorResponse>response.json();
            } else {
                error = new ErrorResponse();
                error.errorMessage = "Не удалось выполнить запрос";
            }
        } else {
            if (response.status == this.HTTP_NOT_FOUND) {
                error = new ErrorResponse();
                error.errorMessage = "Запрашиваемый ресурс не существует";
            }
        }
        if (!error) {
            error = new ErrorResponse();
            error.errorMessage = "Произошла неизвестная ошибка";
        }

        this.messageService.error(error);
    }
}
