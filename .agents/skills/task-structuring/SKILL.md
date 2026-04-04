---
name: task-structuring
description: Use ONLY when the user explicitly wants help turning a raw development task into a structured prompt using Goal, Context, Constraints, and Done when. This skill must never modify code or files.
---

# Purpose

Use this skill only to transform a user's raw task or rough prompt into a cleaner, structured task prompt.

This skill is for prompt enhancement only.

It must never:

- modify code
- create files
- edit files
- run implementation steps
- continue into coding after producing the structured prompt

# Behavior

When this skill is invoked:

1. Read the user's raw task carefully.
2. Determine whether the task is already sufficiently defined.

## If the user's task is already well defined

- Rewrite it into this exact structure:

Goal:

Context:

Constraints:

Done when:

- Preserve the user's original intent and logic.
- Improve clarity, precision, and completeness of wording.
- Return only the structured prompt unless the user asks for explanation.

## If the user's task is incomplete or missing logic

- First try to infer missing logic from the task itself.
- Do not invent important business logic, functional requirements, or technical assumptions that would materially change the user's intent.
- If the missing information is minor and can be safely inferred, produce the structured prompt with reasonable wording.
- If the missing information is important and affects correctness, ask only the minimum necessary clarification questions before producing the structured prompt.

## Clarification rule

Ask questions only when the missing information would significantly affect:

- implementation logic
- scope
- constraints
- expected output
- success criteria

Do not ask unnecessary questions.

# Output format

Always output the structured prompt using exactly this format:

Goal:

Context:

Constraints:

Done when:

# Output rules

- Do not include commentary before or after the structured prompt unless the user explicitly asks for explanation.
- Do not perform coding, planning, or file modification.
- Do not suggest implementation steps unless the user explicitly asks for them.
- The sole job of this skill is to return an improved structured task prompt.

# Quality standard

The structured prompt should:

- preserve the user's intent
- improve clarity and completeness
- reduce ambiguity
- be suitable for direct use in Codex CLI or another coding agent
