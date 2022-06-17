import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginService } from '../login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username = "";
  password = "";

  submitted = false;

  constructor(private loginService: LoginService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.submitted = true;
  }

  login(): void {
    this.loginService.login(this.username, this.password).subscribe(data => {
      console.log(data);
      sessionStorage.setItem("token", data.token)
      this.router.navigate(['product']);
    });
  }

  register(): void {
    this.router.navigate(['register'])
  }

}
