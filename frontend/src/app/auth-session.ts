import { Service, computed, signal } from '@angular/core';

@Service()
export class AuthSession {
  readonly token = signal<string | null>(null);
  readonly isLoggedIn = computed(() => this.token() !== null);

  constructor() {
    const saved = localStorage.getItem('miniadmin.token');
    if (saved) {
      this.token.set(saved);
    }
  }

  setToken(token: string): void {
    this.token.set(token);
    localStorage.setItem('miniadmin.token', token);
  }

  clear(): void {
    this.token.set(null);
    localStorage.removeItem('miniadmin.token');
  }
}
