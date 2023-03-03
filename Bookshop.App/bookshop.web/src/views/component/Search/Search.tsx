import { Component, useState } from "react";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSearch, faTimes } from '@fortawesome/free-solid-svg-icons'
export const Search = () => {

  let [isActive, setIsActive] = useState(false);
  function active() {
    setIsActive(!isActive);
  }

  return (
    <div className={`search-box ${isActive ? "active" : "inactive"}`}>
      <input type="text" placeholder="Type to search..." />
      <div onClick={active} className="search-btn">
        <i><FontAwesomeIcon icon={faSearch} /></i>

      </div>

      <div onClick={active} className="cancel-btn">
        <i><FontAwesomeIcon icon={faTimes} /></i>

      </div>
    </div>
  )
}
