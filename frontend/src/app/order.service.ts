import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Order} from './order.model';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  baseUrl = 'https://localhost:7003/api/';

  constructor(private httpClient: HttpClient) { }

  createOrder(order: Order): Observable<any> {
    const url = this.baseUrl + 'order';
    const body = JSON.stringify(order);
    const httpOptions = {headers: new HttpHeaders({'Content-Type': 'application/json'})};
    return this.httpClient.post(url, body, httpOptions);
  }

}
