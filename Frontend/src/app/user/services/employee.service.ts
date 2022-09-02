import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, } from 'rxjs';
import { environment } from '../../../environments/environment';
import { EmployeeModel } from '../models/employee.model';
@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  apiUrl: string = environment.serverURL + 'api/employee/';
  constructor(private http: HttpClient) { }

  post(addModel: EmployeeModel): Observable<EmployeeModel> {
    return this.http.post<EmployeeModel>(this.apiUrl, addModel, {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
    });
  }
  put(editModel: EmployeeModel): Observable<EmployeeModel> {
    return this.http.put<EmployeeModel>(this.apiUrl, editModel, {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
    });
  }
  delete(id: number): Observable<any> {
    debugger;
    return this.http.delete(this.apiUrl + id, {
      headers: new HttpHeaders({
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
});
  }
  get(id: number): Observable<EmployeeModel> {
    return this.http.get<EmployeeModel>(this.apiUrl + id, {
      headers: new HttpHeaders({
        "Accept-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
    });
  }
  getAll(): Observable<EmployeeModel[]> {
    return this.http.get<EmployeeModel[]>(this.apiUrl, {
      headers: new HttpHeaders({
        "Accept-Type": "application/json",
        "Authorization": "Bearer " + localStorage.getItem("jwt")
      })
    });
  }
}

