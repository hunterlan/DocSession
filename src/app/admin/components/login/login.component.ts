import { Component } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {LoginUser} from "../../models/UserLogin";
import {SnackbarService} from "../../../shared/services/snackbar-service";

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
  constructor(private readonly snackbarService: SnackbarService) {
  }

  submit() {
    if (this.loginForm.valid) {
      const form = new LoginUser(this.loginForm.value.login as string, this.loginForm.value.password as string);
      if (form.login === 'user' && form.password == 'password') {

      } else if (form.login === 'admin' && form.password === 'admin') {

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
