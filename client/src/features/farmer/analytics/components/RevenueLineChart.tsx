import { useMemo } from "react";
import type { RevenuePoint } from "../types/analytics.types";

type RevenueLineChartProps = {
  data: RevenuePoint[];
};

const CHART_WIDTH = 620;
const CHART_HEIGHT = 280;
const PADDING = { top: 24, right: 24, bottom: 36, left: 48 };

export default function RevenueLineChart({ data }: RevenueLineChartProps) {
  const { points, areaPath, xLabels, yTicks } = useMemo(() => {
    const values = data.map((item) => item.value);
    const maxValue = Math.max(...values, 1);
    const chartWidth = CHART_WIDTH - PADDING.left - PADDING.right;
    const chartHeight = CHART_HEIGHT - PADDING.top - PADDING.bottom;

    const stepX = chartWidth / Math.max(data.length - 1, 1);

    const pointList = data.map((item, index) => {
      const x = PADDING.left + index * stepX;
      const y =
        PADDING.top + chartHeight - (item.value / maxValue) * chartHeight;
      return { x, y, label: item.label, value: item.value };
    });

    const linePoints = pointList.map((point) => `${point.x},${point.y}`).join(" ");
    const areaPoints = [
      `${pointList[0]?.x ?? PADDING.left},${PADDING.top + chartHeight}`,
      linePoints,
      `${pointList[pointList.length - 1]?.x ?? PADDING.left},${PADDING.top + chartHeight}`,
    ].join(" ");

    const ticks = Array.from({ length: 4 }, (_, index) => {
      const value = Math.round((maxValue / 3) * index);
      const y =
        PADDING.top + chartHeight - (value / maxValue) * chartHeight;
      return { value, y };
    });

    return {
      points: pointList,
      areaPath: areaPoints,
      xLabels: pointList,
      yTicks: ticks,
    };
  }, [data]);

  return (
    <svg
      className="h-full w-full"
      viewBox={`0 0 ${CHART_WIDTH} ${CHART_HEIGHT}`}
      role="img"
      aria-label="Revenue trends line chart"
    >
      <defs>
        <linearGradient id="revenueFill" x1="0" y1="0" x2="0" y2="1">
          <stop offset="0%" stopColor="rgba(22, 163, 74, 0.18)" />
          <stop offset="100%" stopColor="rgba(22, 163, 74, 0)" />
        </linearGradient>
      </defs>

      {yTicks.map((tick) => (
        <g key={tick.value}>
          <line
            x1={PADDING.left}
            x2={CHART_WIDTH - PADDING.right}
            y1={tick.y}
            y2={tick.y}
            stroke="#F1F5F9"
          />
          <text
            x={PADDING.left - 12}
            y={tick.y + 4}
            textAnchor="end"
            className="fill-slate-400 text-[11px] font-medium"
          >
            ₱{Math.round(tick.value / 1000)}k
          </text>
        </g>
      ))}

      <polygon points={areaPath} fill="url(#revenueFill)" />

      <polyline
        points={points.map((point) => `${point.x},${point.y}`).join(" ")}
        fill="none"
        stroke="#16A34A"
        strokeWidth={3}
        strokeLinecap="round"
        strokeLinejoin="round"
      />

      {points.map((point) => (
        <circle
          key={point.label}
          cx={point.x}
          cy={point.y}
          r={3}
          fill="#16A34A"
        />
      ))}

      {xLabels.map((label) => (
        <text
          key={label.label}
          x={label.x}
          y={CHART_HEIGHT - 12}
          textAnchor="middle"
          className="fill-slate-400 text-[11px] font-medium"
        >
          {label.label}
        </text>
      ))}
    </svg>
  );
}
