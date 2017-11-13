import { ApiEndpoint } from "./api-endpoint"

export class ApiEndpointBase implements ApiEndpoint {
    constructor(
        private url: string
    ) { }

    listUrl(parameters?: any): string {
        let queryString = this.getQueryString(parameters);
        return this.url + queryString;
    }

    itemUrl(id: string, parameters?: any): string {
        let queryString = this.getQueryString(parameters);
        return this.url + '/' + id + queryString;
    }

    private getQueryString(parameters?: any) {
        let queryString = '';
        for (let parameter in parameters) {
            let value = parameters[parameter];
            if (value) {
                queryString += queryString ? '&' : '?';
                queryString += parameter + '=' + value;
            }
        }

        return queryString;
    }
}