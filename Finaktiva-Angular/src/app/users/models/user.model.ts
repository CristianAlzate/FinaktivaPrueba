export class UserModel {

    public id:number;
    public name:string = "";
    public password:string = "";
    public active:boolean;
    public role:RoleModel;
  
}
export class RoleModel {
    public id:number;
    public description:string = "";
}