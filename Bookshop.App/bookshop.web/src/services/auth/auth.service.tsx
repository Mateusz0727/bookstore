import axios from "../../plugins/axios";

 class AuthService{
    
  public  login= async(email:string,password:string)=>
    {
         return await  axios.post("/auth/login",{
                email,
                password,
            });           
    }
     getCurrentUser() {
       
        const user = localStorage.getItem("user")
        if(user)
            return  JSON.parse(user);
        else return null;
    };
}
export default new AuthService();
export interface LoginModel{
    userName:string;
    password:string;
}