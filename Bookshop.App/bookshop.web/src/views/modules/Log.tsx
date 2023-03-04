import React, { Component } from 'react'
import { Search } from '../component/Search/Search';
import { Link } from 'react-router-dom';
import AuthService, { LoginModel } from '../../services/auth/auth.service';



export default class Log extends Component {
    private form :LoginModel = {
        userName:'',
        password:''
      }
      async onSubmit():Promise<void> {
       
        try {
          await AuthService.login(this.form.userName,this.form.password);
        console.log(this.form.password);
          
        } catch (err) {
            debugger;
          console.log(err);
        }
      };
      async setEmail(e:React.ChangeEvent<HTMLInputElement>){
        this.form.userName =e.target.value
     }
     async setPassword(e:React.ChangeEvent<HTMLInputElement>){
        this.form.password =e.target.value
      }
    render() {
        return (
            <div>
                <header>
                    <div id="logo"><img src="logo1.png" alt="jeszcze_nic" /></div>
                    <Search />
                    <nav>
                        <Link to="/" className="butnav">Główna</Link>
                        <Link to="/SubPage" className="butnav">Księgarnia</Link>
                        <Link to="/Log" className="butnav">Logowanie</Link>
                        <Link to="/Bag" className="butnav">Koszyk</Link>
                    </nav>
                </header>
                <main>
                    <div className="center">
                        <h1>Login</h1>
                        <form>
                            <div className="txt_field">
                                <input type="text" 
                                required 
                                 autoComplete='on'
                                onChange={(e) => this.setEmail(e)}
                                />

                                <label>Username</label>
                            </div>
                            <div className="txt_field">
                                <input type="password" 
                                required 
                                 autoComplete='on'
                                 onChange={(e) => this.setPassword(e)}/>
                                <span></span>
                                <label>Password</label>
                            </div>
                            <div className="pass">Forgot Password?</div>
                            <input type="submit" value="Login" onClick={(e)=>this.onSubmit()}/>
                            <div className="signup_link">
                                Not a member? <a href="#">Signup</a>
                            </div>
                        </form>
                    </div>
                </main>


            </div>
        )
    }
}
