import { useMemo, useState } from "react";
import type { MessagesData } from "../types/messages.types";
import { MessagesChatFeed } from "./MessagesChatFeed";
import { MessagesChatHeader } from "./MessagesChatHeader";
import { MessagesInputBar } from "./MessagesInputBar";
import { MessagesNavigation } from "./MessagesNavigation";
import { MessagesSafetyNotice } from "./MessagesSafetyNotice";
import { MessagesSidebar } from "./MessagesSidebar";

type MessagesViewProps = {
  data: MessagesData;
};

export function MessagesView({ data }: MessagesViewProps) {
  const initialConversationId = data.conversations[0]?.id ?? "";
  const [selectedConversationId, setSelectedConversationId] = useState(initialConversationId);

  const activeChat = useMemo(
    () => data.chats[selectedConversationId] ?? data.chats[initialConversationId],
    [data.chats, initialConversationId, selectedConversationId],
  );

  return (
    <div className="bg-[#F8F9FA] font-sans text-[#14532D] antialiased">
      <MessagesNavigation buyerInitials={data.buyerInitials} buyerName={data.buyerName} />

      <main className="mx-auto flex h-screen max-w-7xl flex-col px-6 pb-12 pt-32">
        <div className="grid flex-grow grid-cols-12 gap-6 overflow-hidden">
          <MessagesSidebar
            conversations={data.conversations}
            onSelectConversation={setSelectedConversationId}
            selectedConversationId={selectedConversationId}
          />

          <section className="col-span-12 flex flex-col gap-4 overflow-hidden md:col-span-8 lg:col-span-9">
            <MessagesSafetyNotice notice={data.safetyNotice} />

            {activeChat ? (
              <div className="flex flex-grow flex-col overflow-hidden rounded-[2rem] border border-gray-100 bg-white shadow-sm">
                <MessagesChatHeader chat={activeChat} />
                <MessagesChatFeed messages={activeChat.messages} />
                <MessagesInputBar />
              </div>
            ) : (
              <div className="flex flex-grow items-center justify-center rounded-[2rem] border border-gray-100 bg-white shadow-sm">
                <p className="text-sm text-gray-500">No active conversation selected.</p>
              </div>
            )}
          </section>
        </div>
      </main>
    </div>
  );
}
