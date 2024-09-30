import {Routes, Route} from "react-router-dom";
import Home from "./pages/Home";
import Register from "./pages/Register";
import Login from "./pages/Register";


function MainRoutes() {
  return (
    <Routes>
      <Route path="/" element={ <Home /> }/>
      <Route path="/cadastro" element={ <Register /> }/>
      <Route path="/login" element={ <Login /> }/>

    </Routes>
  );
}

export default MainRoutes;