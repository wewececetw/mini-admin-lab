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

describe('app.routes', () => {
  let httpTesting: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Host],
      providers: [provideRouter(routes), provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();
    httpTesting = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  async function go(path: string): Promise<HTMLElement> {
    const fixture = TestBed.createComponent(Host);
    const router = TestBed.inject(Router);
    await router.navigateByUrl(path);
    await fixture.whenStable();
    fixture.detectChanges();
    return fixture.nativeElement as HTMLElement;
  }

  it('should show login page at /login', async () => {
    const el = await go('/login');
    expect(el.querySelector('[data-testid="login-page"]')).not.toBeNull();
  });

  it('should show members page at /members when logged in', async () => {
    TestBed.inject(AuthSession).setToken('jwt-test');
    const fixture = TestBed.createComponent(Host);
    const router = TestBed.inject(Router);
    await router.navigateByUrl('/members');
    await fixture.whenStable();
    fixture.detectChanges();
    httpTesting.expectOne('/api/members').flush([]);
    await fixture.whenStable();
    fixture.detectChanges();
    expect(
      (fixture.nativeElement as HTMLElement).querySelector('[data-testid="members-page"]'),
    ).not.toBeNull();
  });

  it('should redirect / to /login', async () => {
    const el = await go('/');
    const router = TestBed.inject(Router);
    expect(router.url).toBe('/login');
    expect(el.querySelector('[data-testid="login-page"]')).not.toBeNull();
  });
});
