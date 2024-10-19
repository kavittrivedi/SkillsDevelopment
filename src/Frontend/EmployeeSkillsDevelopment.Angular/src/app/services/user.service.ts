import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { MsalService } from '@azure/msal-angular';
import { Observable, of } from 'rxjs';
import { ApiResponse } from '../models/ApiResponse{T}';

@Injectable({
  providedIn: 'root'
})
export class UserService {


  private apiUrl = `${environment.domain}User`; 
  constructor(private http: HttpClient, private msalService: MsalService) { }

  getUserData(): { username: string | undefined; objectId: string; email: string } | null {
    console.log("inside getUserData");
    const accounts = this.msalService.instance.getAllAccounts();
    if (accounts.length > 0) {
      const account = accounts[0];
      return {
        username: account.name,
        objectId: account.localAccountId,
        email: account.username
      };
    }
    return null;
  }
  
  sendUserDataToBackend(userData: { username: string | undefined; objectId: string; email: string }): void {
    console.log(userData);
    this.http.post(`${this.apiUrl}/AddUser`, userData).subscribe({
      next: () => console.log('User details sent successfully'),
      error: (error) => console.error('Error sending user details', error)
    });
  }

  sendUserDetailsToBackend(): void {
    console.log("inside User service");
    const userData = this.getUserData();
    if (userData) {
      this.sendUserDataToBackend(userData);
    } else {
      console.error('No user data found');
    }
  }

  // sendUserDetailsToBackend() {
  //   console.log("inside User service")
  //   const accounts = this.msalService.instance.getAllAccounts();
  //   if (accounts.length > 0) {
  //     const account = accounts[0];
  //     const userData = {
  //       username: account.name,
  //       objectId: account.localAccountId,
  //       email: account.username
  //     };
  //     console.log(userData)

  //     this.http.post(`${this.apiUrl}/AddUser`, userData).subscribe({
  //       next: () => console.log('User details sent successfully'),
  //       error: (error) => console.error('Error sending user details', error)
  //     });
  //   }
  // }

  getUserRole(): Observable<ApiResponse<string>> {
    const userData = this.getUserData();
    if(userData)
    {
    return this.http.get<ApiResponse<string>>(`${this.apiUrl}/GetUserRole?objectId=${userData?.objectId}`)
    }
    else
    {
      return of({
        success: false,
        data: '', // Default or empty value for data
        message: 'User data not found'
      });
    }
 
  }
}
