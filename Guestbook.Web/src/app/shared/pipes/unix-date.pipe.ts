import { Pipe, PipeTransform } from '@angular/core';
import { DateTime, WordDateUtils } from '../utils';

/*
* Convert unix-formatted datetime value to string
* Takes format argument that defaults to DD.MM.YYYY HH:mm
* Example:
* 1484562317 | unixdate
* returns:
* 16.01.2017 10:25 CET
*/
@Pipe({
    name: 'unixdate',
    pure: true
})
export class UnixDatePipe implements PipeTransform {
    transform(timestamp: number, format: string, note: boolean, daysAdd?: number) {
        if (!timestamp) {
            return null;
        }
        var f = format ? format : 'DD.MM.YY HH:mm';
        var n = note ? note : false;

        if(daysAdd){
            timestamp = this.getDateAfterAdd(timestamp, daysAdd);
        }
        if (note) {
            var diffDays = this.getDifferenceInDays(Math.round(+new Date() / 1000), timestamp);
            return new DateTime(timestamp).formatLocal(f) + " (" + WordDateUtils.getForm(diffDays) + ")";
        }
        else {
            return new DateTime(timestamp).formatLocal(f);
        }
    }

    getDifferenceInDays(timestamp1: number, timestamp2: number): number {
        var timeOneDay = 86400;
        var timeDiff = timestamp2 - timestamp1;
        if (timeDiff > timeOneDay) {
            var diffDays = Math.ceil(timeDiff / timeOneDay);
        }
        else if (timeDiff < -timeOneDay) {
            var diffDays = Math.floor(timeDiff / timeOneDay);
        }
        else {
            var diffDays = 0;
        }
        return diffDays;
    }

    getDateAfterAdd(timestamp1: number, timestamp2: number): number{
        return new DateTime(timestamp1).toUtcMoment().add(+timestamp2, 'day').valueOf() / 1000;
    }
}