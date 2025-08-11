import { inject } from '@angular/core';
import { CanActivateFn, RedirectCommand, Router } from '@angular/router';
import { AuthService } from './Auth/authService';
import { authGuard } from './auth.guard';

export const authadminGuard: CanActivateFn = (route, state) => {
 const authService = inject(AuthService);
 const isAdmin = authService.getUserInfo()?.role?.includes('Admin');
 const router = inject(Router);
  // Check if the user is an admin
  if (isAdmin) {
    // If the user is an admin, proceed with the authGuard
    // This will check if the token is valid and not expired
    if(authGuard(route, state)) {
      return true;
    }else {
      return new RedirectCommand(router.parseUrl('login'));
    }
  } else {
    return new RedirectCommand(router.parseUrl('unauthorized'));
  }
};
