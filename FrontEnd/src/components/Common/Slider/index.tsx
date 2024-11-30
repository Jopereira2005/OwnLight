import { Swiper, SwiperProps } from 'swiper/react'
import { Navigation, Pagination, A11y } from 'swiper/modules';

import './style.scss'
import 'swiper/scss'
import 'swiper/scss/navigation'
import 'swiper/scss/pagination'

const Slider = ({ children }:any) => {
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
        { children }
      </Swiper>
    </div>
  )
}

export default Slider

