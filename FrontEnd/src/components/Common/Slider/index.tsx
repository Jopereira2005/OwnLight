import { Swiper, SwiperProps } from 'swiper/react'
import { Navigation, Pagination, A11y } from 'swiper/modules';
import { Swiper as SwiperCore } from "swiper/types";

import './style.scss'
import 'swiper/scss'
import 'swiper/scss/navigation'
import 'swiper/scss/pagination'

interface SliderProps {
  children: React.ReactNode;
  onSlideChangeFunc: (id_room: number | null) => void; // Função de callback
}

const Slider = ({ children, onSlideChangeFunc }: SliderProps) => {

  const settings: SwiperProps = {
    speed: 1000,
    spaceBetween: 10,
    centeredSlides: true,
    pagination: { 
      dynamicBullets: true,
      clickable: true
    },
    slidesPerView: 3,
    loop: true,
    grabCursor: true,

  }

  const handleSlideChange = (swiper: SwiperCore) => {
    const activeSlide = swiper.slides[swiper.activeIndex];
    const activeRoom = activeSlide.getAttribute('data-id');
    onSlideChangeFunc(Number(activeRoom));
  };

  return (
    <div className='slider-container'>
      <Swiper onSlideChangeTransitionEnd={ handleSlideChange } modules={[Navigation, Pagination, A11y]} { ...settings }>
        { children }
      </Swiper>
    </div>
  )
}

export default Slider

