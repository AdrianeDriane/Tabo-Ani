import type {
  Delivery,
  GradeOption,
  PhotoCard,
} from "../types/qaReporting.types";

export const deliveries: Delivery[] = [
  {
    id: "TABO-9942",
    title: "Benguet Highland Mix",
    status: "Arrived at Hub",
    isActive: true,
  },
  {
    id: "TABO-8821",
    title: "Carabao Mangoes (Class A)",
    status: "In Transit",
  },
  {
    id: "TABO-7756",
    title: "Organic Red Rice - 50kg",
    status: "Pending Pickup",
  },
];

export const photoCards: PhotoCard[] = [
  {
    title: "Batch Overview",
    description: "Wide shot of entire delivery",
    shape: "square",
  },
  {
    title: "Close-up Detail",
    description: "Focus on texture/freshness",
    shape: "circle",
  },
  {
    title: "Weight Display",
    description: "Scale reading or waybill",
    shape: "line",
  },
];

export const gradeOptions: GradeOption[] = [
  { value: "A", label: "A (Premium)" },
  { value: "B", label: "B (Standard)" },
  { value: "C", label: "C (Sub-par)" },
];
