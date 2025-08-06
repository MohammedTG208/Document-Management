import { inject } from '@angular/core';
import { CanActivateFn, RedirectCommand, Router } from '@angular/router';
import { AuthService, getHeaders, isTokenExpired } from './Auth/authService';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const isLogin = authService.getUserInfo()?.jwt;
  const router = inject(Router);
  if (isLogin && !isTokenExpired()) {
    return true;
  } else {
   return new RedirectCommand(router.parseUrl('login'))
  }
};
