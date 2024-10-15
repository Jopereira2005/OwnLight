import { useState } from "react"

import styled from "./style.module.scss"

import { DashboardIcon } from "../../../assets/NavBar/Dashboard"
import { HomeIcon } from "../../../assets/NavBar/Home"
import { RoutineIcon } from "../../../assets/NavBar/Routine"

const NavBar = () => {
  const pages = [
    {id: 1, name: "routine"},
    {id: 2, name: "home"},
    {id: 3, name: "dashboard"},
  ]

  const [page, setPage] = useState(pages[1].name);

  const handlePage = (id: number) => {
    setPage(pages[id].name);
  }

  return (
    <div className={ styled.container }>
      <nav className={ styled.navbar }>
        <div onClick={() => handlePage(0)} className={ page == "routine" ? styled.navbar__item_active : styled.navbar__item }>
          <RoutineIcon className={ styled.icon }/>
          <h1 className={ styled.text }>Routine</h1>
        </div>
        
        <div onClick={() => handlePage(1)} className={ page == "home" ? styled.navbar__item_active : styled.navbar__item }>
          <HomeIcon className={ styled.icon }/>
          <h1 className={ styled.text }>Home</h1>
        </div>

        <div onClick={() => handlePage(2)} className={ page == "dashboard" ? styled.navbar__item_active : styled.navbar__item }>
          <DashboardIcon className={ styled.icon }/>
          <h1 className={ styled.text }>Dashboard</h1>
        </div>
      </nav>
    </div>
  )
}

export default NavBar