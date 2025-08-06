import { ApplicationConfig } from '@angular/core';
import { provideRouter, withComponentInputBinding, withRouterConfig } from '@angular/router';
import { routes } from '../app/app.routers';
import { provideHttpClient } from '@angular/common/http';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes,withRouterConfig({
      paramsInheritanceStrategy:'always',
    }), withComponentInputBinding()
  ),
    provideHttpClient()
  ]
};
