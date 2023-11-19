import { Component } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {SnackbarService} from "../../shared/services/snackbar-service";
import {MakeAppointmentDialogComponent} from "../../dialogs/make-appointment-dialog/make-appointment-dialog.component";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent {
  constructor(private readonly dialog: MatDialog,
              private readonly snackbarService: SnackbarService,) {}

  createAppointment(): void {
    const dialogRef = this.dialog.open(MakeAppointmentDialogComponent);
    dialogRef.afterClosed().subscribe(data => {
      //call API here
      //this.snackbarService.showSuccess('You successfully applied a form of appointment!', 'Close');
    });
  }
}
