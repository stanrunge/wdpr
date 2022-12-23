import React from 'react';
import ReactDOM from 'react-dom/client';
import { RouterProvider, createBrowserRouter } from 'react-router-dom';
import Landing from './pages/Landing/Landing';
import Error from './pages/Error/Error';
import Informatie from './pages/Informatie/Informatie';
import Uitvoering from './pages/Uitvoering/Uitvoering';

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
  }
]);

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
);