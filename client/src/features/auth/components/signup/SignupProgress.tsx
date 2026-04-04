type SignupProgressProps = {
  currentStep: number;
  totalSteps: number;
};

export function SignupProgress({ currentStep, totalSteps }: SignupProgressProps) {
  return (
    <div className="mb-2 flex gap-2" aria-hidden="true">
      {Array.from({ length: totalSteps }, (_, index) => {
        const stepNumber = index + 1;
        const isCompleted = stepNumber <= currentStep;

        return (
          <div
            key={stepNumber}
            className={`h-2 flex-1 rounded-full ${
              isCompleted ? "bg-agri-leaf" : "bg-slate-100"
            }`}
          />
        );
      })}
    </div>
  );
}
