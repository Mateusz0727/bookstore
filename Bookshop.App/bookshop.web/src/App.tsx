import React from 'react';
import logo from './logo.svg';
import './App.css';
import WeatherService from './services/weather.service';
import Dashboard from './views/modules/component/Dashboard';

function App() {

  return (
    <div className="App">
    <Dashboard/>
    </div>
  );
}

export default App;
