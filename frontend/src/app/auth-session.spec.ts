import { TestBed } from '@angular/core/testing';
import { AuthSession } from './auth-session';

const TOKEN_KEY = 'miniadmin.token';

describe('AuthSession', () => {
  let session: AuthSession;

  beforeEach(() => {
    localStorage.clear();
    TestBed.resetTestingModule();
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

  it('should write token to localStorage on setToken', () => {
    session.setToken('jwt-test');
    expect(localStorage.getItem(TOKEN_KEY)).toBe('jwt-test');
  });

  it('should remove token from localStorage on clear', () => {
    session.setToken('jwt-test');
    session.clear();
    expect(localStorage.getItem(TOKEN_KEY)).toBeNull();
  });

  it('should restore token from localStorage on start', () => {
    localStorage.setItem(TOKEN_KEY, 'jwt-saved');
    TestBed.resetTestingModule();
    TestBed.configureTestingModule({});
    const restored = TestBed.inject(AuthSession);
    expect(restored.token()).toBe('jwt-saved');
    expect(restored.isLoggedIn()).toBe(true);
  });
});
