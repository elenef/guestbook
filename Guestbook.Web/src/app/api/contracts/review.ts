export class Review {
    id: string;
    created: number;
    comment: string;
    restaurantId: string;
    userName: string;
    email: string;
    ratingRestaurant: number;


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
            ratingRestaurant: this.ratingRestaurant,
        }
    }
}
