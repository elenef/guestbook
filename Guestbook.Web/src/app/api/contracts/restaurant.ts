import { Review } from "./review";

export class Restaurant {
    id: string;
    reviews: Review[];
    name: string;
    description: string;
    address: string;

    constructor(data?: any) {
        if (data) {
            for (var i in data) { this[i] = data[i] }
        }
    }


}
