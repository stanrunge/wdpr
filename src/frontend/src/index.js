import React from 'react';
import ReactDOM from 'react-dom/client';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import Landing from './pages/Landing';
import Error from './pages/Error';
import Informatie from './pages/Informatie';
import Uitvoering from './pages/Uitvoering';
import Kalender from "./pages/Kalender";
import Account from "./pages/Account";
import './styles.css';

const router = createBrowserRouter([{
    path: "/",
    element: <Landing />,
    errorElement: <Error />,
  },
  {
    path: "informatie",
    element: <Informatie />,
  },
  {
    path: "uitvoering/:id",
    element: <Uitvoering />,
  },
  {
    path: "kalender",
    element: <Kalender />,
  },
  {
    path: "account",
    element: <Account />,
  }
]);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);