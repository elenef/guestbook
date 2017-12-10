export class Restaurant {
    id: string;
    name: string;
    description: string;
    address: string;
    reviewIds: string[];

    constructor(data: any) {
        if (data) {
            for (var i in data) {
                this[i] = data[i];
            }
        }
    }
}
