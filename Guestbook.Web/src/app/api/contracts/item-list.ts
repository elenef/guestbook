export class ItemList<T> {
    /**Page items */
    data: Array<T>;
    /**Requested page nubmer */
    page: number;
    /**Requested page size */
    pageSize: number;
    /**Total number of items */
    total: number;

    constructor(data: any) {
        if (data) {
            for (var i in data) {
                this[i] = data[i];
            }
        }
    }
}