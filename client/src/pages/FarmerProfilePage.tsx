import { Link, useParams } from "react-router-dom";
import { FarmerProfileView, getFarmerProfileById } from "../features/farmer";

export function FarmerProfilePage() {
  const { id } = useParams();

  if (!id) {
    return (
      <main className="flex min-h-screen items-center justify-center bg-slate-50 px-4">
        <div className="w-full max-w-lg rounded-3xl border border-slate-200 bg-white p-8 text-center shadow-sm">
          <h1 className="font-display text-3xl font-bold text-slate-900">Invalid Farmer URL</h1>
          <p className="mt-3 text-slate-600">A farmer id is required to view profile details.</p>
          <Link
            className="mt-6 inline-flex rounded-full bg-[#14532D] px-6 py-3 text-sm font-semibold text-white"
            to="/"
          >
            Back to Home
          </Link>
        </div>
      </main>
    );
  }

  const farmer = getFarmerProfileById(id);

  if (!farmer) {
    return (
      <main className="flex min-h-screen items-center justify-center bg-slate-50 px-4">
        <div className="w-full max-w-lg rounded-3xl border border-slate-200 bg-white p-8 text-center shadow-sm">
          <h1 className="font-display text-3xl font-bold text-slate-900">Farmer Not Found</h1>
          <p className="mt-3 text-slate-600">We could not find a farmer with id "{id}".</p>
          <Link
            className="mt-6 inline-flex rounded-full bg-[#14532D] px-6 py-3 text-sm font-semibold text-white"
            to="/"
          >
            Back to Home
          </Link>
        </div>
      </main>
    );
  }

  return <FarmerProfileView farmer={farmer} />;
}
