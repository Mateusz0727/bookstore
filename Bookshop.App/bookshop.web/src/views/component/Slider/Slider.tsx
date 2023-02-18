import { Component, ReactNode } from "react";
import { BookForm } from "../../../services/book.service";

interface BookProp
{
  books:BookForm[];
}
export default class Slider extends Component<BookProp>
  {
     onClickPrev(): void {
     
    }
    render(): ReactNode {
      

      function prev(): void {
        const productContainers = document.querySelectorAll('.product-container');
        productContainers.forEach((item, i) => {
          let containerDimensions = item.getBoundingClientRect();
          let containerWidth = containerDimensions.width;
          item.scrollLeft -= containerWidth;
        })
      }
      function next(): void {
        const productContainers = document.querySelectorAll('.product-container');
        productContainers.forEach((item, i) => {
          let containerDimensions = item.getBoundingClientRect();
          let containerWidth = containerDimensions.width;
          item.scrollLeft += containerWidth;
        })
      }

      return (
        <div>
          <div className="pre-btn" onClick={prev}>
            <i className="fa-solid fa-chevron-right"></i>          
          </div>
          <div className="nxt-btn" onClick={next}>
            <i className="fa-solid fa-chevron-right"></i>
          </div>     
          <div className="product-container">
            {this.props.books.map((item)=>
              <div className="product-card" key={item.id}>
                <div className="product-image" >
                  <span className="discount-tag">50% off</span>
                  <img src="k1.jpeg" className="product-thumb" alt="" />
                  <button className="card-btn" >add to wishlist</button>
                </div>
                <div className="product-info">
                  <h2 className="product-brand">{item.title}</h2>
                  <p className="produ`ct-short-description">{item.describe}</p>
                  <span className="price">${item.price}</span><span className="actual-price">$40</span>
                </div>   
              </div>
            )}
            {/* <div className="product-card">
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
            </div> */}
              
          </div>
        </div>
      );
    }
    
  }

