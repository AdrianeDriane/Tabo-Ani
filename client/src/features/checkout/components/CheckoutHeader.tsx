type CheckoutHeaderProps = {
  title: string;
  subtitle: string;
};

export default function CheckoutHeader({ title, subtitle }: CheckoutHeaderProps) {
  return (
    <div className="mb-10">
      <h1 className="text-4xl font-bold text-agri-green font-display">{title}</h1>
      <p className="mt-2 text-gray-500">{subtitle}</p>
    </div>
  );
}
