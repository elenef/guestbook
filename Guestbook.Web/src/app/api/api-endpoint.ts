export interface ApiEndpoint {
    listUrl(parameters?: any): string;
    itemUrl(id: string, parameters?: any): string;
}