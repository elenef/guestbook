import {  ɵx } from '@angular/material';


export class CustomPaginator extends ɵx {
  /** A label for the page size selector. */
  itemsPerPageLabel = 'Показывать:';

  /** A label for the button that increments the current page. */
  nextPageLabel = 'Следующая страница';

  /** A label for the button that decrements the current page. */
  previousPageLabel = 'Предыдущая страница';

  /** A label for the range of items within the current page and the length of the whole list. */
  getRangeLabel = (page: number, pageSize: number, length: number) => {
    if (length == 0 || pageSize == 0) { return `0 из ${length}`; }

    length = Math.max(length, 0);

    const startIndex = page * pageSize;

    // If the start index exceeds the list length, do not try and fix the end index to the end.
    const endIndex = startIndex < length ?
        Math.min(startIndex + pageSize, length) :
        startIndex + pageSize;

    return `${startIndex + 1} - ${endIndex} из ${length}`;
  }
}