type InspectorNotesProps = {
  notes: string;
  onChange: (value: string) => void;
};

export default function InspectorNotes({
  notes,
  onChange,
}: InspectorNotesProps) {
  return (
    <div className="bg-white p-6 rounded-xl border border-agri-border shadow-sm">
      <h2 className="font-display text-lg font-bold text-gray-900 mb-4">
        4. Inspector Observations
      </h2>
      <textarea
        className="w-full border-gray-300 rounded-lg focus:ring-agri-green focus:border-agri-green text-sm"
        placeholder="Detail any temperature issues, packaging damage, or notable variations in size..."
        rows={4}
        value={notes}
        onChange={(event) => onChange(event.target.value)}
      />
    </div>
  );
}
