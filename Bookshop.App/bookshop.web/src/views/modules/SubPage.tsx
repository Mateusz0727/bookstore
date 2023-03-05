import React, { Component } from 'react'
import BookService, { BookForm } from '../../services/book.service';
import { Search } from '../component/Search/Search';
import Slider from '../component/Slider/Slider';
import { Link } from 'react-router-dom';



export default class SubPage extends Component {
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

                    <section className="product">
                        <h2 className="product-category">Nowości</h2>

                        <Slider books={this.items} type="news" />
                    </section>
                    <section className="product">
                        <h2 className="product-category">Ebooki</h2>

                        <Slider books={this.items} type="ebook" />
                    </section>

                    <section className="product">
                        <h2 className="product-category">Obcojęzyczne</h2>

                        <Slider books={this.items} type="other" />
                    </section>

                </main>

                <footer>

                </footer>

            </div>
        )
    }
}
