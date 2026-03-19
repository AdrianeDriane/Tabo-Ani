import { useMemo, useState } from "react";
import NavBar from "../../../../components/common/distributor/NavBar";
import {
  deliveries,
  gradeOptions,
  photoCards,
} from "../utils/qaReporting.constants";
import type { Checkpoint, Grade } from "../types/qaReporting.types";
import AssignedDeliveriesSidebar from "./AssignedDeliveriesSidebar";
import FormActions from "./FormActions";
import InspectionCheckpoint from "./InspectionCheckpoint";
import InspectorNotes from "./InspectorNotes";
import PhotoEvidence from "./PhotoEvidence";
import QaFooter from "./QaFooter";
import QaModal from "./QaModal";
import QualityAssessment from "./QualityAssessment";
import ShipmentContext from "./ShipmentContext";

export default function QaReportingPage() {
  const [checkpoint, setCheckpoint] = useState<Checkpoint>("pickup");
  const [freshness, setFreshness] = useState(8);
  const [quantityMatch, setQuantityMatch] = useState(true);
  const [grade, setGrade] = useState<Grade>("A");
  const [notes, setNotes] = useState("");
  const [submitted, setSubmitted] = useState(false);
  const [modal, setModal] = useState<{
    title: string;
    description: string;
    confirmLabel?: string;
    onConfirm?: () => void;
  } | null>(null);

  const selectedDelivery = useMemo(
    () => deliveries.find((item) => item.isActive) ?? deliveries[0],
    [],
  );

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
  };

  const openModal = (
    title: string,
    description: string,
    options?: { confirmLabel?: string; onConfirm?: () => void },
  ) => {
    setModal({ title, description, ...options });
  };

  return (
    <div className="min-h-screen bg-agri-light text-slate-800 font-sans antialiased">
      <header className="fixed top-0 left-0 right-0 z-50 px-4">
        <NavBar />
      </header>

      <main className="pt-24 pb-12 px-6 max-w-7xl mx-auto flex gap-8">
        <AssignedDeliveriesSidebar deliveries={deliveries} />

        <section className="flex-grow" data-purpose="qa-main-view">
          <ShipmentContext selectedDeliveryId={selectedDelivery.id} />

          <form className="space-y-6" onSubmit={handleSubmit}>
            <InspectionCheckpoint
              checkpoint={checkpoint}
              onChange={setCheckpoint}
            />
            <QualityAssessment
              freshness={freshness}
              onFreshnessChange={setFreshness}
              quantityMatch={quantityMatch}
              onQuantityMatchChange={setQuantityMatch}
              grade={grade}
              onGradeChange={setGrade}
              gradeOptions={gradeOptions}
            />
            <PhotoEvidence photoCards={photoCards} />
            <InspectorNotes notes={notes} onChange={setNotes} />
            {submitted && (
              <div className="rounded-xl bg-emerald-50 p-5 text-sm text-emerald-800">
                QA report submitted for #{selectedDelivery.id}. Freshness score
                was {freshness}/10 with grade {grade}. Stakeholders have been
                notified.
              </div>
            )}
            <FormActions
              onSubmitClick={() =>
                openModal(
                  "Submit QA Report",
                  "Please confirm you want to submit this QA report.",
                  {
                    confirmLabel: "Confirm Submit",
                    onConfirm: () => {
                      setSubmitted(true);
                      setModal(null);
                    },
                  },
                )
              }
              onEscalate={() =>
                openModal(
                  "Escalation Sent",
                  "A discrepancy alert has been sent to the hub manager.",
                )
              }
            />
          </form>
        </section>
      </main>

      <QaFooter />
      <QaModal
        isOpen={Boolean(modal)}
        title={modal?.title ?? ""}
        description={modal?.description ?? ""}
        onConfirm={modal?.onConfirm}
        confirmLabel={modal?.confirmLabel}
        onClose={() => setModal(null)}
      />
    </div>
  );
}
