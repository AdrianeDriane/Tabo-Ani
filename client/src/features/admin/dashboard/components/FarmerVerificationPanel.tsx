import type { VerificationApplication } from "../types/dashboard.types";

type FarmerVerificationPanelProps = {
  applications: VerificationApplication[];
};

export default function FarmerVerificationPanel({ applications }: FarmerVerificationPanelProps) {
  return (
    <div className="rounded-[2rem] border border-agri-green/5 bg-white p-8 shadow-sm">
      <div className="mb-8 flex items-center justify-between">
        <h2 className="font-display text-xl font-bold tracking-tight text-agri-green uppercase">
          Verify Farmers
        </h2>
        <a className="text-xs font-bold text-agri-leaf hover:underline" href="#">
          View All Applications
        </a>
      </div>

      <div className="overflow-x-auto">
        <table className="w-full text-left">
          <thead>
            <tr className="border-b border-agri-green/5 text-[10px] font-bold tracking-widest text-agri-green/40 uppercase">
              <th className="pb-4">Farmer Name</th>
              <th className="pb-4">Region</th>
              <th className="pb-4">Crop Type</th>
              <th className="pb-4 text-right">Action</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-agri-green/5">
            {applications.map((application) => (
              <tr className="group" key={application.id}>
                <td className="py-4">
                  <div className="flex items-center gap-3">
                    <div className="flex h-8 w-8 items-center justify-center rounded-full bg-agri-light text-[10px] font-bold">
                      {application.initials}
                    </div>
                    <div>
                      <p className="text-sm font-bold text-agri-green">{application.farmerName}</p>
                      <p className="text-[10px] text-agri-leaf">ID: #{application.id}</p>
                    </div>
                  </div>
                </td>
                <td className="py-4 text-sm text-agri-green/70">{application.region}</td>
                <td className="py-4">
                  <span className="rounded-md bg-agri-light px-2 py-1 text-[10px] font-bold uppercase">
                    {application.cropType}
                  </span>
                </td>
                <td className="py-4 text-right">
                  <button
                    className="rounded-xl bg-agri-leaf px-4 py-2 text-[10px] font-bold text-white transition-colors hover:bg-agri-green"
                    type="button"
                  >
                    VERIFY
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
