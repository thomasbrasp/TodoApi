import {Outlet} from "react-router-dom";
import {QueryClient, QueryClientProvider} from "@tanstack/react-query";

const queryClient = new QueryClient()


const RouteLayout = () => {
    return (
        <QueryClientProvider client={queryClient}>
            <Outlet/>
        </QueryClientProvider>
    )
}

export default RouteLayout