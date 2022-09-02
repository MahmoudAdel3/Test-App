import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { EmployeeModel } from '../../models/employee.model';
import { EmployeeService } from '../../services/employee.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-employee-component',
  templateUrl: './employee-component.component.html',
  styleUrls: ['./employee-component.component.css']
})
export class EmployeeComponentComponent implements OnInit {
  constructor(private empService: EmployeeService, private toastr: ToastrService, private modalService: NgbModal) { }
  employeesList: EmployeeModel[];
  addForm: FormGroup;
  editForm: FormGroup;

  @ViewChild('Add', { static: false }) addModal: ElementRef;
  @ViewChild('Edit', { static: false }) editModal: ElementRef;

  ngOnInit(): void {
    this.empService.getAll().subscribe(response => {
      this.employeesList = response;
    }, error => {
      this.toastr.warning("Something went wrong");
    })
  }
  getAdd() {
    this.addForm = new FormGroup({
      name: new FormControl('', Validators.required)
    });
    this.modalService.open(this.addModal);
  }
  postAdd() {
    if (!this.addForm.valid)
      return;

    let model = new EmployeeModel(this.addForm.value.name);
    this.empService.post(model).subscribe(result => {
      this.employeesList.push(result);
      this.toastr.success("Record Saved Successfully");
      this.modalService.dismissAll();
    }, error => this.toastr.error(error.Message));
  }

  getEdit(id: number) {
    this.empService.get(id).subscribe(result => {
      this.editForm = new FormGroup({
        name: new FormControl(result.name, Validators.required),
        id: new FormControl(result.id)
      });
      this.modalService.open(this.editModal);
    }, error => this.toastr.error(error.Message));
  }
  postEdit() {
    if (!this.editForm.valid)
      return;

    let model = new EmployeeModel(this.editForm.value.name, this.editForm.value.id);
    this.empService.put(model).subscribe(result => {
      let item = this.employeesList.filter(i => i.id == result.id)[0];
      item.name = result.name;
      this.toastr.success("Record Updated Successfully");
      this.modalService.dismissAll();
    }, error => this.toastr.error(error.Message));
  }

  delete(id: number) {
    Swal.fire({
      title: "Delete",
      text: "Are you sure you want to delete this?",
      icon: 'warning',
      showCancelButton: true,
      showCloseButton: true
    }).then((result) => {
      if (result.isConfirmed) {
        this.empService.delete(id).subscribe(r => {
          this.employeesList = this.employeesList.filter(i => i.id != id);
          this.toastr.success("Record Deleted Successfully");
        })
      }
    })
  }

}
