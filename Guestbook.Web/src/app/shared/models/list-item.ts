/**
 * Represents list item in select list
 */
export class ListItem {
    id: string;
    name: string;

    constructor(data?: {id: string, name: string}) {
        if(data) {
            this.id = data.id;
            this.name = data.name;
        }
    }
}