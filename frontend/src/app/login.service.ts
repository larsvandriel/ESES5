import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginRequest } from './login-request.model';
import { RegisterRequest } from './register-request.model';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  baseUrl = 'http://localhost:5000/';

  constructor(private httpClient: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    const url = this.baseUrl + 'login';
    const body = JSON.stringify((new LoginRequest(username, password)));
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.httpClient.post(url, body, httpOptions);
  }

  register(username: string, email: string, password: string) {
    const url = this.baseUrl + 'register';
    const body = JSON.stringify((new RegisterRequest(username, email, password)));
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.httpClient.post(url, body, httpOptions)
  }

  register_admin(username: string, email: string, password: string) {
    const url = this.baseUrl + 'register-admin';
    const body = JSON.stringify((new RegisterRequest(username, email, password)));
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.httpClient.post(url, body, httpOptions)
  }
}
