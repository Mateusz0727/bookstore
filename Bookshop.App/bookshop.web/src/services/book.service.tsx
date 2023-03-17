import axios from "../plugins/axios";

export default class BookService {
    public static async getAll(): Promise<BookForm[]> {
        const result = (await axios.get("/book")).data;
        console.log(result)
        return result;
    }
    public static async get(id: string): Promise<BookForm> {
        const result = (await axios.get(`/book/${id}`)).data;
        console.log(result);
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