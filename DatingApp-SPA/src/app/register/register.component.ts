import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Output() cancelRegister = new EventEmitter();

  constructor(private authService: AuthService) {}

  ngOnInit() {}

  register() {
    console.log(this.model);
    this.authService.register(this.model).subscribe(
      () => {
        console.log('registeration successfull');
      },
      error => {
        console.log(error);
      }
    );
  }

  cancel() {
    console.log('cancelled');
    this.cancelRegister.emit(false);
  }
}
