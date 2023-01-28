import React, { Component } from 'react'
import WeatherService, { WeatherForecastForm } from '../../../services/weather.service';

export default class Dashboard extends Component {
    private items: WeatherForecastForm[]=[];
    componentDidMount(){
        try{
            WeatherService.get().then((response)=>{
            this.setState({itemki:response})
            this.items = response;
           
           }
           );
         
         }
         catch (ex)
         {
             console.log(ex);        
            
         }
      }
    
  render() {
    return (
      <div>  { this.items.map((item,index)=>
        <div key={index}>{item.summary}</div>  
      )}</div>
    )
  }
}
