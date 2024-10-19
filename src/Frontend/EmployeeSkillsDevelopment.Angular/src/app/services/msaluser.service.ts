import { Injectable } from '@angular/core';
import { MsalService } from '@azure/msal-angular';
import { PublicClientApplication, AuthenticationResult } from '@azure/msal-browser';
import { Observable, of } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class MsalUserService {
  private accessToken: string | null = null;

  constructor(private msalService: MsalService) {}

  public getAccessToken(): Observable<string | null> {
    console.log('Checking localStorage availability');
    if (typeof window !== 'undefined' && typeof localStorage !== 'undefined') {
      console.log('localStorage is available');
      if (localStorage.getItem('msal.idtoken') !== undefined && localStorage.getItem('msal.idtoken') != null) {
        this.accessToken = localStorage.getItem('msal.idtoken');
      }
    } else {
      console.log('localStorage is not available');
    }
    return of(this.accessToken);
  }
  
  public async getCurrentUserInfo() {
    try {
      const accounts = this.msalService.instance.getAllAccounts();
      if (accounts.length > 0) {
        const account = accounts[0];
        alert(account.name);
      } else {
        alert('No user is signed in');
      }
    } catch (error) {
      console.error('Error retrieving user info', error);
    }
  }

 
}
