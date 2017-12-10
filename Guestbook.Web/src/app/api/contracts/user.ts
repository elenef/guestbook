export class User {
    id: string;
    name: string;
    login: string;
    email: string;
    phone: string;
    password: string;

    constructor(data?: any) {
        if (data) {
            for (var i in data) { this[i] = data[i] }
        }
    }

    serialize() {
        return {
            name: this.name,
            login: this.login,
            phone: this.phone,
            email: this.email,
            password: this.password,
        }
    }


}