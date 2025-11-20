import { useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import "./App.css";
import "react-toastify/dist/ReactToastify.css";

import { LoginPage } from "./Pages/LoginPage";
import { Route, Routes } from "react-router-dom";
import { RegisterPage } from "./Pages/RegisterPage";
import { MainPage } from "./Pages/MainPage";
import { ProtectedRoute } from "./Components/ProtectedRoute";

function App() {
  const [count, setCount] = useState(0);

  return (
    <>
      <div className="max-w-full">
        <Routes>
          <Route exact path="/" element={<LoginPage />} />

          <Route path="/login" element={<LoginPage />} />
          <Route path="/register" element={<RegisterPage />} />

          <Route path="/mainpage" element={<MainPage />} />
        </Routes>
      </div>
    </>
  );
}

export default App;
