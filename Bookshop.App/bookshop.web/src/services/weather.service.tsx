import axios from "../plugins/axios";

export default class WeatherService{
    public static async get() : Promise<WeatherForecast[]>
    {
        const  result = (await axios.get("/weatherforecast")).data;
        if(result)
        {
            console.log(result);
        }
        return result;
    }
}
export interface WeatherForecast
{
      Date:string ;

      TemperatureC:number ;

     TemperatureF:number ;

      Summary:string ;
}