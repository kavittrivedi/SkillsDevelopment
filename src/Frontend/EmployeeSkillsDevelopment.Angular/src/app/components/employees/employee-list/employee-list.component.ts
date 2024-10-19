import { Component } from '@angular/core';
import { Employee } from '../../../models/employee.model';
import { ApiResponse } from '../../../models/ApiResponse{T}';
import { EmployeeService } from '../../../services/employee.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrl: './employee-list.component.css'
})
export class EmployeeListComponent {
  employees:any[] | undefined |null;
  pageNumber: number = 1;
  pageSize: number = 4;
  totalItems: number = 0;
  totalPages: number = 0;
  loading:boolean=false;
  constructor(private employeeService:EmployeeService,private router: Router){}
  ngOnInit(): void {
      this.getAllEmployeeCount();
      this.getAllEmployee();
  }
  getAllEmployeeCount():void{
   
    this.employeeService.getAllEmployeeCount().subscribe({
      next: (response: ApiResponse<number>) => {
        if (response.success) {
          this.totalItems = response.data;
          this.totalPages = Math.ceil(this.totalItems / this.pageSize);
          this.getAllEmployee();
        } else {
          console.error('Failed to fetch employee count', response.message);
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error fetching employee count.', error);
        this.loading = false;
      }
    });

  }
  getAllEmployee(): void {
    if (this.pageNumber > this.totalPages) {
      console.log('Requested page does not exist.');
      return;
    }
  
    this.employeeService.getAllEmployee(this.pageNumber, this.pageSize).subscribe({
      next: (response: ApiResponse<Employee[]>) => {
        if (response.success) {
          this.employees = response.data;
        } else {
          console.error('Failed to fetch employees', response.message);
        }
        this.loading = false;
      },
      error: (err) => {
        console.log(err);
        this.loading = false;
        alert(err.error.message);
      },
      complete: () => {
        this.loading = false;
        console.log('Completed');
      }
    });
    
  }
  changePageSize(event: Event): void {
    const target = event.target as HTMLSelectElement;
    const newSize = target.value;
    this.pageSize = +newSize; 
    this.pageNumber = 1; 
    this.totalPages = Math.ceil(this.totalItems / this.pageSize);
    this.getAllEmployeeCount();
}

  changePage(pageNumber: number): void {
    this.pageNumber = pageNumber;
    this.getAllEmployee();
  }
 

}
