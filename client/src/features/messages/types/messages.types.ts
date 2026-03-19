export type TransactionStatus = "in-transit" | "negotiating" | "pending";

export type ConversationPreview = {
  id: string;
  title: string;
  supplierName: string;
  status: TransactionStatus;
  latestMessage: string;
};

export type OrderTrackerStep = {
  id: string;
  label: string;
  state: "done" | "current" | "pending";
};

export type ChatImage = {
  id: string;
  alt: string;
  imageUrl: string;
};

export type ChatMessage = {
  id: string;
  type: "system" | "supplier" | "buyer";
  senderName?: string;
  timeLabel?: string;
  text: string;
  images?: ChatImage[];
};

export type ConversationChat = {
  conversationId: string;
  orderTitle: string;
  orderIdLabel: string;
  supplierName: string;
  ctaLabel: string;
  trackerSteps: OrderTrackerStep[];
  messages: ChatMessage[];
};

export type MessagesData = {
  buyerName: string;
  buyerInitials: string;
  safetyNotice: string;
  conversations: ConversationPreview[];
  chats: Record<string, ConversationChat>;
};
