import { Component, Input, OnInit } from '@angular/core';
import { AzureAdDemoService } from '../../services/azure-ad-demo.service';
import { MsalService } from '@azure/msal-angular';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  template:`<p>{{data}}</p>`
})
export class HomeComponent implements OnInit {
  @Input() data:string| undefined;
  username : string | undefined = '';
 // isUserLoggedIn:boolean = false;
  constructor( private azureAdDemoService : AzureAdDemoService, private authService : MsalService,private userService: UserService){} 
  ngOnInit(): void {
    this.username = this.userService.getUserData()?.username
  }
  
  login() {
    console.log('Attempting login...');
    this.authService.initialize().subscribe(() => {
      this.authService.loginPopup();
    });
  }

  isLoggedIn(): boolean {
    return this.authService.instance.getAllAccounts().length > 0;
  }

  
 

}
