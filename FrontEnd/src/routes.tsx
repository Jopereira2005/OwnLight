import {Routes, Route} from "react-router-dom";
import Home from "./pages/Home";
import Routine from "./pages/Routine";
import Register from "./pages/Register";
import Login from "./pages/Login";



function MainRoutes() {
  return (
    <Routes>
      <Route path="/" element={ <Home /> }/>
      <Route path="/rotina" element={ <Routine /> }/>
      <Route path="/cadastro" element={ <Register /> }/>
      <Route path="/login" element={ <Login /> }/>
    </Routes>
  );
}

export default MainRoutes;