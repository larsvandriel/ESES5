import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../login.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  username = "";
  email = "";
  password = "";

  submitted = false;

  constructor(private loginService: LoginService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.submitted = true;
  }

  register(): void {
    this.loginService.register(this.username, this.email, this.password).subscribe(data => {
      console.log(data);
      this.router.navigate(['login']);
    });
  }

  register_admin(): void {
    this.loginService.register_admin(this.username, this.email, this.password).subscribe(data => {
      console.log(data);
      this.router.navigate(['login']);
    });
  }

  login(): void {
    this.router.navigate(['login'])
  }

}
