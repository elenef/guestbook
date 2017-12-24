export class UserProfile {
    id: string;
    role: string;
    value: object;
    
    constructor(data?: any) {
        if(data) {
            for(var i in data){this[i] = data[i]}
        }
    }

    serialize() {
        return {
            id: this.id,
            role: this.role,
            value: this.value,
        };
    }
}
