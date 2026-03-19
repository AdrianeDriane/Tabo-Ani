type HandlingInstructionsSectionProps = {
  placeholder: string;
};

export default function HandlingInstructionsSection({
  placeholder,
}: HandlingInstructionsSectionProps) {
  return (
    <div className="rounded-3xl border border-gray-100 bg-white p-10 shadow-sm">
      <h2 className="mb-8 text-xl font-bold text-gray-900 font-display">Handling Instructions</h2>
      <div>
        <label className="mb-2 block text-xs font-bold tracking-wider text-gray-500 uppercase">
          Produce Handling Notes
        </label>
        <textarea
          className="w-full rounded-xl border-gray-200 p-4 text-gray-900 focus:border-agri-accent focus:ring-agri-accent"
          placeholder={placeholder}
          rows={3}
        />
      </div>
    </div>
  );
}
