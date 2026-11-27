import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { provideRouter } from '@angular/router';
import { routes } from '../app.routes';
import { AuthSession } from '../auth-session';
import { ProductsPage } from './products-page';

describe('ProductsPage', () => {
  let httpTesting: HttpTestingController;
  let session: AuthSession;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductsPage],
      providers: [provideHttpClient(), provideHttpClientTesting(), provideRouter(routes)],
    }).compileComponents();
    httpTesting = TestBed.inject(HttpTestingController);
    session = TestBed.inject(AuthSession);
  });

  afterEach(() => {
    httpTesting.verify();
  });

  async function loadProducts(): Promise<{
    fixture: ComponentFixture<ProductsPage>;
    compiled: HTMLElement;
  }> {
    session.setToken('jwt-test');
    const fixture = TestBed.createComponent(ProductsPage);
    await fixture.whenStable();

    const req = httpTesting.expectOne('/api/products');
    expect(req.request.method).toBe('GET');
    expect(req.request.headers.get('Authorization')).toBe('Bearer jwt-test');
    req.flush([
      { id: 1, name: '筆記本', price: 100 },
      { id: 2, name: '原子筆', price: 20 },
    ]);
    await fixture.whenStable();
    fixture.detectChanges();
    return { fixture, compiled: fixture.nativeElement as HTMLElement };
  }

  it('should load API products into the list', async () => {
    const { compiled } = await loadProducts();
    const items = compiled.querySelectorAll('[data-testid="product-list"] li');
    expect(items.length).toBe(2);
    expect(items[0]?.textContent).toContain('筆記本 100');
    expect(items[1]?.textContent).toContain('原子筆 20');
  });

  it('should POST new product and show it in the list', async () => {
    const { fixture, compiled } = await loadProducts();

    const idInput = compiled.querySelector('[data-testid="product-id"]') as HTMLInputElement;
    const nameInput = compiled.querySelector('[data-testid="product-name"]') as HTMLInputElement;
    const priceInput = compiled.querySelector(
      '[data-testid="product-price"]',
    ) as HTMLInputElement;

    idInput.value = '3';
    idInput.dispatchEvent(new Event('input'));
    nameInput.value = '橡皮擦';
    nameInput.dispatchEvent(new Event('input'));
    priceInput.value = '15';
    priceInput.dispatchEvent(new Event('input'));
    await fixture.whenStable();

    const addBtn = compiled.querySelector(
      '[data-testid="product-add-btn"]',
    ) as HTMLButtonElement;
    addBtn.click();
    await fixture.whenStable();

    const post = httpTesting.expectOne('/api/products');
    expect(post.request.method).toBe('POST');
    expect(post.request.headers.get('Authorization')).toBe('Bearer jwt-test');
    expect(post.request.body).toEqual({ id: 3, name: '橡皮擦', price: 15 });
    post.flush({ id: 3, name: '橡皮擦', price: 15 });
    await fixture.whenStable();
    fixture.detectChanges();

    const items = compiled.querySelectorAll('[data-testid="product-list"] li');
    expect(items.length).toBe(3);
    expect(items[2]?.textContent).toContain('橡皮擦 15');
  });

  it('should DELETE product and remove it from the list', async () => {
    const { fixture, compiled } = await loadProducts();

    const deleteBtns = compiled.querySelectorAll(
      '[data-testid="product-delete-btn"]',
    ) as NodeListOf<HTMLButtonElement>;
    deleteBtns[0].click();
    await fixture.whenStable();

    const del = httpTesting.expectOne('/api/products/1');
    expect(del.request.method).toBe('DELETE');
    expect(del.request.headers.get('Authorization')).toBe('Bearer jwt-test');
    del.flush(null);
    await fixture.whenStable();
    fixture.detectChanges();

    const items = compiled.querySelectorAll('[data-testid="product-list"] li');
    expect(items.length).toBe(1);
    expect(items[0]?.textContent).toContain('原子筆 20');
  });

  it('should link to members page', async () => {
    const { compiled } = await loadProducts();
    const link = compiled.querySelector('[data-testid="nav-members"]') as HTMLAnchorElement;
    expect(link).toBeTruthy();
    expect(link.getAttribute('href')).toBe('/members');
  });

  it('should PUT product and refresh it in the list', async () => {
    const { fixture, compiled } = await loadProducts();

    const idInput = compiled.querySelector('[data-testid="product-id"]') as HTMLInputElement;
    const nameInput = compiled.querySelector('[data-testid="product-name"]') as HTMLInputElement;
    const priceInput = compiled.querySelector(
      '[data-testid="product-price"]',
    ) as HTMLInputElement;

    idInput.value = '1';
    idInput.dispatchEvent(new Event('input'));
    nameInput.value = '精裝筆記本';
    nameInput.dispatchEvent(new Event('input'));
    priceInput.value = '150';
    priceInput.dispatchEvent(new Event('input'));
    await fixture.whenStable();

    const updateBtn = compiled.querySelector(
      '[data-testid="product-update-btn"]',
    ) as HTMLButtonElement;
    updateBtn.click();
    await fixture.whenStable();

    const put = httpTesting.expectOne('/api/products/1');
    expect(put.request.method).toBe('PUT');
    expect(put.request.headers.get('Authorization')).toBe('Bearer jwt-test');
    expect(put.request.body).toEqual({ id: 1, name: '精裝筆記本', price: 150 });
    put.flush({ id: 1, name: '精裝筆記本', price: 150 });
    await fixture.whenStable();
    fixture.detectChanges();

    const items = compiled.querySelectorAll('[data-testid="product-list"] li');
    expect(items.length).toBe(2);
    expect(items[0]?.textContent).toContain('精裝筆記本 150');
    expect(items[1]?.textContent).toContain('原子筆 20');
  });
});
