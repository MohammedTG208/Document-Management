import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './Auth/authService';

export const authadminGuard: CanActivateFn = (route, state) => {
 const authService = inject(AuthService);
 const isAdmin = authService.getUserInfo()?.role?.includes('Admin');
 const router = inject(Router);
  if (isAdmin) {
    return true;
  } else {
    return router.parseUrl('/unauthorized');
  }
};
