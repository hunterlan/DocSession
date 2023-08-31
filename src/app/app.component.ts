import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MakeAppointmentDialogComponent } from './dialogs/make-appointment-dialog/make-appointment-dialog.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'docsession-ui';

  constructor(private readonly dialog: MatDialog) {}

  createAppointment(): void {
      const dialogRef = this.dialog.open(MakeAppointmentDialogComponent);
      dialogRef.afterClosed().subscribe(data => {
        //call API here
      });
  }
}
