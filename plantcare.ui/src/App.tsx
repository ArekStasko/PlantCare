import React from 'react';
import './App.css';
import BaseLayout from "./common/Layouts/baseLayout/baseLayout";
import {createTheme, ThemeProvider, Typography} from "@mui/material";

const darkTheme = createTheme({
    palette: {
        mode: 'dark',
    },
});

function App() {
  return (
      <ThemeProvider theme={darkTheme}>
        <div className="App">
         <BaseLayout>
             <Typography>Test</Typography>
        </BaseLayout>
         </div>
      </ThemeProvider>
  );
}

export default App;
