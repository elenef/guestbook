export class User {
    id: string;
    name: string;
    login: string;
    email: string;
    phones: string;
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
            phones: this.phones,
            email: this.email,
            password: this.password,
        }
    }


}