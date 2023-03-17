import React, { Component } from 'react'
import BookService, { BookForm } from '../../services/book.service';
import { Search } from '../component/Search/Search';
import { Link } from 'react-router-dom';
import { Header } from '../component/Header/Header';
import '../../bag.css'




export default class Bag extends Component {
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

    render() {
        return (
            <div>
                <Header />
                <main className='main_bag'>
                    <div className="cart">
                        <div className="cart-header">
                            <h3>Twój Koszyk</h3>
                        </div>
                        <ul className="cart-items">
                            <li className="cart-item">
                                <div className="cart-item-image">
                                    <img src="k1.jpeg" alt="Product" />
                                </div>
                                <div className="cart-item-details">
                                    <h4>Nazwa</h4>
                                    <p>Cena: $19.99</p>
                                </div>
                                <button className="remove-item">Usuń</button>
                            </li>
                            <li className="cart-item">
                                <div className="cart-item-image">
                                    <img src="k1.jpeg" alt="Product" />
                                </div>
                                <div className="cart-item-details">
                                    <h4>Nazwa</h4>
                                    <p>Cena: $14.99</p>
                                </div>
                                <button className="remove-item">Usuń</button>
                            </li>
                        </ul>
                        <div className="cart-footer">
                            <div className="subtotal">
                                <p>Razem: $54.97</p>
                            </div>
                            <button className="checkout-button">Zapłać</button>
                        </div>
                    </div>
                </main>





                <footer>

                </footer>

            </div>
        )
    }
}
