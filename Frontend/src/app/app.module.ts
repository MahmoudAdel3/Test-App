import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ToastrModule } from 'ngx-toastr';
import { RouterModule } from '@angular/router';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from "../environments/environment";
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginComponent } from './account/components/login-component/login.component';
import { AuthGuard } from './guards/auth.guard';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: 'login', component: LoginComponent },
      { path: '', redirectTo: 'login', pathMatch: 'full' },
      { path: 'user', loadChildren: () => import('./user/user.module').then(e => e.UserModule), canActivate: [AuthGuard] }
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem("jwt")
        },
        allowedDomains: [environment.serverURL],
        disallowedRoutes: []
      }
    }),
    ToastrModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
