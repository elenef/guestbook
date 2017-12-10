export class Review {
    id: string;
    created: number;
    comment: string;
    restaurantId: string;
    userId: string;
    restaurantName: string;
    userName: string;
    like: number;

    constructor(data: any) {
        if (data) {
            for (var i in data) {
                this[i] = data[i];
            }
        }
    }
}
