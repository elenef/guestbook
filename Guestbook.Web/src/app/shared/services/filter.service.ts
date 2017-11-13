import { Injectable } from "@angular/core";
import { Observable } from 'rxjs/Observable';


@Injectable()
export class FilterService {


    constructor(

    ) { }

    init(queryParams: Object, dataFilter: any) {
        for (var key in queryParams) {
                dataFilter[key] = queryParams[key];
        }
        return dataFilter;
    }

    onFiltration(dataFilter: Object, queryParams: any) {
        for (var key in dataFilter) {
            queryParams[key] = dataFilter[key];
        }
        return queryParams;
    }

}