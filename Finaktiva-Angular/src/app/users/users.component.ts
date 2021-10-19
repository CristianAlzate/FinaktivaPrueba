import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';
import { UserModel } from './models/user.model';
import {NgbModal} from '@ng-bootstrap/ng-bootstrap';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  constructor(private userService:UserService,private router:Router,private modalService: NgbModal,private formBuilder: FormBuilder) { }

  public users: Array<UserModel>;
  public role: number;


  public updateForm:FormGroup; 

  ngOnInit() {
    this.role = +JSON.parse(localStorage.getItem("userInfo")).role;
    this.getAllUsers();
    
  }

  getAllUsers(){
    this.userService.getAllUsers().subscribe((data:Array<UserModel>) => {
      this.users = data;
    },error => {
      this.router.navigate(["/login"]);
    });
  }
  OpenUpdate(content,user:UserModel) {
    this.updateForm = this.formBuilder.group({
      username: [user.name, Validators.required],
      password: [user.password, Validators.required],
      role: [user.role.id, Validators.required],
      active: [user.active]
    });
    this.modalService.open(content, {ariaLabelledBy: 'modal-basic-title'}).result.then((result) => {
      let name = this.updateForm.controls["username"].value
      let password = this.updateForm.controls["password"].value
      let role = +this.updateForm.controls["role"].value
      let active = this.updateForm.controls["active"].value

      user.name = name;
      user.password = password;
      user.role.id = role;
      user.active =active;

      this.userService.updateUser(user).subscribe((data:UserModel) => {
        this.getAllUsers();
      },error => {
        this.router.navigate(["/login"]);
      });
    }, (reason) => {

    });
  }

}
