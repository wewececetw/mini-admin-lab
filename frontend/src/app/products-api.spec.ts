import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { firstValueFrom } from 'rxjs';
import { ProductsApi } from './products-api';

describe('ProductsApi', () => {
  let api: ProductsApi;
  let httpTesting: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [provideHttpClient(), provideHttpClientTesting()],
    });
    api = TestBed.inject(ProductsApi);
    httpTesting = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  it('should GET /api/products with Bearer token and return products', async () => {
    const pending = firstValueFrom(api.list('fake-jwt'));

    const req = httpTesting.expectOne('/api/products');
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBe('Bearer fake-jwt');

    req.flush([
      { id: 1, name: '筆記本', price: 100 },
      { id: 2, name: '原子筆', price: 20 },
    ]);

    await expect(pending).resolves.toEqual([
      { id: 1, name: '筆記本', price: 100 },
      { id: 2, name: '原子筆', price: 20 },
    ]);
  });

  it('should POST /api/products with Bearer token and return created product', async () => {
    const body = { id: 3, name: '橡皮擦', price: 15 };
    const pending = firstValueFrom(api.create('fake-jwt', body));

    const req = httpTesting.expectOne('/api/products');
    expect(req.request.method).toBe('POST');
    expect(req.request.headers.get('Authorization')).toBe('Bearer fake-jwt');
    expect(req.request.body).toEqual(body);

    req.flush(body);

    await expect(pending).resolves.toEqual(body);
  });

  it('should DELETE /api/products/:id with Bearer token', async () => {
    const pending = firstValueFrom(api.delete('fake-jwt', 1));

    const req = httpTesting.expectOne('/api/products/1');
    expect(req.request.method).toBe('DELETE');
    expect(req.request.headers.get('Authorization')).toBe('Bearer fake-jwt');

    req.flush(null);
    await expect(pending).resolves.toBeNull();
  });

  it('should PUT /api/products/:id with Bearer token and return updated product', async () => {
    const body = { id: 1, name: '精裝筆記本', price: 150 };
    const pending = firstValueFrom(api.update('fake-jwt', body));

    const req = httpTesting.expectOne('/api/products/1');
    expect(req.request.method).toBe('PUT');
    expect(req.request.headers.get('Authorization')).toBe('Bearer fake-jwt');
    expect(req.request.body).toEqual(body);

    req.flush(body);

    await expect(pending).resolves.toEqual(body);
  });
});
