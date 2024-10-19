import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmployeeListComponent } from './employee-list.component';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { EmployeeService } from '../../../services/employee.service';
import { Router } from '@angular/router';
import { ApiResponse } from '../../../models/ApiResponse{T}';
import { of, throwError } from 'rxjs';
import { Employee } from '../../../models/employee.model';

describe('EmployeeListComponent', () => {
  let component: EmployeeListComponent;
  let fixture: ComponentFixture<EmployeeListComponent>;
  let employeeServiceSpy: jasmine.SpyObj<EmployeeService>;
  let routerSpy: jasmine.SpyObj<Router>;
  const mockEmployee: Employee[] = [
    { employeeId: 1, firstName: 'Test', lastName:'Test',email:'employee1@gmail.com'},
    { employeeId: 1, firstName: 'Test2', lastName:'Test2',email:'employee1@gmail.com' },
    
  ];

  beforeEach(async () => {
    employeeServiceSpy = jasmine.createSpyObj('EmployeeService', ['getAllEmployee','getAllEmployeeCount']);
    await TestBed.configureTestingModule({
      declarations: [EmployeeListComponent],
      providers: [
        { provide: EmployeeService, useValue: employeeServiceSpy },
        provideHttpClient(),provideHttpClientTesting()
    ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmployeeListComponent);
    component = fixture.componentInstance;
    //fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should calculate total employee count',()=>{
    //Arrange
    const mockResponse :ApiResponse<number> ={success:true,data:2,message:''};
    employeeServiceSpy.getAllEmployeeCount.and.returnValue(of(mockResponse));
    const mockResponseall :ApiResponse<Employee[]> ={success:true,data:mockEmployee,message:''};
    employeeServiceSpy.getAllEmployee.and.returnValue(of(mockResponseall));

    //Act
    component.getAllEmployeeCount();

    //Assert
    expect(employeeServiceSpy.getAllEmployeeCount).toHaveBeenCalled();

  })
  it('should fail calculate total employee count',()=>{
    //Arrange
    const mockResponse :ApiResponse<number> ={success:false,data:0,message:'Failed to fetch employee count'};
    employeeServiceSpy.getAllEmployeeCount.and.returnValue(of(mockResponse));
    spyOn(console,'error')
    const mockResponseall :ApiResponse<Employee[]> ={success:true,data:mockEmployee,message:''};
    employeeServiceSpy.getAllEmployee.and.returnValue(of(mockResponseall));

    //Act
    component.getAllEmployeeCount();

    //Assert
    expect(employeeServiceSpy.getAllEmployeeCount).toHaveBeenCalled();
    expect(console.error).toHaveBeenCalledWith('Failed to fetch employee count','Failed to fetch employee count');

  })
  it('should handle Http error response while fethcing employee count',()=>{
    //Arrange
    const mockError = {message:'Network Error'};
    employeeServiceSpy.getAllEmployeeCount.and.returnValue(throwError(mockError));
    spyOn(console,'error')

    //Act
    component.getAllEmployeeCount();

    //Assert
    expect(employeeServiceSpy.getAllEmployeeCount).toHaveBeenCalled();
    expect(console.error).toHaveBeenCalledWith('Error fetching employee count.',mockError);
  })
  it('should load employee successfully',()=>{
    //Arrange
   
    const mockResponse :ApiResponse<Employee[]> ={success:true,data:mockEmployee,message:''};
    employeeServiceSpy.getAllEmployee.and.returnValue(of(mockResponse));
    //Act
    component.totalPages=100;
    component.getAllEmployee();

    //Assert
    expect(employeeServiceSpy.getAllEmployee).toHaveBeenCalledWith(component.pageNumber,component.pageSize);
    expect(component.employees).toEqual(mockEmployee);
    expect(component.loading).toBe(false);
  })
  it('should fail to load employee ',()=>{
    //Arrange
   
    const mockResponse :ApiResponse<Employee[]> ={success:false,data:[],message:'Failed to fetch employees'};
    employeeServiceSpy.getAllEmployee.and.returnValue(of(mockResponse));
    spyOn(console,'error')
    //Act
    component.totalPages=100;
    component.getAllEmployee();

    //Assert

    expect(component.employees).toEqual(undefined);
    expect(component.loading).toBe(false);
    expect(console.error).toHaveBeenCalledWith('Failed to fetch employees','Failed to fetch employees');
  })
  it('should fail to load employee because of pagesize',()=>{
    //Arrange
   
    const mockResponse :ApiResponse<Employee[]> ={success:false,data:[],message:'Failed to fetch employees'};
    employeeServiceSpy.getAllEmployee.and.returnValue(of(mockResponse));
    //Act
  
    component.getAllEmployee();

    //Assert

    expect(component.employees).toEqual(undefined);
    expect(component.loading).toBe(false);
  })
  it('should handle Http error response while fetch employee-list',()=>{
    //Arrange
    
    const mockError = { error: { message: 'Error' } };
    employeeServiceSpy.getAllEmployee.and.returnValue(throwError(mockError));
    spyOn(window,'alert').and.stub();

    //Act
    component.totalPages=100;
    component.getAllEmployee();

    //Assert
    expect(employeeServiceSpy.getAllEmployee).toHaveBeenCalled();
    expect(component.loading).toBe(false);
    expect(window.alert).toHaveBeenCalledWith(mockError.error.message); // Verify alert message
  })
  it('should call getAllEmployee and getAllEmployeeCount on initialization', () => {
    // Mocking isAuthenticated to return true
   

    // Spy on component methods
    spyOn(component, 'getAllEmployee');
    spyOn(component, 'getAllEmployeeCount');

    // Call ngOnInit
    component.ngOnInit();   
    // Expectations
    expect(component.getAllEmployee).toHaveBeenCalled();
    expect(component.getAllEmployeeCount).toHaveBeenCalled();
   
  });

});
