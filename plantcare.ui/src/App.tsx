import React from 'react';
import './App.css';
import BaseLayout from "./common/Layouts/baseLayout/baseLayout";
import {Typography} from "@mui/material";
import MainRouting from "./app/routing/mainRouting";
import {BrowserRouter} from "react-router-dom";

function App() {
  return (
      <BrowserRouter>
        <div className="App">
            <MainRouting />
        </div>
      </BrowserRouter>
  );
}

export default App;
