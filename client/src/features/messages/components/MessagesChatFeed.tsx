import type { ChatMessage } from "../types/messages.types";

type MessagesChatFeedProps = {
  messages: ChatMessage[];
};

export function MessagesChatFeed({ messages }: MessagesChatFeedProps) {
  return (
    <div className="custom-chat-height no-scrollbar flex flex-grow flex-col gap-8 overflow-y-auto p-8">
      {messages.map((message) => {
        if (message.type === "system") {
          return (
            <div className="flex justify-center" key={message.id}>
              <span className="text-[10px] font-bold tracking-widest text-gray-300 uppercase">
                {message.text}
              </span>
            </div>
          );
        }

        if (message.type === "buyer") {
          return (
            <div className="flex max-w-[80%] flex-col items-end gap-3 self-end" key={message.id}>
              <div className="rounded-3xl rounded-tr-none bg-[#14532D] p-6 text-white shadow-md">
                <p className="text-sm leading-relaxed">{message.text}</p>
              </div>
              <span className="text-[10px] font-medium text-gray-400">
                {message.senderName} - {message.timeLabel}
              </span>
            </div>
          );
        }

        return (
          <div className="flex max-w-[80%] flex-col gap-3" key={message.id}>
            <div className="rounded-3xl rounded-tl-none bg-[#F8F9FA] p-6">
              <p className="text-sm leading-relaxed">{message.text}</p>
            </div>

            {message.images && message.images.length > 0 && (
              <div className="grid w-full max-w-sm grid-cols-2 gap-2">
                {message.images.map((image) => (
                  <div
                    className="aspect-square overflow-hidden rounded-2xl border border-gray-100 bg-gray-100"
                    key={image.id}
                  >
                    <img alt={image.alt} className="h-full w-full object-cover" src={image.imageUrl} />
                  </div>
                ))}
              </div>
            )}

            <span className="text-[10px] font-medium text-gray-400">
              {message.senderName} - {message.timeLabel}
            </span>
          </div>
        );
      })}
    </div>
  );
}
