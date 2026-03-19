import { Navigate, Outlet, createBrowserRouter } from "react-router-dom";
import { ErrorPage } from "./pages/ErrorPage";
import { Landing } from "./pages/Landing";
import { SignUp } from "./pages/auth/SignUp";

const router = createBrowserRouter([
  {
    path: "/",
    Component: Outlet,
    ErrorBoundary: ErrorPage,
    children: [
      {
        index: true,
        Component: Landing,
      },
      {
        path: "signup",
        Component: SignUp,
      },
      {
        path: "*",
        element: <Navigate replace to="/" />,
      },
    ],
  },
]);

export { router };
