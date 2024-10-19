import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MSAL_GUARD_CONFIG, MsalGuardConfiguration, MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { InteractionStatus } from '@azure/msal-browser';
import { Subject, filter, takeUntil } from 'rxjs';
import { AzureAdDemoService } from '../../../services/azure-ad-demo.service';
import { UserService } from '../../../services/user.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit,OnDestroy {
  isUsserLoggedIn:boolean = false;
  username : string | undefined = '';
  private readonly _destroy = new Subject<void>();
  constructor(@Inject(MSAL_GUARD_CONFIG) private msalGuardConfig:MsalGuardConfiguration,
private msalBroadcastService:MsalBroadcastService,
private authService : MsalService, private azureadDemoService: AzureAdDemoService,
private userService: UserService
){}


  ngOnInit(): void {
    this.msalBroadcastService.inProgress$.pipe
    (filter((interactionStatus:InteractionStatus)=>
    interactionStatus==InteractionStatus.None),
  takeUntil(this._destroy))
    .subscribe(x=>{
     
      this.isUsserLoggedIn=this.isLoggedIn()
      this.azureadDemoService.isUserLoggedIn.next(this.isUsserLoggedIn);
      if (this.isUsserLoggedIn) {
        this.userService.sendUserDetailsToBackend();
        this.username = this.userService.getUserData()?.username
        console.log("fetching username in app component");
        console.log(this.username);

      }
    })
  }


  ngOnDestroy(): void {
   this._destroy.next(undefined);
   this._destroy.complete();
  }
  
  logout() {
    this.authService.logoutPopup({
        mainWindowRedirectUri: "/"
    });
  }


  isLoggedIn(): boolean {
    return this.authService.instance.getAllAccounts().length > 0;
  }
}

