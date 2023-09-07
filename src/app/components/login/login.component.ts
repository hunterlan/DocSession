import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {LoginUser} from "../../models/login-user";
import {SnackbarService} from "../../shared/services/snackbar-service";
import {NavigationExtras, Router} from "@angular/router";
import {UserDetails} from "../../models/user-details";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  hidePassword = true;

  loginForm = new FormGroup({
    login: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });
  constructor(private readonly snackbarService: SnackbarService,
              private readonly router: Router) {
  }

  submit() {
    if (this.loginForm.valid) {
      const form = new LoginUser(this.loginForm.value.login as string, this.loginForm.value.password as string);
      // Send form to server
      // If user is not admin
      if (form.login === 'user' && form.password == 'password') {
        const userDetails = new UserDetails(1, 'Valeriy Porozhanin', 'valeriy.porozhanin@ukr.net', false);
        sessionStorage.setItem('userDetails', JSON.stringify(userDetails));
        this.router.navigate(['home']);
      } else if (form.login === 'admin' && form.password === 'admin') {
        const userDetails = new UserDetails(2, 'Admin Adminovich', 'admin.adminovich@ukr.net', true);
        sessionStorage.setItem('userDetails', JSON.stringify(userDetails));
        this.router.navigate(['admin']);
      } else {
        this.snackbarService.showError('Wrong login or password', 'Close')
      }
      // this.spinnerService.show();
      // this.authService.login(form).subscribe({
      //   next: () => {
      //     this.router.navigate(['/details']);
      //   },
      //   error: (e) => {
      //     this.barService.showError(e);
      //   }
      // }).add(() => {
      //   this.spinnerService.hide();
      // });
    }
  }

}
