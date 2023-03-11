import React, { Component } from 'react'
import BookService, { BookForm } from '../../services/book.service';
import { Search } from '../component/Search/Search';
import { Link } from 'react-router-dom';
import { Header } from '../component/Header/Header';




export default class Bag extends Component {
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
                <Header />
                <main>




                </main>

                <footer>

                </footer>

            </div>
        )
    }
}
