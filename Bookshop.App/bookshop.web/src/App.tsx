import React from 'react';
import './App.css';
import Dashboard from './views/modules/Dashboard';
import SubPage from './views/modules/SubPage';
import Log from './views/modules/Log';
import Bag from './views/modules/Bag';
import UserPage from './views/component/User_Page/UserPage';



import { Route, Routes } from 'react-router-dom';


function App() {

  return (
    <div className="App">
      <Routes>
        <Route path='/' element={<Dashboard />} />
        <Route path='/SubPage' element={<SubPage />} />
        <Route path='/Log' element={<Log />} />
        <Route path='/Bag' element={<Bag />} />
        <Route path='/UserPage' element={<UserPage />} />

      </Routes>
    </div>
  );
}


export default App;
