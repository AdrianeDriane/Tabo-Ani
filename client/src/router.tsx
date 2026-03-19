import { Navigate, Outlet, createBrowserRouter } from "react-router-dom";
import { ErrorPage } from "./pages/ErrorPage";
import { Landing } from "./pages/Landing";
import { SignUp } from "./pages/auth/SignUp";
import Assistant from "./pages/assistant/Assistant";
import BuyersDashboard from "./pages/buyers/Dashboard";
import BuyersWallet from "./pages/buyers/Wallet";
import DistributorDashboard from "./pages/distributor/Dashboard";
import QaReporting from "./pages/distributor/QaReporting";
import DistributorWallet from "./pages/distributor/Wallet";
import FarmerAnalytics from "./pages/farmer/Analytics";
import FarmerWallet from "./pages/farmer/Wallet";

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
        path: "assistant",
        Component: Assistant,
      },
      {
        path: "buyers/dashboard",
        Component: BuyersDashboard,
      },
      {
        path: "buyers/wallet",
        Component: BuyersWallet,
      },
      {
        path: "farmer/analytics",
        Component: FarmerAnalytics,
      },
      {
        path: "farmer/wallet",
        Component: FarmerWallet,
      },
      {
        path: "distributor/dashboard",
        Component: DistributorDashboard,
      },
      {
        path: "distributor/qa-reporting",
        Component: QaReporting,
      },
      {
        path: "distributor/wallet",
        Component: DistributorWallet,
      },
      {
        path: "*",
        element: <Navigate replace to="/" />,
      },
    ],
  },
]);

export { router };
