import type { SignupBrandPanelContent } from "../../types/signup.types";

type SignupBrandPanelProps = {
  content: SignupBrandPanelContent;
};

export function SignupBrandPanel({ content }: SignupBrandPanelProps) {
  return (
    <aside className="relative hidden overflow-hidden bg-agri-green p-12 text-white md:flex md:w-5/12 md:flex-col md:justify-between lg:w-1/2">
      <div className={`absolute inset-0 ${content.imageOpacityClassName ?? ""}`}>
        <img
          src={content.imageUrl}
          alt={content.imageAlt}
          className="size-full object-cover"
        />
        {(content.overlays ?? []).map((overlayClassName) => (
          <div key={overlayClassName} className={`absolute inset-0 ${overlayClassName}`} />
        ))}
      </div>

      <div className="relative z-10">
        <div className="mb-12 flex items-center gap-2">
          <div className="rounded-lg bg-white p-2">
            <span className="material-symbols-outlined font-bold text-agri-green">
              {content.logoIcon}
            </span>
          </div>
          <span className="text-2xl font-extrabold tracking-tight">Tabo-Ani</span>
        </div>

        <h1 className="mb-6 font-display text-4xl leading-tight font-extrabold lg:text-5xl">
          {content.heroTitle}
        </h1>
        <p className="max-w-md text-lg leading-relaxed text-white/90">
          {content.heroDescription}
        </p>
      </div>

      {content.highlights || content.testimonial || content.footerNote ? (
        <div className="relative z-10 mt-auto space-y-8">
          {content.highlights ? (
            <div className="grid grid-cols-2 gap-6">
              {content.highlights.map((item) => (
                <div key={item.label} className="flex items-center gap-3">
                  <span className="material-symbols-outlined text-agri-leaf">
                    {item.icon}
                  </span>
                  <span className="text-sm font-medium">{item.label}</span>
                </div>
              ))}
            </div>
          ) : null}

          {content.testimonial ? (
            <div className="rounded-3xl border border-white/20 bg-white/10 p-6 backdrop-blur-md">
              <div className="mb-3 flex gap-1 text-agri-accent">
                {Array.from({ length: content.testimonial.stars ?? 5 }, (_, index) => (
                  <span key={index} className="material-symbols-outlined text-sm">
                    star
                  </span>
                ))}
              </div>
              <p className="mb-4 text-sm italic text-white/90">
                "{content.testimonial.quote}"
              </p>
              <div className="flex items-center gap-3">
                <div
                  className="size-10 rounded-full bg-slate-200 bg-cover bg-center"
                  style={{ backgroundImage: `url(${content.testimonial.avatarUrl})` }}
                />
                <div>
                  <p className="text-xs font-bold">{content.testimonial.name}</p>
                  <p className="text-[10px] text-white/70">{content.testimonial.role}</p>
                </div>
              </div>
            </div>
          ) : null}

          {content.footerNote ? (
            <div className="text-sm font-medium text-white/70">{content.footerNote}</div>
          ) : null}
        </div>
      ) : null}
    </aside>
  );
}
