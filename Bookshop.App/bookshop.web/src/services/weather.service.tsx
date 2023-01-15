import axios from "../plugins/axios";

export default class WeatherService{
    public static async get() : Promise<WeatherForecastForm[]>
    {
        const  result = (await axios.get("/weatherforecast")).data;
        if(result)
        {
            console.log(result);
        }
        return result;
    }
}
export interface WeatherForecastForm
{
      Date:string ;

      TemperatureC:number ;

     TemperatureF:number ;

      Summary:string ;
}