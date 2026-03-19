import type { ChatMessage, QuickAction } from "../types/assistant.types";

export const intro = {
  title: "Tabo-Ani Assistant",
  subtitle: "Ask about products, deliveries, or orders.",
};

export const messages: ChatMessage[] = [
  {
    id: "assistant-welcome",
    role: "assistant",
    content:
      "Hello Ricardo! Welcome back to your dashboard. I can help you manage your farm-to-market operations today. What would you like to do?",
  },
  {
    id: "user-status",
    role: "user",
    content: "I want to check the status of my recent harvest delivery.",
  },
  {
    id: "assistant-followup",
    role: "assistant",
    content:
      "I've found your active orders. Would you like to view the details or track the real-time location?",
  },
];

export const quickActions: QuickAction[] = [
  { id: "track", label: "Track Active Order" },
  { id: "wallet", label: "Check Wallet" },
  { id: "prices", label: "Market Prices" },
];
