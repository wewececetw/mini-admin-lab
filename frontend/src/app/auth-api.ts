import { HttpClient } from '@angular/common/http';
import { Service, inject } from '@angular/core';
import { Observable } from 'rxjs';

export type LoginBody = { username: string; password: string };
export type LoginResult = { token: string };

@Service()
export class AuthApi {
  private readonly http = inject(HttpClient);

  login(body: LoginBody): Observable<LoginResult> {
    return this.http.post<LoginResult>('/api/auth/login', body);
  }
}
