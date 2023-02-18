import { Component, useState } from "react";

export const Search =()=>
{
    
    let [isActive, setIsActive] = useState(false);
 function  active(){
      setIsActive(!isActive);
    }

    return (
        <div onClick={active} className={`search-box ${isActive ? "active" : "inactive"}`}>
        <input type="text" placeholder="Type to search..." />
        <div className="search-btn">
          <i className="fas fa-search"></i>
         
        </div>

        <div className="cancel-btn">
          <i className="fas fa-times"></i>
        </div>
      </div>
        )
    }
