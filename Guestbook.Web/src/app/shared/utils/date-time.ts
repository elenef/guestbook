import * as moment from 'moment';
import 'moment-timezone';

export class DateTime {
    private static isoDateFormat = 'YYYY-MM-DD HH:mm:ss';
    private static localTimezone = 'Asia/Almaty';
    private static utcTimezone = 'UTC';
    private utcTimeUnix: number;

    constructor(unixUtcTime?: number) {
        this.utcTimeUnix = unixUtcTime
            ? this.utcTimeUnix = unixUtcTime
            : Math.round(new Date().getTime() / 1000);
    }



    static fromSeconds(seconds:number): Date {
        let zeroDate = new Date(1970, 0, 0);
        zeroDate.setSeconds(seconds || 0);
        return zeroDate;
    }

    static toSeconds(date:Date): number {
        let zeroDate = new Date(1970, 0, 0);
        return (date.getTime() - zeroDate.getTime()) / 1000;
    }

    /**Date in CET timezone */
    static fromLocalDate(date: Date): DateTime {
        let dateAsString = moment(date).format(DateTime.isoDateFormat);
        let unixTime = moment.tz(dateAsString, DateTime.localTimezone).toDate().getTime();
        return new DateTime(Math.round(unixTime / 1000));
    }

    static roundToDate(date: Date) {
        let local = DateTime.fromLocalDate(date).toLocalMoment();
        let today = local
            .subtract(local.hours(), 'hours')
            .subtract(local.minutes(), 'minutes')
            .subtract(local.seconds(), 'seconds')
            .subtract(local.milliseconds(), 'milliseconds');

        return today;
    }


    formatLocal(formatString: string) {
        return this.toLocalMoment().format(formatString);
    }

    toLocalMoment() {
        return moment(this.toUtcMoment()
        .tz(DateTime.localTimezone)
        .format(DateTime.isoDateFormat));
    }

    toUtcMoment() {
        return moment.tz(this.utcTimeUnix * 1000, DateTime.utcTimezone);
    }

    toLocalDate() {
        return this.toLocalMoment().toDate();
    }

    toUtcDate() {
        return this.toUtcMoment().toDate();
    }

    toUtcUnix() {
        return this.utcTimeUnix;
    }

    roundToUtcDateMoment() {
        let d = this.toLocalMoment();
        return d
            .subtract(d.hours())
            .subtract(d.minutes())
            .subtract(d.seconds())
            .subtract(d.milliseconds())
            .utc();
    }
    
}
