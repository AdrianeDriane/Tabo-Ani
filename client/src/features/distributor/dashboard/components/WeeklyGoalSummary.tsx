import type { WeeklyGoal } from "../types/distributorDashboard.types";

type WeeklyGoalSummaryProps = {
  goal: WeeklyGoal;
};

export default function WeeklyGoalSummary({ goal }: WeeklyGoalSummaryProps) {
  return (
    <div
      className="bg-white p-8 rounded-[2rem] border border-slate-100 shadow-sm"
      data-purpose="performance-summary"
    >
      <div className="flex items-center justify-between mb-4">
        <h4 className="font-bold text-slate-800 uppercase text-xs tracking-widest">
          {goal.headline}
        </h4>
        <span className="text-xs font-bold text-agri-leaf">
          {goal.progress}% Reach
        </span>
      </div>
      <div className="w-full h-4 bg-slate-100 rounded-full overflow-hidden">
        <div
          className="h-full bg-gradient-to-r from-agri-leaf to-agri-green"
          style={{ width: `${goal.progress}%` }}
        />
      </div>
      <p className="mt-4 text-sm text-slate-500">{goal.detail}</p>
    </div>
  );
}
