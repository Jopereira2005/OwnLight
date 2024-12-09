import ReactDOM from "react-dom/client"
import "./global.scss"
import { BrowserRouter } from "react-router-dom"
import { AuthProvider } from './context/authContext';
import MainRoutes from './routes'

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <BrowserRouter>
    <AuthProvider>
      <MainRoutes />
    </AuthProvider>
  </BrowserRouter>
)