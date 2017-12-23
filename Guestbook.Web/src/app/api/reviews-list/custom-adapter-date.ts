import * as moment from 'moment';
import { NativeDateAdapter } from '@angular/material';

export class CustomDateAdapter extends NativeDateAdapter {
    private DATE_FORMAT: string = 'DD.MM.YYYY';
    dateLenghtinDateFormat: number = 59;
    dateLenghtinStringFormat: number = 10;

    parse(value: Date): Date | null {
        if (value) {
            console.log(value.toString().length);
            if (value.toString().length === this.dateLenghtinStringFormat
                || value.toString().length === this.dateLenghtinDateFormat) {
                let m = moment(value, this.DATE_FORMAT);
                if (m.isValid()) {
                    return m.toDate();
                }
            } else { return null; }
        } else { return value; }
    }

    format(date: Date, displayFormat: Object): string {
        let m = moment(date);
        return m.format(this.DATE_FORMAT);
    }
}