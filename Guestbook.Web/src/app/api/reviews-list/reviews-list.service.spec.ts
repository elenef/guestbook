import { TestBed, inject } from '@angular/core/testing';

import { ReviewsListService } from './reviews-list.service';

describe('ReviewsListService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ReviewsListService]
    });
  });

  it('should be created', inject([ReviewsListService], (service: ReviewsListService) => {
    expect(service).toBeTruthy();
  }));
});
