type OrdersHeaderSectionProps = {
  title: string;
  subtitle: string;
};

export function OrdersHeaderSection({ title, subtitle }: OrdersHeaderSectionProps) {
  return (
    <section className="mb-10">
      <h1 className="font-display mb-2 text-4xl font-bold text-gray-900">{title}</h1>
      <p className="max-w-2xl text-gray-500">{subtitle}</p>
    </section>
  );
}
