import AssistantChatPanel from "./AssistantChatPanel";
import AssistantIntro from "./AssistantIntro";
import { intro, messages, quickActions } from "../utils/assistant.constants";

export default function AssistantPage() {
  return (
    <main className="min-h-screen flex flex-col pt-24 pb-8 px-4 max-w-5xl mx-auto w-full">
      <AssistantIntro title={intro.title} subtitle={intro.subtitle} />
      <AssistantChatPanel
        messages={messages}
        quickActions={quickActions.map((action) => action.label)}
      />
    </main>
  );
}
