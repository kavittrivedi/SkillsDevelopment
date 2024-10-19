import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration } from '@angular/platform-browser';
import{MsalGuard, MsalInterceptor, MsalModule, MsalRedirectComponent} from '@azure/msal-angular';
import{InteractionType, PublicClientApplication} from '@azure/msal-browser';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS,  provideHttpClient, withFetch, withInterceptorsFromDi } from '@angular/common/http';
import { HomeComponent } from './components/home/home.component';
import { EmployeeListComponent } from './components/employees/employee-list/employee-list.component';
import { NavbarComponent } from './components/shared/navbar/navbar.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { DevelopermoduleComponent } from './components/developermodule/developermodule.component';
import { TestermoduleComponent } from './components/testermodule/testermodule.component';
import { environment } from '../environments/environment.development';

export const protectedResourceMap: any = [
  [environment.baseUrl, environment.scopeUri],
];

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    EmployeeListComponent,
    NavbarComponent,
    FooterComponent,
    DevelopermoduleComponent,
    TestermoduleComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
  
   
    MsalModule.forRoot(new PublicClientApplication
      (
        {
          auth:{
            clientId:environment.uiClientId,
            redirectUri:environment.redirectUrl,
            authority:environment.authority,
          },
          cache:{
            cacheLocation:'localStorage',
            storeAuthStateInCookie:false
          }
        }
      ),
      {
        interactionType:InteractionType.Popup,
        authRequest:{
          scopes:environment.scopeUri
        }
      },
      {
        interactionType:InteractionType.Popup,
        protectedResourceMap:new Map(
          [
            [environment.baseUrl, environment.scopeUri],
          ]
        )
      }
    )
  ],
  providers: [{
    provide:HTTP_INTERCEPTORS,
    useClass:MsalInterceptor,
    multi:true,
  },MsalGuard,
  provideHttpClient(withFetch(),withInterceptorsFromDi()),
    provideClientHydration()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
