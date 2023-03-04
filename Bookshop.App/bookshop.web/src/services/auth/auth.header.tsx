export default function authHeader():string {

  const user = JSON.parse(localStorage.getItem("user")||"");

  if (user && user.token) {   
    return 'Bearer ' + user.token ;
    // return { "x-auth-token": user.accessToken };
  } else {
    return "";
  }
}