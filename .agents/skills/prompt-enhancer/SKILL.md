---
name: prompt-enhancer
description: Use when the user wants to improve the clarity, grammar, and effectiveness of a prompt without changing its intent. This skill rewrites prompts to be clearer and more suitable for AI consumption.
user-invocable: true
---

# Purpose

This skill improves the quality of a user-provided prompt by rewriting it with:

- clearer wording
- better grammar
- improved structure
- reduced ambiguity

It is a general-purpose prompt enhancer and is not limited to development tasks.

# Behavior

When invoked:

1. Read the user's prompt carefully.
2. Rewrite the prompt to improve:
   - clarity
   - grammar
   - flow
   - precision of language
3. Preserve the original intent exactly.
4. Do not change meaning, scope, or introduce new requirements.

# Constraints

- Do not ask clarification questions.
- Do not add new requirements, logic, or assumptions.
- Do not remove important details from the original prompt.
- Do not restructure into rigid formats (e.g., Goal/Context/Constraints) unless already present.
- Do not perform any coding, planning, or execution.
- Do not explain what was changed unless explicitly asked.

# Output Rules

- Output only the improved prompt.
- Do not include commentary, explanation, or extra sections.
- Keep the result natural and easy to use directly.

# Quality Standard

A good result should:

- sound natural and professional
- be easier for an AI to understand and execute
- remove ambiguity without altering meaning
- retain all critical details from the original prompt
