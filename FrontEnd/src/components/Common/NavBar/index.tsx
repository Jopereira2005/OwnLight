import { useState } from "react"
import { useNavigate, useLocation } from 'react-router-dom';

import styled from "./style.module.scss"

import { DashboardIcon } from "../../../assets/NavBar/Dashboard"
import { HomeIcon } from "../../../assets/NavBar/Home"
import { RoutineIcon } from "../../../assets/NavBar/Routine"

const NavBar = () => {
  const navigate = useNavigate();
  const location = useLocation();

  const [page, setPage] = useState(location.pathname);

  const handlePage = (url: string) => {
    setPage(url);
    navigate(url);
  }

  return (
    <div className={ styled.container }>
      <nav className={ styled.navbar }>
        <div onClick={() => handlePage('/rotina')} className={ page == "/rotina" ? styled.navbar__item_active : styled.navbar__item }>
          <RoutineIcon className={ styled.icon }/>
          <h1 className={ styled.text }>Rotina</h1>
        </div>
        
        <div onClick={() => handlePage('/')} className={ page == "/" ? styled.navbar__item_active : styled.navbar__item }>
          <HomeIcon className={ styled.icon }/>
          <h1 className={ styled.text }>Home</h1>
        </div>

        <div onClick={() => handlePage('/')} className={ page == "dashboard" ? styled.navbar__item_active : styled.navbar__item }>
          <DashboardIcon className={ styled.icon }/>
          <h1 className={ styled.text }>Dashboard</h1>
        </div>
      </nav>
    </div>
  )
}

export default NavBar