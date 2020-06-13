import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
@Input() valuesFromHome: any;
@Output() cancelRegister = new EventEmitter();
 model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  register(){

      this.authService.register(this.model).subscribe(() => {
        console.log('Registration Successfull');
      },
      error => {
        console.log('Registration failed');
      }
      );

  }

  cancel(){
    this.cancelRegister.emit(false);
    console.log('Cancelled');
  }

}
