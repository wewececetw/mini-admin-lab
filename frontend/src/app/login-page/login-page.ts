import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthApi } from '../auth-api';
import { AuthSession } from '../auth-session';

@Component({
  selector: 'app-login-page',
  templateUrl: 'login-page.html',
})
export class LoginPage {
  private readonly auth = inject(AuthApi);
  private readonly session = inject(AuthSession);
  private readonly router = inject(Router);

  login(): void {
    this.auth.login({ username: 'admin', password: 'secret' }).subscribe((res) => {
      this.session.setToken(res.token);
      this.router.navigateByUrl('/members');
    });
  }
}
