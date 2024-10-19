import { TestBed } from '@angular/core/testing';
import { MsalUserService } from './msaluser.service';

describe('MsaluserService', () => {
  let service: MsalUserService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MsalUserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
