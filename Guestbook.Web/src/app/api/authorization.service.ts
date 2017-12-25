import { Observable } from 'rxjs/Rx';
import { Subscriber } from 'rxjs/Subscriber';
import { Injectable, Inject, forwardRef } from "@angular/core";
import { Http, Response, RequestOptionsArgs, Headers, RequestOptions } from "@angular/http";
import { Router } from '@angular/router'
import { MessageService } from "../shared/services"
import { Token } from "./contracts/token"
import { TokenRequest } from "./contracts/token-request"
import { ApiEndpoints } from "./api-endpoints"
import { ErrorResponse } from "./contracts/error-response"
import { UserProfile } from "./contracts/user-profile"
import { Config } from './config'
import { ApiService } from './';
import { ListItem } from '../shared/models';



@Injectable()
export class AuthorizationService {
    private static tokenKey = "api-token";
    protected _user: UserProfile;
    private requestAuthorization: boolean;
    private HTTP_NOT_FOUND = 404;
    private userRole: string;





    get authorizationRequired() {
        return this.requestAuthorization;
    }

    get userProfile() {
        return this._user;
    }

    get userProfileRole() {
        return this.userRole;
    }


    /**Navigate to the url after authorization */
    redirectUrl: string = "/reviews";

    constructor(
        private http: Http,
        private messageService: MessageService,
        private router: Router,
    ) {
        let token = AuthorizationService.getToken();
        this.requestAuthorization = token == null ? true : false;


        this._user = new UserProfile();
    }

    setAuthorizationRequired() {
        this.requestAuthorization = true;
        this.router.navigate(["/login"]);
    }

    /*authorize(username: string, password: string){
        this.requestAuthorization = false;
        this.router.navigate(["/reviews"]);
    }*/

    put<TContract>(url: string, item: TContract): Observable<TContract> {
        let body = JSON.stringify(item);
        let headers = new Headers({
            'Content-Type': 'application/json',
            'Authorization': AuthorizationService.getAuthorizationHeader()
        });
        let options = new RequestOptions({ headers: headers });

        return this.http.put(url, body, options)
            .map(res => <TContract>res.json());
    }

    private handleError(response: Response) {
        this.showErrorMessage(response);
    }


    private showErrorMessage(response: Response) {
        let error = null;
        if (response != null) {
            if (response.json) {
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

    authorize(username: string, password: string): Observable<void> {
        let request = new TokenRequest();
        request.grant_type = "password";
        request.scope = "admin_area";
        request.username = username;
        request.password = password;
        request.client_id = Config.clientId;
        request.client_secret = Config.clientSecret;

        let body = this.serialize(request);
        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let options = new RequestOptions({ headers: headers });
        let url = ApiEndpoints.token();

        let ctx = this;
        return new Observable<void>((observable: any) => {
            ctx.http.post(url, body, options)
                .map(res => <Token>res.json())
                .subscribe(t => {
                    t.issued = (new Date()).getTime();
                    localStorage[AuthorizationService.tokenKey] = JSON.stringify(t);
                    ctx.fillUserProfile()
                        .subscribe(() => {
                            this.requestAuthorization = false;
                            observable.next();
                            observable.complete();
                        }, error => {
                            observable.error(null);
                            observable.complete();
                        });

                },
                error => {
                    ctx.messageService.error("Wrong login or password");
                    observable.error(<ErrorResponse>error.json());
                    observable.complete();
                });
        });
    }

    static getToken(): Token {
        try {
            return <Token>JSON.parse(localStorage[AuthorizationService.tokenKey]);
        } catch (ex) {
            //do nothing
        }
        return null;
    }

    static getAuthorizationHeader(): string {
        var token = this.getToken();
        if (token == null) {
            return null;
        }
        return token.token_type + ' ' + token.access_token;
    }

    fillUserProfile(): Observable<void> {
        let ctx = this;

        var url = ApiEndpoints.profile();
        var options = this.getDefaultOptions();
        if (options == null) {
            return;
        }

        return new Observable<void>((observable: any) => {
            ctx.http.get(url, options).subscribe(resp => {
                let data = resp.json();
                this._user = new UserProfile(data);
                this.userRole = this._user.role;
                observable.next();
                observable.complete();
            }, error => {
                ctx.messageService.error("User is not found");
                observable.error(null);
                observable.complete();
            });
        });
    }

    private getDefaultOptions() {
        var token = AuthorizationService.getToken();
        if (token == null) {
            return null;
        }
        let headers = new Headers({
            'Content-Type': 'application/json',
            'Authorization': token.token_type + ' ' + token.access_token
        });
        return new RequestOptions({ headers: headers });
    }

    private serialize(data: any) {
        var str = [];
        for (var p in data)
            if (data.hasOwnProperty(p)) {
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(data[p]));
            }
        return str.join("&");
    }

    logout() {
        localStorage.removeItem(AuthorizationService.tokenKey);
        this.requestAuthorization = true;
    }

}
