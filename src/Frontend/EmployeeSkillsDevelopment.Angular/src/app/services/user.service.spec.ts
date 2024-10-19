import { TestBed } from '@angular/core/testing';

import { UserService } from './user.service';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MsalService } from '@azure/msal-angular';
import { AzureAdDemoService } from './azure-ad-demo.service';

describe('UserService', () => {
  let service: UserService;
  let authServiceSpy: jasmine.SpyObj<MsalService>;
  let azureAdDemoServiceSpy:jasmine.SpyObj<AzureAdDemoService>

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideHttpClientTesting(),provideHttpClient(),
        { provide: MsalService, useValue: authServiceSpy },
        {provide :AzureAdDemoService,useValue:azureAdDemoServiceSpy}
    ]
    });
    service = TestBed.inject(UserService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
