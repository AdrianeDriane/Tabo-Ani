import type { MessagesData } from "../types/messages.types";

const MESSAGES_DATA: MessagesData = {
  buyerName: "Ricardo Santos",
  buyerInitials: "RS",
  safetyNotice:
    "Safety Notice: Always verify proof-of-condition images before releasing payment. Keep all transactions within Tabo-Ani.",
  conversations: [
    {
      id: "c1",
      title: "Benguet Cabbage (200kg)",
      supplierName: "Juan Dela Cruz",
      status: "in-transit",
      latestMessage:
        "The proof of condition looks good. Proceeding with dispatch.",
    },
    {
      id: "c2",
      title: "Premium Red Onions",
      supplierName: "Maria Clara",
      status: "negotiating",
      latestMessage: "Can we adjust the price per kilo for bulk?",
    },
    {
      id: "c3",
      title: "Native Garlic Bulk",
      supplierName: "Baguio Farms Co.",
      status: "pending",
      latestMessage: "I will upload the harvest photos tomorrow morning.",
    },
  ],
  chats: {
    c1: {
      conversationId: "c1",
      orderTitle: "Benguet Cabbage",
      orderIdLabel: "#TA-88291",
      supplierName: "Juan Dela Cruz",
      ctaLabel: "View Order Details",
      trackerSteps: [
        { id: "t1", label: "Paid", state: "done" },
        { id: "t2", label: "Packed", state: "done" },
        { id: "t3", label: "In Transit", state: "current" },
      ],
      messages: [
        {
          id: "m1",
          type: "system",
          text: "Yesterday, 14:20",
        },
        {
          id: "m2",
          type: "supplier",
          senderName: "Juan Dela Cruz",
          timeLabel: "14:22",
          text: "Good afternoon Mr. Santos. We have just finished sorting the 200kg Cabbage. Here is the proof of condition before we load them into the truck.",
          images: [
            {
              id: "i1",
              alt: "Proof of condition 1",
              imageUrl:
                "https://lh3.googleusercontent.com/aida-public/AB6AXuDZHfbsABs1-jnUv5WmXJ597Lt7Pk24mDy1K-0-4uDQ6lUF8mq4PMY1hVae01EptZCm0axCyhF2izNOxgz4ENCNiZRobe8Rtespr12eZxWwVWvQuPlUdJsPoRluGuQbWYEeJ7a-7zbM5b6xJRF-Yd-FF4yGG8LRvMG25by-C6Tmd9HrC3hpUsUNQ8AgUxlA1HPLLwC81nm-Vmytwip6v-52ePTrmQptaS42XyXfzOhqc5tgUGYeOrXoUpuZEvjQoSBKQt3oT0lJkVVo",
            },
            {
              id: "i2",
              alt: "Proof of condition 2",
              imageUrl:
                "https://lh3.googleusercontent.com/aida-public/AB6AXuDb9uRrmlmnVWqcRfGW9xZoe7YyLxMXT9KoWgffrTZuJX_vkEyNct0b9kQyzVK8cozM5Q6WH1lwNdtNLeNzm61trHH9FpG79vuYPCsdkS0WpR2J_Afis3AMB_D2Pbe9aH2lJl3xjdS28zahY7me4ug_rbIH5BAoQMsOTJcC0ljbA1ImB0u199iYiWW0FKk2vhEtYA4W1SxucVh7gIenM06dDtAT53RDWTpEVFwm-9gJDB0EzJn_s2tP028gfd4xFOvFMSPS80aTx85e",
            },
          ],
        },
        {
          id: "m3",
          type: "buyer",
          senderName: "Ricardo Santos",
          timeLabel: "14:45",
          text: "The proof of condition looks good. They look very fresh. Please proceed with dispatch. Please ensure they are properly covered as it might rain this afternoon.",
        },
        {
          id: "m4",
          type: "supplier",
          senderName: "Juan Dela Cruz",
          timeLabel: "15:02",
          text: "Copy that. We are using heavy-duty tarps for this delivery. The truck is now departing from the warehouse. Estimated arrival is tomorrow morning.",
        },
      ],
    },
  },
};

export function getMessagesData(): MessagesData {
  return MESSAGES_DATA;
}
