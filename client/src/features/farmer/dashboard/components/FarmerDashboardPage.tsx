import { Link } from "react-router-dom";

type ActiveOrder = {
  initials: string;
  title: string;
  subtitle: string;
  status: string;
  statusClassName: string;
  amount: string;
};

type WeeklyBar = {
  day: string;
  height: string;
  className: string;
};

type UtilityCard = {
  title: string;
  description: string;
  cardClassName: string;
  iconShellClassName: string;
  iconClassName: string;
  to: string;
};

const NAV_LINKS = [
  { label: "Dashboard", href: "/farmer/dashboard", isActive: true },
  { label: "Marketplace", href: "#", isActive: false },
  { label: "Messages", href: "/messages", isActive: false },
  { label: "Analytics", href: "/farmer/analytics", isActive: false },
];

const ACTIVE_ORDERS: ActiveOrder[] = [
  {
    initials: "HC",
    title: "Highland Cabbage (100kg)",
    subtitle: "Order #TB-8821 - Benguet Logistics",
    status: "In Transit",
    statusClassName: "bg-blue-100 text-blue-700",
    amount: "P12,400",
  },
  {
    initials: "SR",
    title: "Sweet Red Onions (200kg)",
    subtitle: "Order #TB-8845 - Manila Hub",
    status: "Processing",
    statusClassName: "bg-orange-100 text-orange-700",
    amount: "P28,000",
  },
  {
    initials: "SR",
    title: "Sweet Red Onions (50kg)",
    subtitle: "Order #TB-8850 - Local Pickup",
    status: "Awaiting Pickup",
    statusClassName: "bg-green-100 text-green-700",
    amount: "P7,250",
  },
];

const WEEKLY_BARS: WeeklyBar[] = [
  { day: "MON", height: "40%", className: "bg-agri-leaf/40" },
  { day: "TUE", height: "65%", className: "bg-agri-leaf/60" },
  { day: "WED", height: "30%", className: "bg-agri-leaf/20" },
  { day: "THU", height: "90%", className: "bg-agri-leaf" },
  { day: "FRI", height: "75%", className: "bg-agri-leaf/80" },
  { day: "SAT", height: "45%", className: "bg-agri-leaf/30" },
  { day: "SUN", height: "10%", className: "bg-white" },
];

const UTILITY_CARDS: UtilityCard[] = [
  {
    title: "Capital Management",
    description: "Manage loans and insurance for your seasonal crops.",
    cardClassName: "bg-white border border-slate-100 text-slate-700",
    iconShellClassName:
      "bg-agri-leaf/10 group-hover:bg-agri-leaf transition-colors",
    iconClassName: "bg-agri-leaf group-hover:bg-white",
    to: "#",
  },
  {
    title: "Market Trends",
    description: "Real-time price monitoring across regional hubs.",
    cardClassName: "bg-white border border-slate-100 text-slate-700",
    iconShellClassName:
      "bg-agri-leaf/10 group-hover:bg-agri-leaf transition-colors",
    iconClassName: "bg-agri-leaf group-hover:bg-white",
    to: "#",
  },
  {
    title: "Create New Listing",
    description: "Post your harvest to the marketplace today.",
    cardClassName: "bg-agri-accent text-white",
    iconShellClassName: "bg-white/20",
    iconClassName: "bg-white",
    to: "#",
  },
  {
    title: "Open AI Assistant",
    description: "Get harvest advice and weather predictions.",
    cardClassName: "bg-agri-green text-white",
    iconShellClassName: "bg-white/10",
    iconClassName: "border-2 border-white bg-transparent rounded-full",
    to: "/assistant",
  },
];

function UtilityCardIcon({ card }: { card: UtilityCard }) {
  if (card.title === "Create New Listing") {
    return (
      <>
        <div className="h-1 w-6 rounded-full bg-white" />
        <div className="absolute h-6 w-1 rounded-full bg-white" />
      </>
    );
  }

  return <div className={`size-5 rounded-sm ${card.iconClassName}`} />;
}

