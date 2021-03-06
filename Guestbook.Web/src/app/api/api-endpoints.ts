﻿import { ApiEndpoint } from './api-endpoint';
import { ApiEndpointBase } from './base-api-endpoint';
import { Config } from './config';

export class ApiEndpoints {
    static get BaseApiUrl() {
        return  Config.apiUrl;
    }

    static token() {
        return ApiEndpoints.BaseApiUrl + '/connect/token';
    }

    static profile() {
        return ApiEndpoints.BaseApiUrl + '/profiles'; 
    }

    static users(): ApiEndpoint {
        return new ApiEndpointBase(ApiEndpoints.BaseApiUrl + '/users');
    }

    static reviews(): ApiEndpoint {
        return new ApiEndpointBase(ApiEndpoints.BaseApiUrl + '/reviews');
    }

    static restaurants(): ApiEndpoint {
        return new ApiEndpointBase(ApiEndpoints.BaseApiUrl + '/restaurants');
    }

}
