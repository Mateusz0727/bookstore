import axios from "../plugins/axios";

export default class BookService {
    public static async get(): Promise<BookForm[]> {
        const result = (await axios.get("/book")).data;
        console.log(result)
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
}