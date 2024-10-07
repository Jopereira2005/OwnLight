import style from'./style.module.scss';

interface InputProps {
  name: string;
  type: string;
}

const Input = ({ name, type }: InputProps) => {
  return (
    <div className={ style.input_group }>
      <input type={type} required />
      <label>{name}</label>
    </div>
  )
}

export default Input