import { Pipe, PipeTransform } from '@angular/core';
import { DateTime, WordDateUtils } from '../utils';

/*
* text | shortcomment
*/
@Pipe({
    name: 'shortcomment',
    pure: true
})
export class ShortCommentPipe implements PipeTransform {
    maxLength = 100;
    transform(comment: string) {
        const length = comment.length;
        if (length > this.maxLength) {
            comment = comment.substring(0, this.maxLength) + '...';
        }
        return comment;
    }


}