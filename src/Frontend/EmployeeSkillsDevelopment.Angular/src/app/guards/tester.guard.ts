import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { map } from 'rxjs';
import { UserService } from '../services/user.service';

export const testerGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const router = inject(Router); 
  return userService.getUserRole().pipe(
    map(role => {
      console.log(role.data);
      if(role.data === "Tester" || role.data === "Admin"){
        console.log(role.data);
        console.log(role.data === "Tester" || role.data === "Admin");

        return true;
      }
      else{
        router.navigate(['/']);
        return false;
      }
    })
  );
};
