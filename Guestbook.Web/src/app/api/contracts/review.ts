export class Review {
    id: string;
    created: number;
    comment: string;
    restaurantId: string;
    userId: string;

    constructor(data?: any) {
        if (data) {
            for (var i in data) {
                this[i] = data[i];
            }
        }
    }
}
