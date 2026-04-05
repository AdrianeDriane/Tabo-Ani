import { Navigate, Outlet, createBrowserRouter } from "react-router-dom";
import AdminDashboard from "./pages/admin/Dashboard";
import { CheckoutPage } from "./pages/CheckoutPage";
import { ErrorPage } from "./pages/ErrorPage";
import { ProtectedRoute } from "./features/auth";
import { FarmerProfilePage } from "./pages/FarmerProfilePage";
import { Landing } from "./pages/Landing";
import { AccessDenied } from "./pages/auth/AccessDenied";
import { AuthAccessCheck } from "./pages/auth/AuthAccessCheck";
import { AccessPending } from "./pages/auth/AccessPending";
import { Login } from "./pages/auth/Login";
import { SignUp } from "./pages/auth/SignUp";
import { VerifyEmail } from "./pages/auth/VerifyEmail";
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
import { isAuthAccessCheckRouteEnabled } from "./features/auth/utils/authAccessCheck";

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
        path: "verify-email",
        Component: VerifyEmail,
      },
      {
        element: <ProtectedRoute />,
        children: [
          {
            path: "access-pending",
            Component: AccessPending,
          },
          {
            path: "access-denied",
            Component: AccessDenied,
          },
        ],
      },
      {
        path: "admin",
        element: <ProtectedRoute allowedRoles={["ADMIN"]} />,
        children: [
          {
            index: true,
            Component: AdminDashboard,
          },
        ],
      },
      {
        path: "assistant",
        Component: Assistant,
      },
      {
        path: "checkout",
        element: <ProtectedRoute allowedRoles={["BUYER"]} />,
        children: [
          {
            index: true,
            Component: CheckoutPage,
          },
        ],
      },
      {
        element: <ProtectedRoute allowedRoles={["BUYER"]} />,
        children: [
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
            path: "orders",
            element: <Navigate replace to="/orders/TA-8821" />,
          },
          {
            path: "orders/:id",
            Component: OrderDetailsPage,
          },
        ],
      },
      {
        element: <ProtectedRoute allowedRoles={["FARMER"]} />,
        children: [
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
        ],
      },
      {
        element: <ProtectedRoute allowedRoles={["DISTRIBUTOR"]} />,
        children: [
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
        ],
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
        path: "product/:id",
        Component: ProductDetailsPage,
      },
      ...(isAuthAccessCheckRouteEnabled
        ? [
            {
              // TODO(auth-check): Remove this temporary frontend auth verification route after auth QA/UAT sign-off.
              element: <ProtectedRoute />,
              children: [
                {
                  path: "auth-check",
                  element: <AuthAccessCheck />,
                },
              ],
            },
          ]
        : []),
      {
        path: "*",
        element: <Navigate replace to="/" />,
      },
    ],
  },
]);
