import ChatInput from "./ChatInput";
import ChatMessageBubble from "./ChatMessageBubble";
import type { ChatMessage } from "../types/assistant.types";

type AssistantChatPanelProps = {
  messages: ChatMessage[];
  quickActions: string[];
};

export default function AssistantChatPanel({
  messages,
  quickActions,
}: AssistantChatPanelProps) {
  return (
    <section
      className="flex-grow bg-white rounded-3xl shadow-sm border border-slate-100 overflow-hidden flex flex-col relative"
      data-purpose="chat-interface"
    >
      <div className="h-[calc(100vh-220px)] overflow-y-auto p-4 md:p-8 space-y-6">
        {messages.map((message, index) => (
          <ChatMessageBubble
            key={message.id}
            message={message}
            showQuickActions={index === messages.length - 1}
            quickActions={index === messages.length - 1 ? quickActions : undefined}
          />
        ))}
      </div>
      <ChatInput placeholder="Type your message here..." />
    </section>
  );
}
