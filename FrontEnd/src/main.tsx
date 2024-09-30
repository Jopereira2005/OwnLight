import ReactDOM from "react-dom/client"
import "./global.scss"
import { BrowserRouter } from "react-router-dom"
import MainRoutes from './routes'

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
  <BrowserRouter>
    <MainRoutes />
  </BrowserRouter>
)