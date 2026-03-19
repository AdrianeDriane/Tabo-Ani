import type { ChatMessage } from "../types/assistant.types";

type ChatMessageBubbleProps = {
  message: ChatMessage;
  showQuickActions?: boolean;
  quickActions?: string[];
};

function AssistantIcon() {
  return (
    <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
      <path
        d="M9.75 17L9 20l-1 1h8l-1-1-.75-3M3 13h18M5 17h14a2 2 0 002-2V5a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z"
        strokeLinecap="round"
        strokeLinejoin="round"
        strokeWidth="2"
      />
    </svg>
  );
}

function AssistantActionIcon() {
  return (
    <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
      <path
        d="M13 10V3L4 14h7v7l9-11h-7z"
        strokeLinecap="round"
        strokeLinejoin="round"
        strokeWidth="2"
      />
    </svg>
  );
}

export default function ChatMessageBubble({
  message,
  showQuickActions,
  quickActions,
}: ChatMessageBubbleProps) {
  if (message.role === "user") {
    return (
      <div className="flex gap-4 flex-row-reverse max-w-[85%] ml-auto">
        <div className="flex-shrink-0 w-10 h-10 rounded-full bg-slate-200 flex items-center justify-center text-slate-600 font-bold">
          RS
        </div>
        <div className="bg-white border border-slate-100 text-slate-800 rounded-2xl rounded-tr-none p-4 shadow-sm">
          <p className="text-sm md:text-base">{message.content}</p>
        </div>
      </div>
    );
  }

  if (showQuickActions) {
    return (
      <div className="flex gap-4 max-w-[85%]">
        <div className="flex-shrink-0 w-10 h-10 rounded-full bg-agri-green flex items-center justify-center text-white shadow-md">
          <AssistantActionIcon />
        </div>
        <div className="space-y-4 w-full">
          <div className="bg-agri-light text-agri-green rounded-2xl rounded-tl-none p-4 shadow-sm">
            <p className="text-sm md:text-base mb-4">{message.content}</p>
            <div className="flex flex-wrap gap-2" data-purpose="quick-actions">
              {quickActions?.map((action) => (
                <button
                  key={action}
                  className="bg-white border border-agri-leaf/30 hover:border-agri-leaf hover:bg-agri-light text-agri-green px-4 py-2 rounded-full text-xs md:text-sm font-semibold transition-all shadow-sm"
                >
                  {action}
                </button>
              ))}
            </div>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="flex gap-4 max-w-[85%]">
      <div className="flex-shrink-0 w-10 h-10 rounded-full bg-agri-green flex items-center justify-center text-white shadow-md">
        <AssistantIcon />
      </div>
      <div className="bg-agri-light text-agri-green rounded-2xl rounded-tl-none p-4 shadow-sm">
        <p className="text-sm md:text-base leading-relaxed">{message.content}</p>
      </div>
    </div>
  );
}
