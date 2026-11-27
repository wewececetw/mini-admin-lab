import { Service, computed, signal } from '@angular/core';

@Service()
export class AuthSession {
  readonly token = signal<string | null>(null);
  readonly isLoggedIn = computed(() => this.token() !== null);

  setToken(token: string): void {
    this.token.set(token);
  }

  clear(): void {
    this.token.set(null);
  }
}
