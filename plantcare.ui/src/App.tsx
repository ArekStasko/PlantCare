import React from 'react';
import './App.css';
import BaseLayout from "./common/Layouts/baseLayout/baseLayout";
import {Typography} from "@mui/material";

function App() {
  return (
    <div className="App">
      <BaseLayout>
          <Typography>Test</Typography>
      </BaseLayout>
    </div>
  );
}

export default App;
