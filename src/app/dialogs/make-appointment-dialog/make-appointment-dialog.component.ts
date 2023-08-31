import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-make-appointment-dialog',
  templateUrl: './make-appointment-dialog.component.html',
  styleUrls: ['./make-appointment-dialog.component.scss']
})
export class MakeAppointmentDialogComponent implements OnInit {
  appointmentForm!: FormGroup;

  constructor(public dialogRef: MatDialogRef<MakeAppointmentDialogComponent>,
    private readonly fb: FormBuilder) { }
  
  /*
    1. GET ALL AVAILABLE DOCTORS
    2. ADD FIELD "DOCTORS"
  */


  ngOnInit(): void {
    this.appointmentForm = this.fb.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      appointmentDate: ['', Validators.required],
      description: ['', Validators.required]
    });
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onSubmit(): void {
    if (this.appointmentForm.valid) {
      this.dialogRef.close(this.appointmentForm.value);
    }
  }
}
