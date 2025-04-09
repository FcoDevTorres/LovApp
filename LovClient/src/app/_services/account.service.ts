import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private baseUrl = 'https://localhost:5104/api/';
  private http = inject(HttpClient)

  loginService(loginModel: any) {
    return this.http.post(this.baseUrl + 'account/login', loginModel)
  }
}
