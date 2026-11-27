import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthSession } from '../auth-session';
import { ApiProduct, ProductsApi } from '../products-api';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-products-page',
  imports: [FormsModule, RouterLink],
  templateUrl: './products-page.html',
})
export class ProductsPage {
  private readonly session = inject(AuthSession);
  private readonly productsApi = inject(ProductsApi);
  private readonly router = inject(Router);

  protected readonly products = signal<ApiProduct[]>([]);
  protected readonly name = signal('');
  protected readonly price = signal(0);
  protected readonly id = signal(0);

  constructor() {
    this.load();
  }

  logout(): void {
    this.session.clear();
    this.router.navigateByUrl('/login');
  }

  load(): void {
    const token = this.session.token();
    if (!token) {
      return;
    }
    this.productsApi.list(token).subscribe((list) => {
      this.products.set(list);
    });
  }

  fill(p: ApiProduct): void {
    this.id.set(p.id);
    this.name.set(p.name);
    this.price.set(p.price);
  }

  add(): void {
    const token = this.session.token();
    if (!token) {
      return;
    }
    this.productsApi
      .create(token, { id: this.id(), name: this.name(), price: this.price() })
      .subscribe((p) => {
        this.products.update((list) => [...list, p]);
      });
  }

  save(): void {
    const token = this.session.token();
    if (!token) {
      return;
    }
    this.productsApi
      .update(token, { id: this.id(), name: this.name(), price: this.price() })
      .subscribe((p) => {
        this.products.update((list) => list.map((x) => (x.id === p.id ? p : x)));
      });
  }

  remove(id: number): void {
    const token = this.session.token();
    if (!token) {
      return;
    }
    this.productsApi.delete(token, id).subscribe(() => {
      this.products.update((list) => list.filter((p) => p.id !== id));
    });
  }
}
