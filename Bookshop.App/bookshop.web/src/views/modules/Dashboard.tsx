import React, { Component } from 'react'
import BookService, { BookForm } from '../../services/book/book.service';
import Slider from '../component/Slider/Slider';
import { Foter } from '../component/Foter/Foter';
import { Header } from '../component/Header/Header';





export default class Dashboard extends Component {
  private items: BookForm[] = [];
  componentDidMount() {
    try {
      BookService.getPopular().then((response) => {
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
      <section>

        <Header />


        <main>
          <div id="powitalny">
            <h2>Witaj w Bibliotece KMMK</h2><br />

            <h5 id="tekst_boczny">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum
              has been the
              industry's standard dummy text ever since the 1500s</h5>

          </div>
          <section className="product">
            <h2 className="product-category">Popularne ostatnio</h2>

            <Slider books={this.items} type="main" />


          </section>


        </main>

        <Foter />


      </section >
    )
  }
}


