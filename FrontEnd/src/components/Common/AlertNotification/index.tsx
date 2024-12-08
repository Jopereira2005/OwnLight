import './style.scss'

import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';

interface AlertNotificationProps {
  state: boolean,
  message: string, 
  timeDuration: number,
  type: 'error' | 'success',
  handleClose: () => void
}

const AlertNotification = ({ state, message, timeDuration, type, handleClose}: AlertNotificationProps) => {
  return (
    <Snackbar 
      anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
      open={ state }
      onClose={ handleClose }
      autoHideDuration={ timeDuration } 
    >    
      <Alert
        className={ type }
        severity={ type }
        onClose={ handleClose }
        variant="filled"
        sx={{ width: '100%' }}
      >
      { message }
      </Alert>
    </Snackbar>
  )
}

export default AlertNotification