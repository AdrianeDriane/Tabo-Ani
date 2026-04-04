import { Navigate, Outlet, createBrowserRouter } from "react-router-dom";
import AdminDashboard from "./pages/admin/Dashboard";
import { CheckoutPage } from "./pages/CheckoutPage";
import { ErrorPage } from "./pages/ErrorPage";
import { FarmerProfilePage } from "./pages/FarmerProfilePage";
import { Landing } from "./pages/Landing";
import { Login } from "./pages/auth/Login";
import { SignUp } from "./pages/auth/SignUp";
import Assistant from "./pages/assistant/Assistant";
import BuyersDashboard from "./pages/buyers/Dashboard";
import BuyersMarketplace from "./pages/buyers/Marketplace";
import BuyersWallet from "./pages/buyers/Wallet";
import DistributorDashboard from "./pages/distributor/Dashboard";
import QaReporting from "./pages/distributor/QaReporting";
import DistributorWallet from "./pages/distributor/Wallet";
import FarmerDashboard from "./pages/farmer/Dashboard";
import FarmerAnalytics from "./pages/farmer/Analytics";
import FarmerWallet from "./pages/farmer/Wallet";
import { MessagesPage } from "./pages/MessagesPage";
import { OrderDetailsPage } from "./pages/OrderDetailsPage";
import { ProductDetailsPage } from "./pages/ProductDetailsPage";

export const router = createBrowserRouter([
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
        path: "login",
        Component: Login,
      },
      {
        path: "admin",
        Component: AdminDashboard,
      },
      {
        path: "assistant",
        Component: Assistant,
      },
      {
        path: "checkout",
        Component: CheckoutPage,
      },
      {
        path: "buyers/dashboard",
        Component: BuyersDashboard,
      },
      {
        path: "buyers/marketplace",
        Component: BuyersMarketplace,
      },
      {
        path: "buyers/wallet",
        Component: BuyersWallet,
      },
      {
        path: "farmer/dashboard",
        Component: FarmerDashboard,
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
        path: "farmer/:id",
        Component: FarmerProfilePage,
      },
      {
        path: "messages",
        Component: MessagesPage,
      },
      {
        path: "orders",
        element: <Navigate replace to="/orders/TA-8821" />,
      },
      {
        path: "orders/:id",
        Component: OrderDetailsPage,
      },
      {
        path: "product/:id",
        Component: ProductDetailsPage,
      },
      {
        path: "*",
        element: <Navigate replace to="/" />,
      },
    ],
  },
]);
