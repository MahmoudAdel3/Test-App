import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmployeeComponentComponent } from './components/employee-component/employee-component.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';



@NgModule({
  declarations: [
    EmployeeComponentComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    SweetAlert2Module,
    RouterModule.forChild([
      {
        path: '', children:
        [
            { path: "employee", component: EmployeeComponentComponent }
        ]
      }
    ]),
  ]
})
export class UserModule { }
