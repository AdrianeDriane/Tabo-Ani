export type Checkpoint = "pickup" | "delivery";

export type Grade = "A" | "B" | "C";

export type Delivery = {
  id: string;
  title: string;
  status: string;
  isActive?: boolean;
};

export type PhotoCard = {
  title: string;
  description: string;
  shape: "square" | "circle" | "line";
};

export type GradeOption = {
  value: Grade;
  label: string;
};
