type MessagesSafetyNoticeProps = {
  notice: string;
};

export function MessagesSafetyNotice({ notice }: MessagesSafetyNoticeProps) {
  return (
    <div className="flex items-center justify-between rounded-2xl border border-[#FF6B0033] bg-[#FF6B000D] p-4">
      <p className="text-xs font-medium tracking-wider text-[#FF6B00] uppercase">{notice}</p>
    </div>
  );
}
