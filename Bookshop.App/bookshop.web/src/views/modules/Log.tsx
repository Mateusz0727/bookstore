import React, { Component } from 'react'
import BookService, { BookForm } from '../../services/book.service';
import { Search } from '../component/Search/Search';
import { Link } from 'react-router-dom';



export default class Log extends Component {
    private items: BookForm[] = [];
    componentDidMount() {
        try {
            BookService.get().then((response) => {
                this.setState({ itemki: response })
                this.items = response;

            }
            );

        }
        catch (ex) {
            console.log(ex);

        }
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
                        <form method="post">
                            <div className="txt_field">
                                <input type="text" required />

                                <label>Username</label>
                            </div>
                            <div className="txt_field">
                                <input type="password" required />
                                <span></span>
                                <label>Password</label>
                            </div>
                            <div className="pass">Forgot Password?</div>
                            <input type="submit" value="Login" />
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
