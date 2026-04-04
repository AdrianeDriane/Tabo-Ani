import { MessagesView, getMessagesData } from "../features/messages";

export function MessagesPage() {
  const data = getMessagesData();
  return <MessagesView data={data} />;
}
