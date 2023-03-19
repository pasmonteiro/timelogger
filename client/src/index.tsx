import * as React from "react";
import { StrictMode } from 'react';
import * as ReactDOM from "react-dom";
import { BrowserRouter } from 'react-router-dom';
import Application from "./app/App";

ReactDOM.render(
    <StrictMode>
    <BrowserRouter>
      <Application />
    </BrowserRouter>
  </StrictMode>, document.getElementById("root"));
