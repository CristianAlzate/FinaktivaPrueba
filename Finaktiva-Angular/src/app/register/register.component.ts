import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  private imgLogo: string = "assets/images/logo-min.png";
  public registerForm = this.formBuilder.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
    role: ['1', Validators.required],
  });
  constructor(private formBuilder: FormBuilder, private userService: UserService,private router:Router) { }

  ngOnInit() {
  }
  OnSubmit() {
    let user = this.registerForm.controls["username"].value
    let password = this.registerForm.controls["password"].value
    let role = +this.registerForm.controls["role"].value

    this.userService.register(user, password, role).subscribe((data) => {
      this.router.navigate(["/login"]);
    });
  }
}
