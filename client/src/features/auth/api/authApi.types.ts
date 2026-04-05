export type ApiResponse<T> = {
  success: boolean;
  message: string;
  data: T;
};

export type ErrorResponse = {
  success: boolean;
  message: string;
  errors: string[];
};
