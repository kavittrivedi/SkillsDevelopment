import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeComponent } from './home.component';
import { AzureAdDemoService } from '../../services/azure-ad-demo.service';
import { MsalService } from '@azure/msal-angular';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let fixture: ComponentFixture<HomeComponent>;
  let authServiceSpy: jasmine.SpyObj<MsalService>;
  let azureAdDemoServiceSpy:jasmine.SpyObj<AzureAdDemoService>

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HomeComponent], 
      providers: [
        { provide: MsalService, useValue: authServiceSpy },
        {provide :AzureAdDemoService,useValue:azureAdDemoServiceSpy}
      ] 
        
    })
    .compileComponents();

    fixture = TestBed.createComponent(HomeComponent);
    component = fixture.componentInstance;
    //fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
