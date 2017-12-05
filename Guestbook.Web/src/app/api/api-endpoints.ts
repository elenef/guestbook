﻿import { ApiEndpoint } from './api-endpoint';
import { ApiEndpointBase } from './base-api-endpoint';
import { Config } from './config';

export class ApiEndpoints {
    static get BaseApiUrl() {
        return  Config.apiUrl;
    }

    static reviews(): ApiEndpoint {
        return new ApiEndpointBase(ApiEndpoints.BaseApiUrl + '/reviews');
    }


}
