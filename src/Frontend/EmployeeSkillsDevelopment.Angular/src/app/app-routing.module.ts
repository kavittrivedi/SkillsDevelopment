import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';
import { HomeComponent } from './components/home/home.component';
import { EmployeeListComponent } from './components/employees/employee-list/employee-list.component';
import { DevelopermoduleComponent } from './components/developermodule/developermodule.component';
import { TestermoduleComponent } from './components/testermodule/testermodule.component';
import { developerGuard } from './guards/developer.guard';
import { testerGuard } from './guards/tester.guard';
import { adminGuard } from './guards/admin.guard';

const routes: Routes = [
 
  {path:'',redirectTo:'employee-list',pathMatch:'full'},
  {path:'home',component:HomeComponent},
  {path:'employee-list',component:EmployeeListComponent,canActivate:[adminGuard, MsalGuard]},
  {path:'developermodule',component:DevelopermoduleComponent,canActivate:[developerGuard, MsalGuard]},
  {path:'testermodule',component:TestermoduleComponent,canActivate:[testerGuard, MsalGuard]}

];

@NgModule({
  imports: [RouterModule.forRoot(routes,
    {
      initialNavigation:'enabledBlocking'
    }
  )],
  exports: [RouterModule]
})
export class AppRoutingModule { }
