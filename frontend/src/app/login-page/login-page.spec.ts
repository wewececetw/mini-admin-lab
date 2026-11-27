import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { provideRouter, Router } from '@angular/router';
import { routes } from '../app.routes';
import { AuthSession } from '../auth-session';
import { LoginPage } from './login-page';

describe('LoginPage', () => {
  let httpTesting: HttpTestingController;
  let session: AuthSession;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginPage],
      providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter(routes)],
    }).compileComponents();
    httpTesting = TestBed.inject(HttpTestingController);
    session = TestBed.inject(AuthSession);
    router = TestBed.inject(Router);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  it('should login, store token, and navigate to /members', async () => {
    const fixture = TestBed.createComponent(LoginPage);
    await fixture.whenStable();
    const compiled = fixture.nativeElement as HTMLElement;

    const button = compiled.querySelector('[data-testid="login-btn"]') as HTMLButtonElement;
    button.click();
    await fixture.whenStable();

    const loginReq = httpTesting.expectOne('/api/auth/login');
    expect(loginReq.request.method).toBe('POST');
    expect(loginReq.request.body).toEqual({ username: 'admin', password: 'secret' });
    loginReq.flush({ token: 'jwt-test' });
    await fixture.whenStable();

    expect(session.token()).toBe('jwt-test');
    expect(router.url).toBe('/members');
  });
});
