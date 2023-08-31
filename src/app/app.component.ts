import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MakeAppointmentDialogComponent } from './dialogs/make-appointment-dialog/make-appointment-dialog.component';
import {SnackbarService} from "./shared/services/snackbar-service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'docsession-ui';

  constructor(private readonly dialog: MatDialog,
              private readonly snackbarService: SnackbarService,) {}

  createAppointment(): void {
      const dialogRef = this.dialog.open(MakeAppointmentDialogComponent);
      dialogRef.afterClosed().subscribe(data => {
        //call API here
        this.snackbarService.showSuccess('You successfully applied a form of appointment!', 'Close');
      });
  }
}