function FarmerDashboardNav() {
  return (
    <nav className="fixed top-4 left-1/2 z-50 w-[calc(100%-1rem)] max-w-4xl -translate-x-1/2 sm:top-6 sm:w-[90%] lg:max-w-5xl">
      <div className="flex items-center justify-between rounded-full border border-white/30 bg-white/80 px-4 py-3 shadow-lg shadow-black/5 backdrop-blur-xl sm:px-6 lg:px-8">
        <div className="flex items-center gap-2">
          <div className="size-8 rounded-full bg-agri-green" />
          <span className="font-display text-sm font-bold tracking-tight text-agri-green sm:text-base">
            TABO-ANI
          </span>
        </div>

        <ul className="hidden items-center gap-6 text-sm font-semibold text-slate-600 md:flex lg:gap-8">
          {NAV_LINKS.map((item) => (
            <li key={item.label}>
              {item.href.startsWith("/") ? (
                <Link
                  to={item.href}
                  className={
                    item.isActive ? "text-agri-green" : "hover:text-agri-green"
                  }
                >
                  {item.label}
                </Link>
              ) : (
                <a
                  href={item.href}
                  className={
                    item.isActive ? "text-agri-green" : "hover:text-agri-green"
                  }
                >
                  {item.label}
                </a>
              )}
            </li>
          ))}
        </ul>

        <div className="size-10 overflow-hidden rounded-full border-2 border-white bg-slate-200 shadow-sm">
          <img
            src="https://lh3.googleusercontent.com/aida-public/AB6AXuCjtEVcm89XRWQeVeOoTBUYnBc4I9gYymE1WQQLJ7_Z3uLrtDtR2NfCfZKIZxOZD894yVqcFG9l7a-0c2H0HBUeo3b_jYUB7ik_LhRLhqSvKz08Y4QHzRGRstDSKDiZ23Z9uoHmxLLsFjrmE_ZEZmJJn4qg82BQyawKDZxdWEl-rdRai6-NZafknpAJqDQ84q7v0j8AMxzLwJUhapQidTIpQ_Jhw7hgr7SVjKMWIpNApTYyANSsZ8SFbFZgyJO1aK0k1PdlIZyJHlkZ"
            alt="User profile"
            className="size-full object-cover"
          />
        </div>
      </div>
    </nav>
  );
}

