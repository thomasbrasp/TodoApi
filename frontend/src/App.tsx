import { RouterProvider, createBrowserRouter } from "react-router-dom";

import "./App.css";
import RootLayout from "./layouts/RouteLayout.tsx";
import NotFoundPage from "./pages/NotFoundPage.tsx";
import TodoPage from "./pages/TodoPage.tsx";

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <RootLayout />,
      children: [{ path: "/", element: <TodoPage /> }],
    },
    { path: "*", element: <NotFoundPage /> },
  ]);
  return (
    <>
      <RouterProvider router={router} />
    </>
  );
}

export default App;
