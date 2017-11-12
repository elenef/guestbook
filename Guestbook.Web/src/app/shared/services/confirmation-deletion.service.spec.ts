import { TestBed, inject } from '@angular/core/testing';

import { ConfirmationDeletionService } from './confirmation-deletion.service';

describe('ConfirmationDeletionService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ConfirmationDeletionService]
    });
  });

  it('should be created', inject([ConfirmationDeletionService], (service: ConfirmationDeletionService) => {
    expect(service).toBeTruthy();
  }));
});
