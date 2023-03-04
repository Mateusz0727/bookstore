import axios from "../../plugins/axios";

export default class AuthService{
    public static async login(email:string,password:string):Promise<any>
    {
            const result =  (  await axios.post("/auth/login",{
                email,
                password,
            })).data;
       
            if(result){        
                localStorage.setItem("user",JSON.stringify(result));
            }
    }
    public static getCurrentUser() {
       
        const user = localStorage.getItem("user")
        if(user)
            return  JSON.parse(user);
        else return null;
    };
}
export interface LoginModel{
    userName:string;
    password:string;
}