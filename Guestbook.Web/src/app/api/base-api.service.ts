import { Observable } from 'rxjs/Observable';


import { Http, Response, RequestOptionsArgs, Headers, RequestOptions } from '@angular/http';
import { Injectable } from '@angular/core';

import { ApiService } from './api.service';
import { AuthorizationService } from "./authorization.service";

@Injectable()
export class BaseApiService implements ApiService {
    constructor(
        private http: Http
    ) { }

    loadInProgress: boolean;
    errorMessage: string;

    get<TContract>(url: string, options?: RequestOptionsArgs): Observable<TContract> {
        this.loadInProgress = true;
        options = options == null ? new RequestOptions() : options;
        let headers = this.getHeaders();
        options.headers = headers;
        return this.http.get(url, options)
            .map(this.toJson)
            .do(res => {this.loadInProgress = false;
                console.log(this.loadInProgress); })
            .catch(error => this.handleError(error, this));
    }

    post<TContract>(url: string, item: TContract): Observable<TContract> {
        this.loadInProgress = true;
        let body = JSON.stringify(item);
        let headers = this.getHeaders();
        let options = new RequestOptions({ headers: headers });

        return this.http.post(url, body, options)
            .map(this.toJson)
            .do(res => this.loadInProgress = false)
            .catch(error => this.handleError(error, this));
    }

    put<TContract>(url: string, item: TContract): Observable<TContract> {
        this.loadInProgress = true;
        let body = JSON.stringify(item);
        let headers = this.getHeaders();
        let options = new RequestOptions({ headers: headers });

        return this.http.put(url, body, options)
            .map(this.toJson)
            .do(res => this.loadInProgress = false)
            .catch(error => this.handleError(error, this));
    }

    delete(url: string) {
        let headers = this.getHeaders();
        let options = new RequestOptions({ headers: headers });
        return this.http
            .delete(url, options)
            .catch(error => this.handleError(error, this));
    }

    private getHeaders(): Headers {
        let headers = new Headers({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Methods': 'GET, POST, PUT, OPTIONS, DETELE',
            'Access-Control-Allow-Headers': '*',
            'Authorization': AuthorizationService.getAuthorizationHeader()
        });
        return headers;
    }

    private handleError(error: Response, ctx: BaseApiService) {
        this.loadInProgress = false;
        if (error.status == 401) {// Unauthorized
            //
        }
        return Observable.throw(error);
    }

    private toJson<TContract>(response: Response) {
        if (!response.text()) {
            return null;
        } else {
            return <TContract>response.json();
        }
    }
}
