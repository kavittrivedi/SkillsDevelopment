import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { map } from 'rxjs';

export const adminGuard: CanActivateFn = (route, state) => {
  const userService = inject(UserService);
  const router = inject(Router); 
  return userService.getUserRole().pipe(
    map(role => {
      console.log(role.data);
      if(role.data == "Admin"){
        return true;
      }
      else{
        router.navigate(['/home']);
        return false;
      }
    })
  );
};
