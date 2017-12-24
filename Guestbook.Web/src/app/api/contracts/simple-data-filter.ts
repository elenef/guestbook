export class SimpleDataFilter {
    orderBy: string;
    orderDesc: boolean;
    page: number;
    pageSize: number;
    search: string;
    dateTo: number;
    dateFrom: number;
    buyerName: string;
    invoiceAmountUp: number;
    invoiceAmountDown: number;
    financingPeriodDays: number;

    constructor(data?: any) {
        if (data) {
            for (var i in data) { this[i] = data[i] }
        }
    }
}