import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { firstValueFrom } from 'rxjs';
import { MembersApi } from './members-api';

describe('MembersApi', () => {
  let api: MembersApi;
  let httpTesting: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });
    api = TestBed.inject(MembersApi);
    httpTesting = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  it('should GET /api/members with Bearer token and return members', async () => {
    const pending = firstValueFrom(api.list('fake-jwt'));

    const req = httpTesting.expectOne('/api/members');
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBe('Bearer fake-jwt');

    req.flush([
      { id: 1, name: 'Ada', email: 'ada@example.com' },
      { id: 2, name: 'Bob', email: 'bob@example.com' },
    ]);

    await expect(pending).resolves.toEqual([
      { id: 1, name: 'Ada', email: 'ada@example.com' },
      { id: 2, name: 'Bob', email: 'bob@example.com' },
    ]);
  });

  it('should POST /api/members with Bearer token and return created member', async () => {
    const body = { id: 3, name: 'Cara', email: 'cara@example.com' };
    const pending = firstValueFrom(api.create('fake-jwt', body));

    const req = httpTesting.expectOne('/api/members');
    expect(req.request.method).toBe('POST');
    expect(req.request.headers.get('Authorization')).toBe('Bearer fake-jwt');
    expect(req.request.body).toEqual(body);

    req.flush(body);

    await expect(pending).resolves.toEqual(body);
  });
});
