import { TestBed } from '@angular/core/testing';
import { AuthSession } from './auth-session';

describe('AuthSession', () => {
  let session: AuthSession;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    session = TestBed.inject(AuthSession);
  });

  it('should start logged out with null token', () => {
    expect(session.token()).toBeNull();
    expect(session.isLoggedIn()).toBe(false);
  });

  it('should store token and report logged in after setToken', () => {
    session.setToken('jwt-test');
    expect(session.token()).toBe('jwt-test');
    expect(session.isLoggedIn()).toBe(true);
  });

  it('should clear token and report logged out after clear', () => {
    session.setToken('jwt-test');
    session.clear();
    expect(session.token()).toBeNull();
    expect(session.isLoggedIn()).toBe(false);
  });
});
