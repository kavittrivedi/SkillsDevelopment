import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { inject } from '@angular/core';
import { map } from 'rxjs';

export const developerGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const router = inject(Router); 
  return userService.getUserRole().pipe(
    map(role => {
      console.log(role.data);
      if(role.data === "Developer" || role.data === "Admin"){
        console.log(role.data);
        console.log(role.data === "Developer" || role.data === "Admin");
        return true;
      }
    
      else{
        router.navigate(['/']);
        return false;
      }
    })
  );
};
