import { ActivatedRouteSnapshot, CanDeactivateFn, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

export interface Candeactivatecomponent {

  canDeactivate: () => boolean | Observable<boolean> | Promise<boolean>;
}

export const candeactivatecomponentGuard: CanDeactivateFn<Candeactivatecomponent> = (component:Candeactivatecomponent, currentRoute:ActivatedRouteSnapshot, currentState:RouterStateSnapshot, nextState:RouterStateSnapshot) => {
  return component.canDeactivate ? component.canDeactivate() : true;
};


// Below code will be used in the component where we need to apply this guard. First we need to
// implement Candeactivatecomponent then the below code we need to write

// hasUnsavedChanges: boolean = true;

//   canDeactivate(): Observable<boolean> | Promise<boolean> | boolean {
//     if (this.hasUnsavedChanges) {
//       return confirm('You have unsaved changes. Do you really want to leave?');
//     }
//     return true;
//   }
