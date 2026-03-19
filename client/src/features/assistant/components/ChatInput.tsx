type ChatInputProps = {
  placeholder: string;
};

function AttachmentIcon() {
  return (
    <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
      <path
        d="M15.172 7l-6.586 6.586a2 2 0 102.828 2.828l6.414-6.586a4 4 0 00-5.656-5.656l-6.415 6.585a6 6 0 108.486 8.486L20.5 13"
        strokeLinecap="round"
        strokeLinejoin="round"
        strokeWidth="2"
      />
    </svg>
  );
}

function SendIcon() {
  return (
    <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
      <path
        d="M14 5l7 7m0 0l-7 7m7-7H3"
        strokeLinecap="round"
        strokeLinejoin="round"
        strokeWidth="2"
      />
    </svg>
  );
}

export default function ChatInput({ placeholder }: ChatInputProps) {
  return (
    <div className="p-4 md:p-6 border-t border-slate-100 bg-white" data-purpose="chat-input-container">
      <div className="relative max-w-4xl mx-auto">
        <input
          className="w-full bg-slate-50 border-none rounded-2xl py-4 pl-6 pr-24 focus:ring-2 focus:ring-agri-green transition-all text-slate-700"
          placeholder={placeholder}
          type="text"
        />
        <div className="absolute right-2 top-2 flex gap-2">
          <button className="p-2 text-slate-400 hover:text-agri-green-600 transition-colors">
            <AttachmentIcon />
          </button>
          <button className="bg-agri-green hover:bg-agri-green/90 text-white p-2 rounded-xl transition-all shadow-lg shadow-agri-green/20">
            <SendIcon />
          </button>
        </div>
      </div>
    </div>
  );
}
