import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter, Router } from '@angular/router';
import { routes } from '../app.routes';
import { AuthSession } from '../auth-session';
import { MembersPage } from './members-page';

describe('MembersPage', () => {
  let httpTesting: HttpTestingController;
  let session: AuthSession;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MembersPage],
      providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter(routes)],
    }).compileComponents();
    httpTesting = TestBed.inject(HttpTestingController);
    session = TestBed.inject(AuthSession);
    router = TestBed.inject(Router);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  async function loadMembers(): Promise<{
    fixture: ComponentFixture<MembersPage>;
    compiled: HTMLElement;
  }> {
    session.setToken('jwt-test');
    const fixture = TestBed.createComponent(MembersPage);
    await fixture.whenStable();

    const membersReq = httpTesting.expectOne('/api/members');
    expect(membersReq.request.method).toBe('GET');
    expect(membersReq.request.headers.get('Authorization')).toBe('Bearer jwt-test');
    membersReq.flush([
      { id: 1, name: 'Ada', email: 'ada@example.com' },
      { id: 2, name: 'Bob', email: 'bob@example.com' },
      { id: 3, name: 'Cara', email: 'cara@example.com' },
    ]);
    await fixture.whenStable();
    fixture.detectChanges();
    return { fixture, compiled: fixture.nativeElement as HTMLElement };
  }

  it('should load API members into the list', async () => {
    const { compiled } = await loadMembers();
    const items = compiled.querySelectorAll('[data-testid="member-list"] li');
    expect(items.length).toBe(3);
    expect(items[0]?.textContent?.trim()).toBe('Ada');
    expect(items[1]?.textContent?.trim()).toBe('Bob');
    expect(items[2]?.textContent?.trim()).toBe('Cara');
  });

  it('should show member count 3', async () => {
    const { compiled } = await loadMembers();
    expect(compiled.querySelector('[data-testid="member-count"]')?.textContent?.trim()).toBe(
      '3',
    );
  });

  it('should filter by search', async () => {
    const { fixture, compiled } = await loadMembers();
    const search = compiled.querySelector('[data-testid="member-search"]') as HTMLInputElement;
    search.value = 'bo';
    search.dispatchEvent(new Event('input'));
    await fixture.whenStable();
    fixture.detectChanges();

    const items = compiled.querySelectorAll('[data-testid="member-list"] li');
    expect(items.length).toBe(1);
    expect(items[0]?.textContent?.trim()).toBe('Bob');
  });

  it('should clear token and navigate to /login on logout', async () => {
    const { fixture, compiled } = await loadMembers();
    const button = compiled.querySelector('[data-testid="logout-btn"]') as HTMLButtonElement;
    button.click();
    await fixture.whenStable();

    expect(session.token()).toBeNull();
    expect(router.url).toBe('/login');
  });

  it('should link to products page', async () => {
    const { compiled } = await loadMembers();
    const link = compiled.querySelector('[data-testid="nav-products"]') as HTMLAnchorElement;
    expect(link).toBeTruthy();
    expect(link.getAttribute('href')).toBe('/products');
  });

  it('should POST new member and show it in the list', async () => {
    const { fixture, compiled } = await loadMembers();

    const idInput = compiled.querySelector('[data-testid="member-id"]') as HTMLInputElement;
    const nameInput = compiled.querySelector('[data-testid="member-name"]') as HTMLInputElement;
    const emailInput = compiled.querySelector(
      '[data-testid="member-email"]',
    ) as HTMLInputElement;

    idInput.value = '4';
    idInput.dispatchEvent(new Event('input'));
    nameInput.value = 'Dan';
    nameInput.dispatchEvent(new Event('input'));
    emailInput.value = 'dan@example.com';
    emailInput.dispatchEvent(new Event('input'));
    await fixture.whenStable();

    const addBtn = compiled.querySelector(
      '[data-testid="member-add-btn"]',
    ) as HTMLButtonElement;
    addBtn.click();
    await fixture.whenStable();

    const post = httpTesting.expectOne('/api/members');
    expect(post.request.method).toBe('POST');
    expect(post.request.headers.get('Authorization')).toBe('Bearer jwt-test');
    expect(post.request.body).toEqual({
      id: 4,
      name: 'Dan',
      email: 'dan@example.com',
    });
    post.flush({ id: 4, name: 'Dan', email: 'dan@example.com' });
    await fixture.whenStable();
    fixture.detectChanges();

    const items = compiled.querySelectorAll('[data-testid="member-list"] li');
    expect(items.length).toBe(4);
    expect(items[3]?.textContent?.trim()).toBe('Dan');
    expect(compiled.querySelector('[data-testid="member-count"]')?.textContent?.trim()).toBe(
      '4',
    );
  });
});
