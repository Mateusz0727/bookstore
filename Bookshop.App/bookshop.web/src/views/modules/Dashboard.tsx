import React, { Component } from 'react'
import BookService, { BookForm } from '../../services/book.service';
import { Search } from '../component/Search/Search';
import Slider from '../component/Slider/Slider';
import { Foter } from '../component/Foter/Foter';
import { Link } from 'react-router-dom';




export default class Dashboard extends Component {
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
        <hr />
        <main>
          <div id="powitalny">
            <h2>Witaj w Bibliotece KMMK</h2><br />

            <h5 id="tekst_boczny">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum
              has been the
              industry's standard dummy text ever since the 1500s</h5>

          </div>
          <section className="product">
            <h2 className="product-category">Popularne ostatnio</h2>

            <Slider books={this.items} />


          </section>


        </main>

        <Foter />


      </div >
    )
  }
}


