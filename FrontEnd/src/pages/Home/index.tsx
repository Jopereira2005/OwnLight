import { useEffect, useState } from 'react';

import styled from './style.module.scss'

import Header from '../../components/Common/Header'
import SeachBar from '../../components/Common/SearchBar'
import Slider from '../../components/Common/Slider'
import NavBar from '../../components/Common/NavBar'
import Card from '../../components/Home/Card'

import CreateModal from '../../components/Home/CreateModal'
import CreateRoomModal from '../../components/Home/CreateRoomModal'
import EditModal from '../../components/Home/EditModal'
import EditRoomModal from '../../components/Home/EditRoomModal'


import { Room } from '../../interfaces/Room'
import { Device } from '../../interfaces/Device'

import { AddIcon } from '../../assets/Home/Add'

import { SwiperSlide } from 'swiper/react'

function Home() {
  const _ = {  name: "", }

  const devices = [
    { id_device:1, id_room: 1, name: "Lâmpada 1", is_dimmable: true, brightness: 60, state: true },
    { id_device:2, id_room: 1, name: "Lâmpada 2", is_dimmable: false, brightness: 20, state: false },
    { id_device:3, id_room: 1, name: "Lâmpada 3", is_dimmable: true, brightness: 20, state: false },
    { id_device:4, id_room: 2, name: "Lâmpada 4", is_dimmable: false, brightness: 10, state: false },
    { id_device:5, id_room: 2, name: "Lâmpada 5", is_dimmable: false, brightness: 10, state: false },
    { id_device:6, id_room: 2, name: "Lâmpada 6", is_dimmable: false, brightness: 20, state: false },
    { id_device:7, id_room: 3, name: "Lâmpada 7", is_dimmable: false, brightness: 50, state: false },
    { id_device:8, id_room: 3, name: "Lâmpada 8", is_dimmable: true, brightness: 20, state: false },
    { id_device:9, id_room: 3,  name: "Lâmpada 9", is_dimmable: false, brightness: 100, state: false },
    { id_device:10, id_room: 4, name: "Lâmpada 10", is_dimmable: false, brightness: 100, state: false },
    { id_device:11, id_room: 4, name: "Lâmpada 11", is_dimmable: false, brightness: 100, state: false },
    { id_device:12, id_room: 4, name: "Lâmpada 12", is_dimmable: false, brightness: 100, state: false },
    { id_device:13, id_room: 5, name: "Lâmpada 13", is_dimmable: true, brightness: 100, state: false },
    { id_device:14, id_room: 5, name: "Lâmpada 14", is_dimmable: true, brightness: 100, state: false },
    { id_device:15, id_room: 5, name: "Lâmpada 15", is_dimmable: true, brightness: 100, state: false },
    { id_device:16, id_room: 5, name: "Lâmpada 16", is_dimmable: true, brightness: 100, state: false },
    { id_device:17, id_room: 5, name: "Lâmpada 17", is_dimmable: true, brightness: 100, state: false },
    { id_device:18, id_room: 5, name: "Lâmpada 18", is_dimmable: true, brightness: 100, state: false },
    { id_device:19, id_room: 5, name: "Lâmpada 19", is_dimmable: true, brightness: 100, state: false },
    { id_device:20, id_room: 5, name: "Lâmpada 20", is_dimmable: true, brightness: 100, state: false },
    { id_device:21, id_room: 5, name: "Lâmpada 21", is_dimmable: true, brightness: 100, state: false },
    { id_device:22, id_room: 5, name: "Lâmpada 22", is_dimmable: true, brightness: 100, state: false },
    { id_device:23, id_room: 5, name: "Lâmpada 23", is_dimmable: true, brightness: 100, state: false },
    { id_device:24, id_room: 5, name: "Lâmpada 24", is_dimmable: true, brightness: 100, state: false },
    { id_device:25, id_room: 5, name: "Lâmpada 25", is_dimmable: true, brightness: 100, state: false },
    { id_device:26, id_room: 5, name: "Lâmpada 26", is_dimmable: true, brightness: 100, state: false },
    { id_device:27, id_room: 5, name: "Lâmpada 27", is_dimmable: true, brightness: 100, state: false },
    { id_device:28, id_room: 5, name: "Lâmpada 28", is_dimmable: true, brightness: 100, state: false },
    { id_device:29, id_room: 5, name: "Lâmpada 29", is_dimmable: true, brightness: 100, state: false },
    { id_device:30, id_room: 5, name: "Lâmpada 30", is_dimmable: true, brightness: 100, state: false },
    { id_device:31, id_room: 5, name: "Lâmpada 31", is_dimmable: true, brightness: 100, state: false },
    { id_device:32, id_room: 5, name: "Lâmpada 32", is_dimmable: true, brightness: 100, state: false },
  ]

  const rooms = [
    { id_room: 1, name: "Sala" },
    { id_room: 2, name: "Cozinha" },
    { id_room: 3, name: "Banheiro" },
    { id_room: 4, name: "Lavanderia" },
    { id_room: 5, name: "Quarto" },
  ]

  const [pressTimer, setPressTimer] = useState<NodeJS.Timeout | null>(null);

  const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
  const [isCreateRoomModalOpen, setIsCreateRoomModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [isEditRoomModalOpen, setIsEditRoomModalOpen] = useState(false);

  const [selectDeviceData, setSelectDeviceData] = useState<Device>(_);
  const [selectRoomData, setSelectRoomData] = useState<Room>(rooms[0] || _);
  const [selectRoomEditData, setSelectRoomEditData] = useState<Room>(_);

  const[filteredDevices, setfilteredDevices] = useState<Device[]>([]);

  const toggleStateDevice = ( id_device: number, state: boolean ) => {
    console.log("Request Enviada: " + id_device);
    console.log("Estado: " + state);
  }

  const toggleCreateModal = () => {
    setIsCreateModalOpen(!isCreateModalOpen);
  };

  const toggleCreateRoomModal = () => {
    setIsCreateRoomModalOpen(!isCreateRoomModalOpen);
  };

  const toggleEditModal = () => {
    setIsEditModalOpen(!isEditModalOpen);
  };

  const toggleEditRoomModal = () => {
    setIsEditRoomModalOpen(!isEditRoomModalOpen);
  };

  const createDevice = (formData: FormData) => {
    const nome = formData.get('name');
    const ajustavel = formData.get('dimmable'); 
    const sala = formData.get('room');
    console.log('Dados enviados:', { nome, ajustavel, sala });
  };

  const createRoom = (formData: FormData) => {
    const nome = formData.get('name');
    console.log('Dados enviados:', { nome });
  };

  const editDevice = (data: FormData | number) => {
    if (data instanceof FormData) {
      const nome = data.get('name');
      const ajustavel = data.get('dimmable'); 
      const sala = data.get('room');
      const brilho = data.get('brightness');
      console.log('Dados enviados (Edit):', { nome, ajustavel, sala, brilho });
    } else {
      const id_device = data;
      console.log('Dados enviados (Delete):', id_device);
    }
  };

  const editRoom = (data: FormData | number) => {
    if (data instanceof FormData) {
      const nome = data.get('name');
      console.log('Dados enviados (Edit):', { nome });
    } else {
      const id_room = data;
      console.log('Dados enviados (Delete):', id_room);
    }
  };

  const sendDeviceData = (id_device: number) => {
    const selectedCard = devices.find((device: Device) => device.id_device === id_device);
    if (selectedCard) 
      setSelectDeviceData(selectedCard);
  }

  const sendRoomData = (id_room: number | null) => {
    const selectedRoom = rooms.find((room: Room) => room.id_room === id_room);
    if(selectedRoom !== selectRoomData) 
      setSelectRoomData(selectedRoom || _);  
  }

  const filterDevices = (id_room: number, devices: Device[]) => {
    const filteredDevices = devices.filter(
      (device) => device.id_room === id_room
    );
    setfilteredDevices(filteredDevices);
  }

  const handleMouseDown = (room: Room) => {
    const timer = setTimeout(() => {
      setSelectRoomEditData(room || _)
      toggleEditRoomModal()
    }, 1500);
    setPressTimer(timer);
  };

  const handleMouseUp = () => {
    if(pressTimer) {
      clearTimeout(pressTimer);
      setPressTimer(null);
    }
  };

  useEffect(() => {
    filterDevices(selectRoomData.id_room || 0, devices)
    console.log(selectRoomData);
  }, [selectRoomData]);

  return (
    <div className={ styled.home }>
      <Header />
      <main className={ styled.main }>
        <SeachBar 
          list={ devices }
          handleStateFunc={ toggleStateDevice }
          handleModalFunc={ toggleEditModal  }
          sendData={ sendDeviceData }
        />
        <div className={ styled.main__carousel }>
          <Slider onSlideChangeFunc={ sendRoomData }>
            { rooms.map((room) => (
              <SwiperSlide
                key={ room.id_room }
                data-id={ room.id_room }
                
              ><button
                onMouseDown={() => handleMouseDown(room)}
                onMouseUp={ handleMouseUp }
                onMouseLeave={ handleMouseUp }
                onTouchStart={ () => handleMouseDown(room) }
                onTouchEnd={ handleMouseUp }
              >{ room.name }</button>
              </SwiperSlide>
            ))}
            <SwiperSlide><button className="create_button" onClick={ toggleCreateRoomModal }>Criar <AddIcon className="create_button__icon"/></button></SwiperSlide>
          </Slider>
        </div>
        <div className={ styled.main__cards }>
          <div className={ styled.main__cards__header }>
            { selectRoomData.name !== '' ? 
               <h1 className={ styled.main__cards__header__text }>{ selectRoomData.name }</h1> : 
               <h1 className={ styled.main__cards__menssage} >Selecione algum dispositivo ;)</h1>
            }
                
            { selectRoomData.name !== '' && <AddIcon onClick={ toggleCreateModal } className={ styled.main__cards__header__icon }/>}
             
          </div>
            { devices.length == 0 ? 
              <h1 className={ styled.main__cards__menssage} >Cadastre algum dispositivo para começar ;)</h1>
            : undefined}
          <div className={ styled.main__cards__body }>
            { filteredDevices.map((device) => (
              <Card 
                id={ device.id_device || 0} 
                key={ device.id_device } 
                name={ device.name } 
                state={ device.state || false}
                onClickFunc = { toggleEditModal }
                sendData={ sendDeviceData }
              />
            ))}
          </div>
        </div>
      </main>
      <NavBar />
      <CreateModal 
        rooms={ rooms } 
        isOpen={ isCreateModalOpen } 
        toggleCreateModal={ toggleCreateModal } 
        onSubmit={ createDevice }
      />

      <CreateRoomModal 
        isOpen={ isCreateRoomModalOpen } 
        toggleCreateRoomModal={ toggleCreateRoomModal } 
        onSubmit={ createRoom }
      />

      <EditModal
        device={ selectDeviceData }
        rooms={ rooms }
        isOpen={ isEditModalOpen }
        toggleEditModal={ toggleEditModal }
        onSubmit={ editDevice }
      />

      <EditRoomModal
        room={ selectRoomEditData }
        isOpen={ isEditRoomModalOpen }
        toggleEditRoomModal={ toggleEditRoomModal }
        onSubmit={ editRoom }
      />
    </div>
  )
}

export default Home
