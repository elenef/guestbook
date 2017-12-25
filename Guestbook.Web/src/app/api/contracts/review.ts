export class Review {
    id: string;
    created: number;
    comment: string;
    restaurantId: string;
    userName: string;
    email: string;
    reviewRating: number;
    like: number;


    constructor(data?: any) {
        if (data) {
            for (var i in data) {
                this[i] = data[i];
            }
        }
    }

    serialize() {
        return {
            restaurantId: this.restaurantId,
            comment: this.comment,
            email: this.email,
            userName: this.userName,
            reviewRating: this.reviewRating,
            like: this.like,
        }
    }
}
