import { TestBed } from '@angular/core/testing';
import { CanDeactivateFn } from '@angular/router';

import { candeactivatecomponentGuard } from './candeactivatecomponent.guard';

describe('candeactivatecomponentGuard', () => {
  const executeGuard: CanDeactivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => candeactivatecomponentGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
