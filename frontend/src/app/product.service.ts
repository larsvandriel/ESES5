import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Product} from './product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  baseUrl = 'http://localhost:5000/';

  constructor(private httpClient: HttpClient) { }

  getAllProducts(): Observable<any> {
    const url = this.baseUrl + 'product';
    return this.httpClient.get(url);
  }

  getProduct(productId: string): Observable<any> {
    const url = this.baseUrl + 'product/' + productId;
    return this.httpClient.get(url);
  }

  deleteProduct(productId: string): Observable<any> {
    const url = this.baseUrl + 'product/' + productId;
    return this.httpClient.delete(url);
  }

  createProduct(product: Product): Observable<any> {
    const url = this.baseUrl + 'product';
    const body = JSON.stringify(product);
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.httpClient.post(url, body, httpOptions);
  }

  editProduct(product: Product): Observable<any> {
    const url = this.baseUrl + 'product/' + product.id;
    const body = JSON.stringify(product);
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.httpClient.put(url, body, httpOptions);
  }
}
