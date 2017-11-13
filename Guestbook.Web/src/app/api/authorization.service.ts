import { Observable } from 'rxjs/Rx';
import { Injectable} from "@angular/core";
import { Http, Response} from "@angular/http";
import { Router } from '@angular/router';



@Injectable()
export class AuthorizationService {

    private requestAuthorization: boolean;



    get authorizationRequired() {
        return this.requestAuthorization;
    }

    /**Navigate to the url after authorization */
    redirectUrl: string;

    constructor(
        private http: Http,
        private router: Router,
    ) {
    }


    setAuthorizationRequired() {
        this.requestAuthorization = true;
        this.router.navigate(["/login"]);
    }

    authorize(username: string, password: string): Observable<void> {
        return new Observable<void>((observable: any) => {
        });
    }

}
