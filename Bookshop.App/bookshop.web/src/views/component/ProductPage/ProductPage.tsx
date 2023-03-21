import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import BookService, { BookForm } from '../../../services/book/book.service';
import './ProductPage.css'
import { Header } from '../Header/Header';
import { Foter } from '../Foter/Foter';



interface RouteParams {
    id: string;
    [key: string]: string | undefined;
}


export default function ProductPage() {
    const [item, setItem] = useState<BookForm>({
        id: 0,
        title: '',
        autor: '',
        describe: '',
        price: 0,
        publishingHouse: '',
        imageUrl: '',
        isDiscount: false,
        discount:0
    });
    const { id } = useParams<RouteParams>();

    useEffect(() => {
        if (id) {
            BookService.get(id).then(response => {
                setItem(response);
            });
        }
    }, [id]);

    return (
        <div>
            <Header />

            <main className='main2'>
                <div className="product-image2">
                    <img src={item.imageUrl} className="product-thumb" alt="" />
                </div>
                <div className="product-info2">
                    <h1>{item.title}</h1>
                    <h3>{item.autor}</h3>
                    <span className="price">{item.price} zł</span>

                    <p className='des_Product'>{item.describe}</p>

                    <form className='but_Product'>

                        <button type="submit" >Dodaj do Koszyka</button>
                    </form>
                </div>
            </main>
            <Foter />


        </div>
    );
}
