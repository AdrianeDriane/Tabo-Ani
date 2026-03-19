export function MessagesInputBar() {
  return (
    <div className="border-t border-gray-50 p-6">
      <div className="relative flex items-center rounded-2xl bg-[#F8F9FA] px-6 py-4">
        <input
          className="flex-grow border-none bg-transparent text-sm text-[#14532D] placeholder-gray-400 focus:ring-0"
          placeholder="Type your message here..."
          type="text"
        />

        <div className="flex items-center gap-4">
          <button
            className="text-xs font-bold text-[#14532D99] uppercase transition-colors hover:text-[#14532D]"
            type="button"
          >
            Attach Image
          </button>
          <button
            className="rounded-full bg-[#14532D] px-8 py-2 text-xs font-bold text-white transition-all hover:bg-[#16A34A]"
            type="button"
          >
            SEND
          </button>
        </div>
      </div>
    </div>
  );
}
