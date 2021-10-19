import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  private imgLogo: string = "assets/images/logo-min.png";
  public loginForm = this.formBuilder.group({
    username:['',Validators.required],
    password:['',Validators.required]
  });
  constructor(private formBuilder:FormBuilder,private userService:UserService,private router:Router) { }

  ngOnInit() {
    localStorage.removeItem("userInfo");
  }

  OnSubmit(){
    let user = this.loginForm.controls["username"].value
    let password = this.loginForm.controls["password"].value
    this.userService.login(user, password).subscribe((data:any) => {
      localStorage.setItem("userInfo",JSON.stringify(data));
      this.router.navigate(["/user"]);
    });
  }

}
