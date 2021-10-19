import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserModel } from '../users/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private readonly baseURL:string = "https://localhost:44323/api/";
  constructor(private httpClient:HttpClient) {


   }

  public login(username:string, password:string){
    const body ={
      Name:username,
      Password:password
    }
    return this.httpClient.post(this.baseURL + "Login",body);
  }

  public register(username:string, pass:string,role:number){
    const body ={
      name:username,
      password:pass,
      role:{id:role}
    }
    console.log(body);
    return this.httpClient.post(this.baseURL + "Users",body);
  }

  public getAllUsers(){
    let token = JSON.parse(localStorage.getItem("userInfo")).token;
    const header = new HttpHeaders({
    'Authorization':`Bearer ${token}`
    });
    return this.httpClient.get(this.baseURL + "Users",{headers:header});
  }

  public updateUser(user:UserModel){
    let token = JSON.parse(localStorage.getItem("userInfo")).token;
    const header = new HttpHeaders({
    'Authorization':`Bearer ${token}`
    });
    debugger;
    return this.httpClient.put(this.baseURL + "Users/" + user.id,user,{headers:header});
  }

}
