import React, { Component } from 'react'
import BookService, { BookForm } from '../../services/book.service';
import { Search } from '../component/Search/Search';
import Slider from '../component/Slider/Slider';



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
          <Search/>
          <nav>
            <a href="http://" className="butnav">Home</a>
            <a href="http://" className="butnav">Popular</a>
            <a href="http://" className="butnav">Bag</a>
            <a href="http://" className="butnav">Contact</a>
          </nav>
        </header>
        <main>
          <div id="powitalny">
            <h2>Witaj w Bibliotece kmmk</h2><br />
            <h5 id="tekst_boczny">Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum
              has been the
              industry's standard dummy text ever since the 1500s</h5>

          </div>
          <section className="product">
            <h2 className="product-category">Popularne ostatnio</h2>
            <div className="pre-btn">
              
              <i className="fa-solid fa-chevron-right"></i>
            </div>
            <div className="nxt-btn">
              
              <i className="fa-solid fa-chevron-right"></i>
            </div>
            <Slider  books={this.items} /> 

            
          </section>
        
         
        </main>

        <footer>

        </footer>

      </div>
    )
  }
}
