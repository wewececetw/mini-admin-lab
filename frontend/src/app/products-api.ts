import { HttpClient } from '@angular/common/http';
import { Service, inject } from '@angular/core';
import { Observable } from 'rxjs';

export type ApiProduct = {
  id: number;
  name: string;
  price: number;
};

@Service()
export class ProductsApi {
  private readonly http = inject(HttpClient);

  list(token: string): Observable<ApiProduct[]> {
    return this.http.get<ApiProduct[]>('/api/products', {
      headers: { Authorization: `Bearer ${token}` },
    });
  }

  create(token: string, product: ApiProduct): Observable<ApiProduct> {
    return this.http.post<ApiProduct>('/api/products', product, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }

  delete(token: string, id: number): Observable<void> {
    return this.http.delete<void>(`/api/products/${id}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }

  update(token: string, product: ApiProduct): Observable<ApiProduct> {
    return this.http.put<ApiProduct>(`/api/products/${product.id}`, product, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }
}
