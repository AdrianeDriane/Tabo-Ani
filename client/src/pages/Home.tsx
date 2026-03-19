import { type FormEvent, useState } from "react";
import { Link } from "react-router-dom";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";

type ProduceListing = {
  id: number;
  name: string;
  category: string;
  pricePerKg: number;
  createdAtUtc: string;
};

const API_BASE_URL = "https://localhost:7225";

async function getListings(): Promise<ProduceListing[]> {
  const res = await fetch(`${API_BASE_URL}/api/ProduceListings`);
  if (!res.ok) throw new Error("Failed to fetch listings");
  return res.json();
}

async function createListing(payload: {
  name: string;
  category: string;
  pricePerKg: number;
}): Promise<ProduceListing> {
  const res = await fetch(`${API_BASE_URL}/api/ProduceListings`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(payload),
  });

  if (!res.ok) {
    const errorText = await res.text();
    throw new Error(errorText || "Failed to create listing");
  }

  return res.json();
}

export function Home() {
  const queryClient = useQueryClient();

  const [name, setName] = useState("");
  const [category, setCategory] = useState("");
  const [pricePerKg, setPricePerKg] = useState("");

  const { data, isLoading, error } = useQuery({
    queryKey: ["produce-listings"],
    queryFn: getListings,
  });

  const mutation = useMutation({
    mutationFn: createListing,
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ["produce-listings"] });
      setName("");
      setCategory("");
      setPricePerKg("");
    },
  });

  const onSubmit = (e: FormEvent) => {
    e.preventDefault();

    mutation.mutate({
      name,
      category,
      pricePerKg: Number(pricePerKg),
    });
  };

  return (
    <main className="min-h-screen bg-green-50 px-6 py-10">
      <div className="mx-auto max-w-4xl space-y-8">
        <header className="rounded-2xl bg-white p-6 shadow-sm">
          <h1 className="text-3xl font-bold text-green-800">Tabo-Ani Digital</h1>
          <p className="mt-2 text-slate-600">
            React + ASP.NET Core + Supabase smoke test
          </p>
          <div className="mt-4 flex flex-wrap gap-3">
            <Link
              className="inline-block rounded-full bg-[#14532D] px-5 py-2 text-sm font-semibold text-white"
              to="/product/1"
            >
              Open Product Details Demo
            </Link>
            <Link
              className="inline-block rounded-full border border-[#14532D] px-5 py-2 text-sm font-semibold text-[#14532D]"
              to="/farmer/1"
            >
              Open Farmer Profile Demo
            </Link>
            <Link
              className="inline-block rounded-full border border-[#14532D] px-5 py-2 text-sm font-semibold text-[#14532D]"
              to="/messages"
            >
              Open Messages Demo
            </Link>
            <Link
              className="inline-block rounded-full border border-[#14532D] px-5 py-2 text-sm font-semibold text-[#14532D]"
              to="/orders/TA-8821"
            >
              Open Orders Demo
            </Link>
          </div>
        </header>

        <section className="rounded-2xl bg-white p-6 shadow-sm">
          <h2 className="mb-4 text-xl font-semibold text-slate-800">
            Add Produce Listing
          </h2>

          <form onSubmit={onSubmit} className="grid gap-4 md:grid-cols-3">
            <input
              className="rounded-xl border border-slate-300 px-4 py-3"
              placeholder="Product name"
              value={name}
              onChange={(e) => setName(e.target.value)}
            />

            <input
              className="rounded-xl border border-slate-300 px-4 py-3"
              placeholder="Category"
              value={category}
              onChange={(e) => setCategory(e.target.value)}
            />

            <input
              className="rounded-xl border border-slate-300 px-4 py-3"
              placeholder="Price per kg"
              type="number"
              step="0.01"
              value={pricePerKg}
              onChange={(e) => setPricePerKg(e.target.value)}
            />

            <div className="md:col-span-3">
              <button
                type="submit"
                disabled={mutation.isPending}
                className="rounded-xl bg-green-700 px-5 py-3 font-medium text-white disabled:opacity-60"
              >
                {mutation.isPending ? "Saving..." : "Create listing"}
              </button>
            </div>
          </form>

          {mutation.error && (
            <p className="mt-4 text-sm text-red-600">
              {(mutation.error as Error).message}
            </p>
          )}
        </section>

        <section className="rounded-2xl bg-white p-6 shadow-sm">
          <h2 className="mb-4 text-xl font-semibold text-slate-800">
            Listings from database
          </h2>

          {isLoading && <p>Loading...</p>}

          {error && <p className="text-red-600">{(error as Error).message}</p>}

          <div className="grid gap-4">
            {data?.map((item) => (
              <article key={item.id} className="rounded-xl border border-slate-200 p-4">
                <h3 className="text-lg font-semibold text-slate-800">{item.name}</h3>
                <p className="text-slate-600">Category: {item.category}</p>
                <p className="text-slate-600">
                  Price/kg: ₱{Number(item.pricePerKg).toFixed(2)}
                </p>
              </article>
            ))}

            {!isLoading && data?.length === 0 && (
              <p className="text-slate-500">No listings yet.</p>
            )}
          </div>
        </section>
      </div>
    </main>
  );
}
