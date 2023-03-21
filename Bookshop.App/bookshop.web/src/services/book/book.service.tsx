import axios from "../../plugins/axios";
import authHeader from "../auth/auth.header";

export default class BookService {
    public static async getAll(): Promise<BookForm[]> {
        const result = (await axios.get("/book")).data;
       
        return result;
    }
    public static async getNewBooks(): Promise<BookForm[]> {
        const result = (await axios.get("/book/new")).data;
       
        return result;
    }
    public static async getPopular(): Promise<BookForm[]> {
        const result = (await axios.get("/book/popular")).data;
        
        return result;
    }
    public static async getByLanguage(): Promise<BookForm[]> {
        const result = (await axios.get("/book/language")).data;
     
        return result;
    }
    public static async get(id: string): Promise<BookForm> {
        const result = (await axios.get(`/book/${id}`)).data;
      
        return result;
    }
    public static async getUserBook():Promise<BookForm[]>{
        const result = (await axios.get('book/userBooks',{headers:{Authorization:authHeader().toString()}})).data;
        return result;
    }
}
export interface BookForm {
    id: number,
    title: string,
    autor: string,
    describe: string,
    price: number,
    publishingHouse: string
    imageUrl: string
    isDiscount:boolean
    discount:number
}