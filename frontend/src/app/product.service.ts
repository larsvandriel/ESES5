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
    const httpOptions = {headers: new HttpHeaders({'Authorization': 'Bearer ' + sessionStorage.getItem("Token")})};
    return this.httpClient.get(url, httpOptions);
  }

  getProduct(productId: string): Observable<any> {
    const url = this.baseUrl + 'product/' + productId;
    const httpOptions = {headers: new HttpHeaders({'Authorization': 'Bearer ' + sessionStorage.getItem("Token")})};
    return this.httpClient.get(url, httpOptions);
  }

  deleteProduct(productId: string): Observable<any> {
    const url = this.baseUrl + 'product/' + productId;
    const httpOptions = {headers: new HttpHeaders({'Authorization': 'Bearer ' + sessionStorage.getItem("Token")})};
    return this.httpClient.delete(url, httpOptions);
  }

  createProduct(product: Product): Observable<any> {
    const url = this.baseUrl + 'product';
    const body = JSON.stringify(product);
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json', 'Authorization': 'Bearer ' + sessionStorage.getItem("Token")})};
    return this.httpClient.post(url, body, httpOptions);
  }

  editProduct(product: Product): Observable<any> {
    const url = this.baseUrl + 'product/' + product.id;
    const body = JSON.stringify(product);
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json', 'Authorization': 'Bearer ' + sessionStorage.getItem("Token")})};
    return this.httpClient.put(url, body, httpOptions);
  }
}
