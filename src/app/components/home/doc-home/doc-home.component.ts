import {Component, Input, OnInit} from '@angular/core';
import {Appointment} from "../../../models/appointment";
import {UserDetails} from "../../../models/user-details";

@Component({
  selector: 'app-doc-home',
  templateUrl: './doc-home.component.html',
  styleUrls: ['./doc-home.component.scss']
})
export class DocHomeComponent implements OnInit {
  @Input() userDetails = new UserDetails(0, '', '', false);
  appointments: Appointment[] = [];

  constructor() {
  }

  ngOnInit(): void {
    // Fetch data about appointments
    this.appointments = [
      new Appointment(1, '9/7/2023, 10:30', 'Ivan Levitskiy'),
      new Appointment(2, '9/7/2023, 12:30', 'Yevhenii Yenovich'),
      new Appointment(3, '9/7/2023, 14:30', 'Iryna Amazarova'),
      new Appointment(4, '9/7/2023, 17:30', 'Taras Stepanenko'),
    ]
  }
}
