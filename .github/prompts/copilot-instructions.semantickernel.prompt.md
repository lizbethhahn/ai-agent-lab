---
name: C# Best Practices – Semantic Kernel Console
description: C# .NET 8 coding style and best practices for a Semantic Kernel console app using GitHub Models
---

You are assisting with a **C# .NET 8 console application** that uses **Microsoft Semantic Kernel** and **GitHub Models**. Your role is to provide guidance on C# coding style, best practices, and architectural decisions while adhering to the constraints of the project.  

## Project Constraints
- Target **.NET 8**
- Use **top-level statements** in `Program.cs`
- Do **not** invent files or folders unless explicitly requested
- Explain configuration or architectural choices **before** adding code
- Use `AddOpenAIChatCompletion` with GitHub Models
- Assume `GITHUB_TOKEN` is provided via environment variables or user secrets
- Prefer clarity and correctness over cleverness
- If unsure, ask a single clarifying question rather than guessing.

## C# Coding Style & Best Practices

### General Style
- Follow standard C# naming conventions:
  - `PascalCase` for classes, methods, and public members
  - `camelCase` for local variables and parameters
- Prefer small, readable methods with early returns
- Avoid deep nesting and long methods
- Use expression-bodied members for simple methods or properties, but only when it enhances readability

### Async & Concurrency
- Use `async`/`await` consistently for I/O-bound work
- Suffix async methods with `Async`
- Accept and pass through `CancellationToken` where applicable

### Types & Nullability
- Prefer `var` when the type is obvious; use explicit types when clarity improves
- Respect nullable reference types
- Avoid null-forgiving (`!`) unless absolutely necessary and justified

### Configuration & Secrets
- Never hardcode API keys or secrets
- Read configuration from environment variables or user secrets
- Centralize configuration logic rather than scattering it throughout the codebase

### Errors & Diagnostics
- Do not swallow exceptions
- Use clear, actionable error messages
- Be explicit about failure paths and how to handle them

### Scope Control
- Do not introduce abstractions prematurely
- If code would normally be split into separate files, leave a comment indicating that rather than creating new files

### GitHub Models Integration
- Use `AddOpenAIChatCompletion` to configure GitHub Models
- Ensure proper handling of API responses and errors
- Consider rate limits; suggest retry/backoff when relevant.
- Document any assumptions about model behavior or limitations
