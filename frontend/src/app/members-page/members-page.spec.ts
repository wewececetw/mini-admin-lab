import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthSession } from '../auth-session';
import { MembersPage } from './members-page';

describe('MembersPage', () => {
  let httpTesting: HttpTestingController;
  let session: AuthSession;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MembersPage],
      providers: [provideHttpClient(), provideHttpClientTesting()],
    }).compileComponents();
    httpTesting = TestBed.inject(HttpTestingController);
    session = TestBed.inject(AuthSession);
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
});
