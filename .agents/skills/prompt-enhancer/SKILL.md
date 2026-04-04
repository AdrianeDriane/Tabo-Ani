---
name: prompt-enhancer
description: Use when the user wants to improve a prompt so it becomes clearer, more precise, and more actionable for an AI model, while preserving intent and natural flow.
user-invocable: true
---

# Purpose

This skill enhances a user-provided prompt by making it clearer, more precise, and more actionable for an AI model.

It improves how the task is communicated, not just how it is worded.

# Behavior

When invoked:

1. Read the user's prompt carefully.
2. Rewrite it to:
   - clarify the actual task
   - make instructions more direct and actionable
   - remove vague or uncertain phrasing (e.g., "I think", "maybe")
   - make the expected output more explicit
   - improve flow and readability
3. Preserve the original intent and scope exactly.
4. Lightly organize the prompt for clarity, but keep it natural (no rigid templates).

# Enhancement Principles

- Convert vague intentions into clear instructions.
- Make the task explicit (what needs to be checked, compared, built, or modified).
- Clarify what the AI should produce as output.
- Keep the tone natural and developer-like, not robotic.
- Prefer clarity and precision over politeness or filler wording.

# Constraints

- Do not ask clarification questions.
- Do not introduce new requirements or assumptions.
- Do not remove important details from the original prompt.
- Do not force rigid structures like "Goal / Context / Constraints".
- Do not turn the prompt into a checklist unless it naturally improves clarity.
- Do not perform any coding or planning.

# Output Rules

- Output only the enhanced prompt.
- No explanations, no commentary.
- Keep it concise but more precise than the original.
- Ensure the result is immediately usable as an AI instruction.

# Quality Standard

A good result should:

- feel like a more "professional" version of the original prompt
- be more actionable and less ambiguous
- clearly communicate what needs to be done and what the expected output is
- avoid sounding like a paraphrase — it should feel like an upgrade
