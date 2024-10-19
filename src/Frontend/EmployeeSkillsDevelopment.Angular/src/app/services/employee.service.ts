import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ApiResponse } from '../models/ApiResponse{T}';
import { Employee } from '../models/employee.model';
import { Observable } from 'rxjs';
import { MsalUserService } from './msaluser.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl=environment.domain;
  // httpOptions = {
  //   headers: new HttpHeaders({
  //     "Content-Type": "application/json",
  //   }),
  // };
  constructor(private http:HttpClient,private msalService: MsalUserService) {
    // this.httpOptions = {
    //   headers: new HttpHeaders({
    //     "Content-Type": "application/json",
    //     Authorization: "Bearer " + this.msalService.getAccessToken(),
    //   }),
    // };
   }
  getAllEmployee(pageNumber: number,pageSize:number) : Observable<ApiResponse<Employee[]>>{
    return this.http.get<ApiResponse<Employee[]>>(this.apiUrl+'Employee/GetAllEmployees?page='+pageNumber+'&pageSize='+pageSize);
  }
  getAllEmployeeCount() : Observable<ApiResponse<number>>{
    return this.http.get<ApiResponse<number>>(this.apiUrl+'Employee/GetTotalEmployeesCount');
  }
}
