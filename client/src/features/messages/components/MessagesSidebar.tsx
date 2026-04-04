import type { ConversationPreview, TransactionStatus } from "../types/messages.types";

type MessagesSidebarProps = {
  conversations: ConversationPreview[];
  selectedConversationId: string;
  onSelectConversation: (conversationId: string) => void;
};

const STATUS_LABELS: Record<TransactionStatus, string> = {
  "in-transit": "IN TRANSIT",
  negotiating: "NEGOTIATING",
  pending: "PENDING",
};

const STATUS_BADGE_CLASSNAMES: Record<TransactionStatus, string> = {
  "in-transit": "bg-[#FF6B001A] text-[#FF6B00]",
  negotiating: "bg-gray-100 text-gray-400",
  pending: "bg-gray-100 text-gray-400",
};

export function MessagesSidebar({
  conversations,
  selectedConversationId,
  onSelectConversation,
}: MessagesSidebarProps) {
  return (
    <aside className="col-span-12 flex flex-col gap-4 overflow-hidden md:col-span-4 lg:col-span-3">
      <header className="flex flex-col gap-1">
        <h1 className="font-display text-2xl font-bold">Messages</h1>
        <p className="text-xs font-semibold tracking-widest text-gray-400 uppercase">
          Active Transactions
        </p>
      </header>

      <div className="no-scrollbar flex-grow overflow-y-auto rounded-[1.5rem] border border-gray-100 bg-white shadow-sm">
        {conversations.map((conversation) => {
          const isSelected = conversation.id === selectedConversationId;

          return (
            <button
              className={`w-full border-b border-gray-50 p-6 text-left transition-all ${
                isSelected ? "bg-[#F8F9FA80]" : "hover:bg-gray-50"
              }`}
              key={conversation.id}
              onClick={() => onSelectConversation(conversation.id)}
              type="button"
            >
              <div className="mb-1 flex items-start justify-between gap-2">
                <span className="font-display text-sm font-semibold">{conversation.title}</span>
                <span
                  className={`rounded-full px-2 py-0.5 text-[10px] font-bold ${STATUS_BADGE_CLASSNAMES[conversation.status]}`}
                >
                  {STATUS_LABELS[conversation.status]}
                </span>
              </div>

              <p className="mb-2 text-xs text-gray-500">Supplier: {conversation.supplierName}</p>

              <p
                className={`line-clamp-1 text-sm ${
                  isSelected ? "font-medium text-[#14532D]" : "text-gray-400"
                }`}
              >
                {conversation.latestMessage}
              </p>
            </button>
          );
        })}
      </div>
    </aside>
  );
}
