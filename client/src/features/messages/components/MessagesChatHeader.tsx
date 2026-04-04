import type { ConversationChat, OrderTrackerStep } from "../types/messages.types";

type MessagesChatHeaderProps = {
  chat: ConversationChat;
};

function getStepClass(step: OrderTrackerStep): { dot: string; label: string } {
  if (step.state === "current") {
    return { dot: "bg-[#FF6B00] animate-pulse", label: "text-[#FF6B00]" };
  }

  if (step.state === "done") {
    return { dot: "bg-[#16A34A]", label: "text-[#16A34A]" };
  }

  return { dot: "bg-gray-200", label: "text-gray-300" };
}

export function MessagesChatHeader({ chat }: MessagesChatHeaderProps) {
  return (
    <div className="flex items-center justify-between border-b border-gray-50 p-6">
      <div className="flex items-center gap-6">
        <div>
          <h2 className="font-display text-lg font-bold">{chat.orderTitle}</h2>
          <p className="text-xs text-gray-400">
            Order ID: {chat.orderIdLabel} - Supplier: {chat.supplierName}
          </p>
        </div>

        <div className="ml-8 hidden items-center gap-4 lg:flex">
          {chat.trackerSteps.map((step, index) => {
            const styles = getStepClass(step);

            return (
              <div className="flex items-center gap-4" key={step.id}>
                <div className="flex items-center gap-2">
                  <div className={`h-2 w-2 rounded-full ${styles.dot}`} />
                  <span className={`text-[10px] font-bold uppercase ${styles.label}`}>
                    {step.label}
                  </span>
                </div>

                {index < chat.trackerSteps.length - 1 && <div className="h-px w-8 bg-gray-200" />}
              </div>
            );
          })}
        </div>
      </div>

      <button
        className="rounded-full bg-[#14532D] px-6 py-2.5 text-xs font-bold text-white transition-all hover:bg-opacity-90"
        type="button"
      >
        {chat.ctaLabel.toUpperCase()}
      </button>
    </div>
  );
}
