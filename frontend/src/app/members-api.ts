import { HttpClient } from '@angular/common/http';
import { Service, inject } from '@angular/core';
import { Observable } from 'rxjs';

export type ApiMember = {
  id: number;
  name: string;
  email: string;
  nickname?: string | null;
};

@Service()
export class MembersApi {
  private readonly http = inject(HttpClient);
  list(token: string): Observable<ApiMember[]> {
    return this.http.get<ApiMember[]>('/api/members', {
      headers: { Authorization: `Bearer ${token}` },
    });
  }
}
