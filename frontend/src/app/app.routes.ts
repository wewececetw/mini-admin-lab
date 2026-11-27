import { Routes } from '@angular/router';
import { authGuard } from './auth.guard';
import { LoginPage } from './login-page/login-page';
import { MembersPage } from './members-page/members-page';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: 'login', component: LoginPage },
  { path: 'members', component: MembersPage, canActivate: [authGuard] },
];
