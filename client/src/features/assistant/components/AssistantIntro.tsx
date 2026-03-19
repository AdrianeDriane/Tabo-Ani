type AssistantIntroProps = {
  title: string;
  subtitle: string;
};

export default function AssistantIntro({ title, subtitle }: AssistantIntroProps) {
  return (
    <section className="mb-6 text-center" data-purpose="chat-intro">
      <h1 className="text-2xl md:text-3xl font-display font-extrabold text-agri-green mb-2">
        {title}
      </h1>
      <p className="text-slate-500 text-sm md:text-base">{subtitle}</p>
    </section>
  );
}
