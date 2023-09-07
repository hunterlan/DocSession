import {Component, OnInit} from '@angular/core';
import {UserDetails} from "../../models/user-details";
import {Router} from "@angular/router";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
  userDetails: UserDetails | undefined;

  constructor(private readonly router: Router) {
  }
  ngOnInit(): void {
    const jsonUser = sessionStorage.getItem('userDetails');
    if (jsonUser != null) {
      this.userDetails = JSON.parse(sessionStorage.getItem('userDetails')!) as UserDetails;
    } else {
      this.router.navigate(['login'])
    }
  }
}
