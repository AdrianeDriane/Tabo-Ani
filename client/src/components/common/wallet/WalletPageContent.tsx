import BalanceCard from "./BalanceCard";
import IncomeBreakdown from "./IncomeBreakdown";
import TransactionTable from "./TransactionTable";
import WalletHeader from "./WalletHeader";
import {
  balanceCard,
  breakdown,
  pendingNote,
  transactions,
  walletSummary,
} from "./wallet.constants";

export default function WalletPageContent() {
  return (
    <main className="pt-32 pb-20 px-4 sm:px-6 lg:px-8 max-w-7xl mx-auto">
      <WalletHeader title={walletSummary.title} subtitle={walletSummary.subtitle} />
      <div className="grid grid-cols-1 lg:grid-cols-12 gap-8">
        <div className="lg:col-span-4 space-y-8">
          <BalanceCard
            label={balanceCard.label}
            amount={balanceCard.amount}
            payoutNote={balanceCard.payoutNote}
            payoutAction={balanceCard.payoutAction}
          />
          <IncomeBreakdown
            title="This Month's Breakdown"
            items={breakdown}
            pendingTitle={pendingNote.title}
            pendingBody={pendingNote.body}
          />
        </div>
        <div className="lg:col-span-8">
          <TransactionTable title="Transaction History" transactions={transactions} />
        </div>
      </div>
    </main>
  );
}
