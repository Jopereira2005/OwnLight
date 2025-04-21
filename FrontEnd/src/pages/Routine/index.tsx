import styled from './style.module.scss'

import Header from '../../components/Common/Header'
import SeachBar from '../../components/Common/SearchBar'
import NavBar from '../../components/Common/NavBar'
import Card from '../../components/Routine/Card'

import { AddIcon } from '../../assets/Home/Add'
import { useState } from 'react'
import type { Routine } from '../../interfaces/Routine'
import { Room } from '../../interfaces/Room'

import EditModal from '../../components/Routine/EditModal'
import CreateModal from '../../components/Routine/CreateModal'

function Routine() {
  const _ = {  name: "", }

  const [routines, setRoutines] = useState<Routine[]>();
  const [rooms, setRooms] = useState<Room[]>([]);

  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [selectRoutineData, setSelectRoutineData] = useState<Routine>(_);


  const cards = [
    {id:1, name: "Bom dia", time: "12:00", group: "Sala", days: null, state: true},
    {id:2, name: "Acorda fio", time: "4:15", group: "Cozinha", days: ["Seg", "Ter", "Qua", "Qui", "Sex", "Sab", "Dom"], state: true},
    {id:3, name: "Gym", time: "16:00", group: "Grupo 1", days: ["Ter", "Qua", "Qui", "Sex"], state: true},
    {id:4, name: "Volei", time: "18:00", group: "Sala", days: ["Ter", "Qui"], state: true},
    {id:5, name: "Acordar tarde", time: "18:00", group: "Sala", days: ["Sab", "Dom"], state: false}
  ]

  const toggleCreateModal = () => {
    setIsCreateModalOpen(!isCreateModalOpen);
  };

  const toggleEditModal = () => {
    setIsEditModalOpen(!isEditModalOpen);
  };

  const listRoutine = async () => {
    // try {
    //   const response = await deviceService.list_device();
    //   const items = response.data?.items || [];

    //   const devices = items.filter((device: Device) => 
    //     rooms.some(room => room.id === device.roomId)
    //   );

    //   setDevices(devices);
    // } catch (error) {
    //   setDevices([]);
  }

  const deleteRoutine = async ( id: string ) => {
    // try {
    //   await deviceService.delete_device(id);
    //   toggleAlertOpen(3000, "Dispositivo deletado com sucesso", "success");
    //   listDevice()
    //   listDeviceByRoom()
    // } catch (error) {
    //   setDevices([]);
    // }
  }

  const createRoutine = async (formData: FormData) => {
    // const nome = formData.get('name');
    // const ajustavel = formData.get('dimmable'); 

    // try {
    //   await deviceService.create_device(selectRoomData.id || '', String(nome), Boolean(ajustavel));
    //   listDevice();
    //   listDeviceByRoom();
    // } catch (error) {
    //   return error;
    // }

  };

  const changeRoutineState = async( id: string) => {
    // try {
    //   await deviceService.device_switch( id );
    //   listRooms()
    // } catch(error: any) {
    //   toggleAlertOpen(3000, "Erro ao tentar mudar o estado do dispositivo.", "error")
    // }
  }

  const editRoutine = async (data: FormData, id: string) => {
    // const name = String(data.get('name')).trim();
    // const room = String(data.get('room'));
    // const brightness = Number(data.get('brightness')); 
    
    // if(selectDeviceData.name !== name )  {
    //   await deviceService.update_device_name(id, name);
    //   toggleAlertOpen(3000, "Dispositivo alterado com sucesso", "success");
    // }
    // if(selectDeviceData.roomId !== room) {
    //   await deviceService.update_device_room(id, room);
    //   toggleAlertOpen(3000, "Dispositivo alterado com sucesso", "success");
    // }

    // if(selectDeviceData.brightness !== brightness) {
    //   await deviceService.update_device_dim(id, brightness);
    //   toggleAlertOpen(3000, "Dispositivo alterado com sucesso", "success");
    // }

    // listDevice();
    // listDeviceByRoom();
  };

  const sendDeviceData = (id: string) => {
    // const selectedCard = devices.find((device: Device) => device.id === id);
    // if (selectedCard) 
    //   setSelectDeviceData(selectedCard);
  }

  return (
    <>
      <div className={ styled.home }>
        <Header />
        <main className={ styled.main }>
          {/* <SeachBar /> */}
          <div className={ styled.main__cards }>
            <div className={ styled.main__cards__header }>
              <h1 className={ styled.main__cards__header__text }>Rotinas</h1>
              <AddIcon onClick={ toggleCreateModal } className={ styled.main__cards__header__icon }/>
            </div>
            <div className={ styled.main__cards__body}>
              {cards.map((card) => (
                <Card 
                  key={card.id}
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
      
      <EditModal
        routine={ selectRoutineData }
        rooms={ rooms }
        isOpen={ isEditModalOpen }
        toggleEditModal={ toggleEditModal }
        deleteRoutineFunc={ deleteRoutine }
        onSubmit={ editRoutine }
      />

      <CreateModal 
        rooms={ rooms }
        isOpen={ isCreateModalOpen } 
        toggleCreateModal={ toggleCreateModal } 
        onSubmit={ createRoutine }
      />
    </>
  )
}

export default Routine
