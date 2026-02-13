import './App.css'
import {createBrowserRouter, RouterProvider} from "react-router-dom";
import TodoPage from "./pages/TodoPage.tsx";
import RootLayout from "./layouts/RouteLayout.tsx";
import NotFoundPage from "./pages/NotFoundPage.tsx";


function App() {
    const router = createBrowserRouter([
        {
            path: "/",
            element: <RootLayout/>,
            children: [
                {path: "/", element: <TodoPage/>},
            ],
        },
        {path: "*", element: <NotFoundPage/>}
    ])
    return (
        <>
            <RouterProvider router={router}/>
        </>
    )
}

export default App
