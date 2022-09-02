import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/shared/services/auth.service';
import { LoginModel } from '../../../shared/models/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  invalidLogin: boolean;
  loginForm: FormGroup;
  constructor(private router: Router, private toastr: ToastrService, private authService: AuthService) { }
  ngOnInit(): void {
    if (this.authService.isUserAuthenticated())
      this.router.navigate(["/user/employee"]);

    this.loginForm = new FormGroup({
      username: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required)
    });
  }

  login() {
    this.loginForm.markAllAsTouched();
    if (!this.loginForm.valid)
      return;

    let model = new LoginModel()
    model.Username= this.loginForm.value.username;
    model.Password = this.loginForm.value.password;
    this.authService.login(model).subscribe(response => {
      const token = (<any>response).token;
      localStorage.setItem("jwt", token);
      this.invalidLogin = false;
      this.toastr.success("Logged In successfully");
      this.router.navigate(["/user/employee"]);
    }, err => {
      this.invalidLogin = true;
    });
  }

}
