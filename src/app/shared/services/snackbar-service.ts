import {MatSnackBar, MatSnackBarConfig} from "@angular/material/snack-bar";
import {Injectable} from "@angular/core";

@Injectable({
  providedIn: 'root',
})
export class SnackbarService {
  constructor(private readonly snackBar: MatSnackBar) {}

  showSuccess(message: string, action: string) {
    this.snackBar.open(message, action, {
      panelClass: 'success-bar',
      duration: 1000
    });
  }

  showError(message: string, action: string) {
    this.snackBar.open(message, action, {
      panelClass: 'error-bar',
      duration: 2000
    });
  }
}
