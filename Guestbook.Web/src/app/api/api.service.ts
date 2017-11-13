import { Observable } from 'rxjs';
import { Injectable } from '@angular/core'
import { Response } from "@angular/http";

@Injectable()
export abstract class ApiService {
    abstract get<TContract>(url: string): Observable<TContract>;
    abstract post<TContract>(url: string, item: TContract): Observable<TContract>;
    abstract put<TContract>(url: string, item: TContract): Observable<TContract>;
    abstract delete(url: string);
}