import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}; // creating an empty object to store username and password. 
  constructor(public authService: AuthService , private alertify: AlertifyService) { }

  ngOnInit() {
  }
  login(){
    this.authService.login(this.model).subscribe(next => {
     // console.log('Logged in successfully!!');
     this.alertify.success('Logged in successfully!!');
    },
    error => {
      // console.log('Failed to login');
      this.alertify.error('Failed to login');
    }
    );
  }

  loggedIn(){
  // const token = localStorage.getItem('token');
  // hete the !! means, it'll return true or false according to the return value. Means it returns false if token is null.
  // return !!token;
  return this.authService.loggedIn();

  }

  logOut(){
      localStorage.removeItem('token');
      // console.log('Logged out!!');
      this.alertify.message('Logged out!!');
  }
}
