import { Component, useState } from "react";
import '../../../index.css';
import { Link } from "react-router-dom";
import { Search } from '../../component/Search/Search';
import AuthService, { LoginModel } from '../../../services/auth/auth.service';



export const Header = () => {

    let user = AuthService.getCurrentUser();

    return (
        <header>
            <div id="logo"><img src="logo1.png" alt="jeszcze_nic" /></div>
            <Search />
            <nav>
                <Link to="/" className="butnav">Główna</Link>
                <Link to="/SubPage" className="butnav">Księgarnia</Link>
                {user ? (
                    <Link to="/UserPage" className="butnav">Profil</Link>
                ) : (
                    <Link to="/Log" className="butnav">Logowanie</Link>

                )
                }
                <Link to="/Bag" className="butnav">Koszyk</Link>
            </nav>
        </header>





    )
}
