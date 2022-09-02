import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginModel } from '../models/login.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) { }
  login(creds: LoginModel): Observable<any> {
    let url = environment.serverURL + "api/token";
    return this.http.post(url, creds, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    });
  }
  isUserAuthenticated(): boolean {
    let token = localStorage.getItem("jwt");
    return token && !this.jwtHelper.isTokenExpired(token);
  }
}
