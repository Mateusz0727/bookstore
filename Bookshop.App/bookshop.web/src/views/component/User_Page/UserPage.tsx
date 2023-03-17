import { Component, useState } from "react";
import '../../../index3.css';
import BookService, { BookForm } from "../../../services/book.service";
import { Link } from 'react-router-dom';
import AuthService, { LoginModel } from '../../../services/auth/auth.service';
import authService from "../../../services/auth/auth.service";









export default class UserPage extends Component {
    private items: BookForm[] = [];
    componentDidMount() {
        try {
            BookService.getAll().then((response) => {
                this.setState({ itemki: response })
                this.items = response;

            }
            );

        }
        catch (ex) {
            console.log(ex);

        }


    }
    wyloguj() { authService.logout() }

    render() {

        return (

            <div>

                <div className="container">
                    <div className="sidebar">
                        <div id="logo"><img src="logo1.png" alt="jeszcze_nic" /></div>

                        <div className="user-info">
                            <img className="user-avatar" src="avatar.png" alt="User avatar" />
                            <h2 className="user-name">John Doe</h2>
                        </div>

                        <ul>
                            <li><Link to="/" className="butnav">Główna</Link></li>
                            <li><Link to="/SubPage" className="butnav">Księgarnia</Link></li>
                            <li><Link to="/Bag" className="butnav">Koszyk</Link></li>
                            <li><Link to="/" className="butnav" onClick={(e) => this.wyloguj()}>Wyloguj</Link></li>
                        </ul>
                    </div>
                    <main>
                        <h1>Twoje zakupione Ksiązki </h1>

                        <div>
                            {this.items.map((item) =>
                                <div className="product-card" key={item.id}>
                                    <div className="product-image">
                                        <img src={item.imageUrl} className="product-thumb" alt="" />
                                    </div>
                                    <div className="product-info">
                                        <h3 className="product-brand">{item.title}</h3>
                                    </div>
                                </div>
                            )}
                        </div>
                    </main>
                </div>
            </div>
        )
    }
}



