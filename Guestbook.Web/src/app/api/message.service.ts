import { Injectable } from "@angular/core";
import { MdSnackBar, MdSnackBarConfig } from '@angular/material';

@Injectable()
export class MessageService {
    private duration: number = 7000;

    constructor(
        private snackBar: MdSnackBar
    ) {
    }

    error(error: any, title?: string) {
        title = title == null ? 'Ошибка' : title;
        let message = '';

        if (error) {
            message = error.errorMessage != null ? error.errorMessage : error;
            message = (message == null ? '' : message) + ' ';

            let details = error.errorDetails;
            if (details) {
                for (var key in <any>details) {
                    let values = '';
                    details[key].forEach((d: any) => values += d + ' ');
                    message = values != null && values != '' ? message + values : message;
                }
            }
        } else {
            message = 'Произошла ошибка';
        }

        let options = new MdSnackBarConfig();
        options.duration = this.duration;
        options.extraClasses = ["snackbar-error"]
        this.snackBar.open(message, 'Закрыть', options);
    }

    info(message: string) {
        let options = new MdSnackBarConfig();
        options.duration = this.duration;
        options.extraClasses = ["snackbar-info"]
        this.snackBar.open(message, 'Закрыть', options);
    }

    success(message: string) {
        let options = new MdSnackBarConfig();
        options.duration = this.duration;
        options.extraClasses = ["snackbar-success"]
        this.snackBar.open(message, 'Закрыть', options);
    }
}
