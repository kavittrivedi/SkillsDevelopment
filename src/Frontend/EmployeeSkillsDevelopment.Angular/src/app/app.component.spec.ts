import { TestBed } from '@angular/core/testing';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MSAL_GUARD_CONFIG, MsalBroadcastService, MsalGuardConfiguration, MsalService } from '@azure/msal-angular';
import { AzureAdDemoService } from './services/azure-ad-demo.service';

let authServiceSpy: jasmine.SpyObj<MsalService>;
let azureAdDemoServiceSpy:jasmine.SpyObj<AzureAdDemoService>;
let MsalGuardConfigurationSpy:jasmine.SpyObj<MsalGuardConfiguration>;
let MsalBroadcastServiceSpy:jasmine.SpyObj<MsalBroadcastService>;

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterModule.forRoot([])
      ],
      declarations: [
        AppComponent
      ],
      providers:[provideHttpClient(),provideHttpClientTesting(),
        { provide: MsalService, useValue: authServiceSpy },
        {provide :AzureAdDemoService,useValue:azureAdDemoServiceSpy},
        {provide :MSAL_GUARD_CONFIG,useValue:MsalGuardConfigurationSpy},
        {provide :MsalBroadcastService,useValue:MsalBroadcastServiceSpy},
      ]
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  // it(`should have as title 'EmployeeSkillsDevelopment.Angular'`, () => {
  //   const fixture = TestBed.createComponent(AppComponent);
  //   const app = fixture.componentInstance;
  //   expect(app.title).toEqual('EmployeeSkillsDevelopment.Angular');
  // });

  it('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    //fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.container-fluid .navbar-brand')?.textContent).toContain('Employee Skills Development');
  });
});
