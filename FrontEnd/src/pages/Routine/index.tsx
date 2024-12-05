import styled from './style.module.scss'

import Header from '../../components/Common/Header'
import SeachBar from '../../components/Common/SearchBar'
import NavBar from '../../components/Common/NavBar'
import Card from '../../components/Routine/Card'

import { AddIcon } from '../../assets/Home/Add'

function Routine() {
  const cards = [
    {id:1, name: "Bom dia", time: "12:00", group: "Sala", days: null, state: true},
    {id:2, name: "Acorda fio", time: "4:15", group: "Cozinha", days: ["Seg", "Ter", "Qua", "Qui", "Sex", "Sab", "Dom"], state: true},
    {id:3, name: "Gym", time: "16:00", group: "Grupo 1", days: ["Ter", "Qua", "Qui", "Sex"], state: true},
    {id:4, name: "Volei", time: "18:00", group: "Sala", days: ["Ter", "Qui"], state: true},
    {id:5, name: "Acordar tarde", time: "18:00", group: "Sala", days: ["Sab", "Dom"], state: false}
  ]

  return (
    <div className={ styled.home }>
      <Header />
      <main className={ styled.main }>
        {/* <SeachBar /> */}
        <div className={ styled.main__cards }>
          <div className={ styled.main__cards__header }>
            <h1 className={ styled.main__cards__header__text }>Rotinas</h1>
            <AddIcon className={ styled.main__cards__header__icon }/>
          </div>
          <div className={ styled.main__cards__body}>
            {cards.map((card) => (
              <Card 
                id={card.id} 
                name={ card.name } 
                time={ card.time }
                group={ card.group }
                days={ card.days }
                state={ card.state }
              />
            ))}
          </div>
        </div>
      </main>
      <NavBar />
    </div>
  )
}

export default Routine
