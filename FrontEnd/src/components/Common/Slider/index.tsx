import { Swiper, SwiperSlide, SwiperProps } from 'swiper/react'
import { Navigation, Pagination, A11y } from 'swiper/modules';

import './style.scss'
import 'swiper/scss'
import 'swiper/scss/navigation'
import 'swiper/scss/pagination'

const Slider = () => {
  const settings: SwiperProps = {
    spaceBetween: 10,
    centeredSlides: true,
    pagination: { 
      dynamicBullets: true,
      clickable: true
    },
    slidesPerView: 3,
    loop: true,
  }

  return (
    <div className='slider-container'>
      <Swiper modules={[Navigation, Pagination, A11y]} { ...settings }>
        <SwiperSlide><span>Cozinha</span></SwiperSlide>
        <SwiperSlide><span>Sala</span></SwiperSlide>
        <SwiperSlide><span>Cozinha</span></SwiperSlide>
        <SwiperSlide><span>Cozinha</span></SwiperSlide>
        <SwiperSlide><span>Cozinha</span></SwiperSlide>
        <SwiperSlide><span>Cozinha</span></SwiperSlide>
        <SwiperSlide><span>Cozinha</span></SwiperSlide>
        <SwiperSlide><span>Cozinha</span></SwiperSlide>
        <SwiperSlide><span>Cozinha</span></SwiperSlide>
        <SwiperSlide><span>Cozinha</span></SwiperSlide>
      </Swiper>
    </div>
  )
}

export default Slider

