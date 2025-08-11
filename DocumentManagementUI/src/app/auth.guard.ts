import { inject } from '@angular/core';
import { CanActivateFn, RedirectCommand, Router } from '@angular/router';
import { AuthService, deleteToken, isTokenExpired } from './Auth/authService';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const jwt = authService.getUserInfo()?.jwt;
  const router = inject(Router);
  if (jwt && !isTokenExpired(jwt!)) {
    return true;
  } else {
    deleteToken();
   return new RedirectCommand(router.parseUrl('login'));
  }
};
