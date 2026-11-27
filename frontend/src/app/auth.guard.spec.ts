import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { Component } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { provideRouter, Router, RouterOutlet } from '@angular/router';
import { routes } from './app.routes';
import { AuthSession } from './auth-session';

@Component({
  imports: [RouterOutlet],
  template: `<router-outlet />`,
})
class Host {}

describe('authGuard', () => {
  let router: Router;
  let session: AuthSession;
  let httpTesting: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Host],
      providers: [provideRouter(routes), provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();
    router = TestBed.inject(Router);
    session = TestBed.inject(AuthSession);
    httpTesting = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  it('should redirect anonymous users from /members to /login', async () => {
    const fixture = TestBed.createComponent(Host);
    await router.navigateByUrl('/members');
    await fixture.whenStable();
    fixture.detectChanges();
    expect(router.url).toBe('/login');
  });

  it('should allow logged-in users to open /members', async () => {
    session.setToken('jwt-test');
    const fixture = TestBed.createComponent(Host);
    await router.navigateByUrl('/members');
    await fixture.whenStable();
    fixture.detectChanges();
    httpTesting.expectOne('/api/members').flush([]);
    await fixture.whenStable();
    expect(router.url).toBe('/members');
  });
});
