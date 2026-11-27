import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthSession } from './auth-session';

export const authGuard: CanActivateFn = () => {
  const session = inject(AuthSession);
  const router = inject(Router);

  if (session.isLoggedIn()) {
    return true;
  }

  return router.createUrlTree(['/login']);
};
