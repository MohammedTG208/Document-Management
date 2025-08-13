import { inject } from '@angular/core';
import { CanActivateFn, RedirectCommand, Router } from '@angular/router';
import { AuthService } from './Auth/authService';
import { authGuard } from './auth.guard';

export const authadminGuard: CanActivateFn = (route, state) => {
 const authService = inject(AuthService);
 const isAdmin = authService.getUserInfo()?.role?.includes('Admin') || false;
 const router = inject(Router);
  
  if (isAdmin) {
    if(authGuard(route, state)) {
      return true;
    }else {
      return new RedirectCommand(router.parseUrl('login'));
    }
  } else {
    return new RedirectCommand(router.parseUrl('unauthorized'));
  }
};
