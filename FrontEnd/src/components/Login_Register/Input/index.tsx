import style from'./style.module.scss';

interface InputProps {
  name: string;
  label: string;
  type: string;
  value: string;
  onChangeFunc: any
}

const Input = ({ name, label, type, value, onChangeFunc}: InputProps) => {
  return (
    <div className={ style.input_group }>
      <input name={ name } type={ type } value={ value } onChange={ onChangeFunc } required />
      <label htmlFor={ name }>{ label }</label>
    </div>
  )
}

export default Input