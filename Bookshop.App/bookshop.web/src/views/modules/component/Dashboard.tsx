import React, { Component } from 'react'
import WeatherService, { WeatherForecastForm } from '../../../services/weather.service';

export default class Dashboard extends Component {
  private items: WeatherForecastForm[] = [];
  componentDidMount() {
    try {
      WeatherService.get().then((response) => {
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
      <body>
        <header>
          <div id="logo"><img src="logo1.png" alt="jeszcze_nic" /></div>

          <div className="search-box">
            <input type="text" placeholder="Type to search..." />
            <div className="search-btn">
              <i className="fas fa-search"></i>
            </div>

            <div className="cancel-btn">
              <i className="fas fa-times"></i>
            </div>
          </div>

          <nav>
            <a href="http://" className="butnav">Home</a>
            <a href="http://" className="butnav">Popular</a>
            <a href="http://" className="butnav">Bag</a>
            <a href="http://" className="butnav">Contact</a>
          </nav>


        </header>
        {/* <script>
        const searchBtn = document.querySelector(".search-btn");
        const cancelBtn = document.querySelector(".cancel-btn");
        const searchBox = document.querySelector(".search-box");

        searchBtn.onclick = () => {
            searchBox.classList.add("active");
        }

        cancelBtn.onclick = () => {
            searchBox.classList.remove("active");
        }
    </script> */}




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
            <div className="product-container">
              <div className="product-card">
                <div className="product-image">
                  <span className="discount-tag">50% off</span>
                  <img src="k1.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn">add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">brand</h2>
                  <p className="product-short-description">a short line about the cloth..</p>
                  <span className="price">$20</span><span className="actual-price">$40</span>
                </div>
              </div>
              <div className="product-card">
                <div className="product-image">
                  <span className="discount-tag">50% off</span>
                  <img src="k2.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn">add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">brand</h2>
                  <p className="product-short-description">a short line about the cloth..</p>
                  <span className="price">$20</span><span className="actual-price">$40</span>
                </div>
              </div>
              <div className="product-card">
                <div className="product-image">
                  <span className="discount-tag">50% off</span>
                  <img src="k3.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn">add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">brand</h2>
                  <p className="product-short-description">a short line about the cloth..</p>
                  <span className="price">$20</span><span className="actual-price">$40</span>
                </div>
              </div>
              <div className="product-card">
                <div className="product-image">
                  <span className="discount-tag">50% off</span>
                  <img src="k1.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn">add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">brand</h2>
                  <p className="product-short-description">a short line about the cloth..</p>
                  <span className="price">$20</span><span className="actual-price">$40</span>
                </div>
              </div>
              <div className="product-card">
                <div className="product-image">
                  <span className="discount-tag">50% off</span>
                  <img src="k1.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn">add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">brand</h2>
                  <p className="product-short-description">a short line about the cloth..</p>
                  <span className="price">$20</span><span className="actual-price">$40</span>
                </div>
              </div>
              <div className="product-card">
                <div className="product-image">
                  <span className="discount-tag">50% off</span>
                  <img src="k2.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn">add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">brand</h2>
                  <p className="product-short-description">a short line about the cloth..</p>
                  <span className="price">$20</span><span className="actual-price">$40</span>
                </div>
              </div>
              <div className="product-card">
                <div className="product-image">
                  <span className="discount-tag">50% off</span>
                  <img src="k1.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn">add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">brand</h2>
                  <p className="product-short-description">a short line about the cloth..</p>
                  <span className="price">$20</span><span className="actual-price">$40</span>
                </div>
              </div>
              <div className="product-card">
                <div className="product-image">
                  <span className="discount-tag">50% off</span>
                  <img src="k1.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn">add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">brand</h2>
                  <p className="product-short-description">a short line about the cloth..</p>
                  <span className="price">$20</span><span className="actual-price">$40</span>
                </div>
              </div>
              <div className="product-card">
                <div className="product-image">
                  <span className="discount-tag">50% off</span>
                  <img src="k1.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn">add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">brand</h2>
                  <p className="product-short-description">a short line about the cloth..</p>
                  <span className="price">$20</span><span className="actual-price">$40</span>
                </div>
              </div>
              <div className="product-card">
                <div className="product-image">
                  <span className="discount-tag">50% off</span>
                  <img src="k2.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn">add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">brand</h2>
                  <p className="product-short-description">a short line about the cloth..</p>
                  <span className="price">$20</span><span className="actual-price">$40</span>
                </div>
              </div>
            </div>
          </section>

          {/* <script>
            const productContainers = [...document.querySelectorAll('.product-container')];
            const nxtBtn = [...document.querySelectorAll('.nxt-btn')];
            const preBtn = [...document.querySelectorAll('.pre-btn')];

            productContainers.forEach((item, i) => {
                let containerDimensions = item.getBoundingClientRect();
                let containerWidth = containerDimensions.width;

                nxtBtn[i].addEventListener('click', () => {
                    item.scrollLeft += containerWidth;
                })

                preBtn[i].addEventListener('click', () => {
                    item.scrollLeft -= containerWidth;
                })
            })
        </script> */}
        </main>

        <footer>

        </footer>

      </body>
    )
  }
}
