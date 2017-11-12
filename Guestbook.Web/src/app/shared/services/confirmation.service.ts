import { Injectable } from "@angular/core";
import { MdSnackBar, MdSnackBarConfig, MdSnackBarRef } from '@angular/material';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class ConfirmationService {
    private duration: number = 10000;

    constructor(
        private snackBar: MdSnackBar,
    ) {
    }

    confirmation(message: string): MdSnackBarRef<any> {
        let options = new MdSnackBarConfig();
        options.duration = this.duration;
        options.extraClasses = ["snackbar-confirmation"]
        this.snackBar.open(message, 'Да', options);
        return this.snackBar._openedSnackBarRef;
    }

}
