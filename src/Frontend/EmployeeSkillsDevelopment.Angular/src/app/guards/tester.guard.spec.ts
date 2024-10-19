import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { testerGuard } from './tester.guard';

describe('testerGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => testerGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
