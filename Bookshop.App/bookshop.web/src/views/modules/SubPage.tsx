import React, { Component } from 'react'
import BookService, { BookForm } from '../../services/book/book.service';
import Slider from '../component/Slider/Slider';
import { Header } from '../component/Header/Header';




export default class SubPage extends Component {
    private newBooks: BookForm[] = [];
    private popularBooks: BookForm[]=[];
    private booksByLanguage: BookForm[]=[];
    componentDidMount() {
        try {
            BookService.getNewBooks().then((response) => {
                this.setState({ itemki: response })
                this.newBooks = response;

                }
            );
            BookService.getPopular().then((response) => {
                this.setState({ itemki: response })
                this.popularBooks = response;

                }
            );
            BookService.getByLanguage().then((response) => {
                this.setState({ itemki: response })
                this.booksByLanguage = response;

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
                <main>

                    <section className="product">
                        <h2 className="product-category">Nowości</h2>

                        <Slider books={this.newBooks} type="news" />
                    </section>
                    <section className="product">
                        <h2 className="product-category">Popularne</h2>

                        <Slider books={this.popularBooks} type="ebook" />
                    </section>

                    <section className="product">
                        <h2 className="product-category">Obcojęzyczne</h2>

                        <Slider books={this.booksByLanguage} type="other" />
                    </section>

                </main>

                <footer>

                </footer>

            </div>
        )
    }
}