export default function FarmerDashboardPage() {
  return (
    <div className="min-h-screen bg-agri-light pb-10 text-slate-900 antialiased sm:pb-12">
      <FarmerDashboardNav />

      <main className="mx-auto w-full max-w-420 space-y-8 px-4 pt-24 sm:px-6 sm:pt-28 lg:px-8 lg:pt-32">
        <header className="flex flex-col gap-4 md:flex-row md:items-end md:justify-between">
          <div>
            <p className="text-sm font-bold tracking-widest text-agri-leaf uppercase">
              Overview
            </p>
            <h1 className="font-display text-3xl font-extrabold text-agri-green sm:text-4xl">
              Farmer Dashboard
            </h1>
          </div>
          <div className="md:text-right">
            <p className="text-sm font-medium text-slate-400">
              Harvest Season: Q3 2024
            </p>
          </div>
        </header>

        <section className="grid grid-cols-1 gap-5 md:grid-cols-3 md:gap-6">
          <article className="flex min-h-44 flex-col justify-between rounded-4xl border border-slate-100 bg-white p-6 shadow-sm sm:p-8">
            <p className="text-sm font-semibold text-slate-500">
              Total Revenue
            </p>
            <div>
              <p className="font-display text-4xl font-extrabold tracking-tighter text-agri-green sm:text-5xl">
                P142.5k
              </p>
              <p className="mt-2 text-sm font-bold text-agri-leaf">
                +12.4% from last month
              </p>
            </div>
          </article>

          <article className="flex min-h-44 flex-col justify-between rounded-4xl border border-slate-100 bg-white p-6 shadow-sm sm:p-8">
            <p className="text-sm font-semibold text-slate-500">
              Active Shipments
            </p>
            <div>
              <p className="font-display text-4xl font-extrabold tracking-tighter text-agri-green sm:text-5xl">
                24
              </p>
              <p className="mt-2 text-sm font-bold text-agri-accent">
                8 arriving today
              </p>
            </div>
          </article>

          <article className="flex min-h-44 flex-col justify-between rounded-4xl border border-slate-100 bg-white p-6 shadow-sm sm:p-8">
            <p className="text-sm font-semibold text-slate-500">
              Fulfillment Rate
            </p>
            <div>
              <p className="font-display text-4xl font-extrabold tracking-tighter text-agri-green sm:text-5xl">
                98.2%
              </p>
              <div className="mt-4 h-2 w-full overflow-hidden rounded-full bg-slate-100">
                <div className="h-full w-[98.2%] bg-agri-leaf" />
              </div>
            </div>
          </article>
        </section>

        <div className="grid grid-cols-1 gap-8 xl:grid-cols-3">
          <section className="space-y-6 xl:col-span-2">
            <h3 className="px-2 font-display text-xl font-bold text-agri-green">
              Active Orders
            </h3>

            <div className="space-y-4">
              {ACTIVE_ORDERS.map((order) => (
                <article
                  key={order.subtitle}
                  className="group rounded-4xl border border-slate-50 bg-white p-5 shadow-sm transition-all hover:border-agri-leaf sm:p-6"
                >
                  <div className="flex flex-col gap-5 sm:flex-row sm:items-center sm:justify-between">
                    <div className="flex items-center gap-4 sm:gap-6">
                      <div className="flex size-14 items-center justify-center rounded-2xl bg-slate-100 text-lg font-bold text-agri-green sm:size-16">
                        {order.initials}
                      </div>
                      <div>
                        <h4 className="text-base font-bold text-slate-800 sm:text-lg">
                          {order.title}
                        </h4>
                        <p className="text-sm text-slate-500">
                          {order.subtitle}
                        </p>
                      </div>
                    </div>

                    <div className="flex flex-row items-center justify-between gap-3 sm:flex-col sm:items-end">
                      <span
                        className={`rounded-full px-4 py-1.5 text-[11px] font-bold tracking-wider uppercase ${order.statusClassName}`}
                      >
                        {order.status}
                      </span>
                      <p className="font-bold text-agri-green">
                        {order.amount}
                      </p>
                    </div>
                  </div>
                </article>
              ))}
            </div>
          </section>

          <section className="rounded-4xl bg-agri-green p-6 text-white shadow-xl sm:p-8">
            <h3 className="mb-8 font-display text-xl font-bold">
              Weekly Earnings
            </h3>
            <div className="flex h-44 items-end justify-between gap-2 sm:h-48">
              {WEEKLY_BARS.map((bar) => (
                <div
                  key={bar.day}
                  className="flex flex-1 flex-col items-center gap-3"
                >
                  <div
                    style={{ height: bar.height }}
                    className={`w-full rounded-t-lg transition-all duration-700 ease-out ${bar.className}`}
                  />
                  <span className="text-[10px] font-bold opacity-60">
                    {bar.day}
                  </span>
                </div>
              ))}
            </div>
            <div className="mt-8 border-t border-white/10 pt-8">
              <p className="text-sm opacity-80">Top Source</p>
              <p className="font-display text-2xl font-bold">
                Direct Wholesale
              </p>
            </div>
          </section>
        </div>

        <section className="pt-4">
          <h3 className="mb-6 px-2 font-display text-xl font-bold text-agri-green">
            Farmer Toolkit
          </h3>

          <div className="grid grid-cols-1 gap-5 sm:grid-cols-2 xl:grid-cols-4 xl:gap-6">
            {UTILITY_CARDS.map((card) => {
              if (card.to.startsWith("/")) {
                return (
                  <Link
                    key={card.title}
                    to={card.to}
                    className={`group block rounded-4xl p-7 transition-shadow hover:shadow-md ${card.cardClassName}`}
                  >
                    <div
                      className={`mb-6 flex size-12 items-center justify-center rounded-2xl ${card.iconShellClassName}`}
                    >
                      <UtilityCardIcon card={card} />
                    </div>
                    <h4 className="mb-2 font-display font-bold">
                      {card.title}
                    </h4>
                    <p
                      className={
                        card.cardClassName.includes("text-white")
                          ? "text-sm leading-relaxed text-white/80"
                          : "text-sm leading-relaxed text-slate-500"
                      }
                    >
                      {card.description}
                    </p>
                  </Link>
                );
              }

              return (
                <a
                  key={card.title}
                  href={card.to}
                  className={`group block rounded-4xl p-7 transition-shadow hover:shadow-md ${card.cardClassName}`}
                >
                  <div
                    className={`mb-6 flex size-12 items-center justify-center rounded-2xl ${card.iconShellClassName}`}
                  >
                    <UtilityCardIcon card={card} />
                  </div>
                  <h4 className="mb-2 font-display font-bold">{card.title}</h4>
                  <p
                    className={
                      card.cardClassName.includes("text-white")
                        ? "text-sm leading-relaxed text-white/80"
                        : "text-sm leading-relaxed text-slate-500"
                    }
                  >
                    {card.description}
                  </p>
                </a>
              );
            })}
          </div>
        </section>
      </main>
    </div>
  );
}
