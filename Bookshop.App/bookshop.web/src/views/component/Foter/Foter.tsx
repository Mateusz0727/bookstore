import { Component, useState } from "react";
import '../../../index.css';


export const Foter = () => {
    return (
        <>
            <footer>
                <div className='col1'>
                    <h3>Newsletter</h3>
                    <form>
                        <input type="email" placeholder='Your Email Addres' required />

                        <button type="submit">SUBSCRIBE NOW</button>
                    </form>

                </div><div className='col2'>
                    <div className='foot1'>
                        <div className='numbers'>01</div>
                        <div className='textfoot'><h3>Shipping</h3><p>Worldwide shiping</p></div>
                    </div>

                    <div className='foot2'>
                        <div className='numbers'>02</div>
                        <div className='textfoot'><h3>Best Price</h3><p>Best price with best quality</p></div>
                    </div>

                    <div className='foot3'>
                        <div className='numbers'>03</div>
                        <div className='textfoot'><h3>Authors</h3><p>Authors from around the world</p></div>
                    </div>


                </div>
            </footer>
        </>

    )
}

