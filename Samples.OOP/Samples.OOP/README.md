# OOP Samples — Documentation

C# (.NET 6) practice solution for object‑oriented programming concepts and common interview questions.
Solution file: [`Samples.BlaxZoser.sln`](Samples.BlaxZoser.sln).

## Projects

| Project | Type | What it covers | Exercises | Level |
|---|---|---|---|---|
| [Samples.OOP](Samples.OOP/README.md) | Class library | The class model: inheritance, `virtual`/`override`/`new`, abstract classes, interfaces, static members, encapsulation | — (used by Samples.Test) | 🟢 → 🔴 |
| [Samples.Test](Samples.Test/README.md) | Console app | Runnable scenarios on encapsulation, inheritance, polymorphism, abstract classes and interfaces | 20 | 🟢 → 🔴 |
| [Samples.Generics](Samples.Generic/README.md) | Console app | Generic methods, generic classes, type constraints, iterators | 2 | 🟢 → 🟡 |
| [Samples.Delegates](Samples.Delegates/README.md) | Console app | Custom delegates, `Action`, `Func`, plus a template‑parser interview problem | 4 | 🟢 → 🔴 |

**Levels:** 🟢 Beginner · 🟡 Intermediate · 🔴 Advanced / interview trap

## Suggested learning path

1. **Encapsulation** — [Samples.Test E1–E3](Samples.Test/README.md#1-encapsulation--encapsulationplayerconsolecs)
2. **Inheritance & constructors** — [Samples.Test I1–I3](Samples.Test/README.md#2-inheritance--inheritancesamplescs)
3. **Polymorphism** — [Samples.Test P4, P5, P7](Samples.Test/README.md#3-polymorphism--polymorphismsamplescs)
4. **`override` vs `new`** — I4, P6, P9, then the traps I5 and P8
5. **Abstract classes** — [Samples.Test A1–A4](Samples.Test/README.md#4-abstract-classes--abstractsamplescs)
6. **Interfaces** — [Samples.Test F1–F2](Samples.Test/README.md#5-interfaces--interfacesamplescs)
7. **Generics** — [Samples.Generics G1–G2](Samples.Generic/README.md)
8. **Delegates** — [Samples.Delegates D1–D3](Samples.Delegates/README.md), then the interview problem **C1**

## Running any project

Each console project runs whatever scenario its `Program.Main` calls. Edit that line, then from this folder (next to the `.sln`):

```bash
dotnet run --project Samples.Test
```

(Swap `Samples.Test` for `Samples.Generic` or `Samples.Delegates`.) Requires the .NET 6 SDK or newer.

All expected outputs in these docs were captured by running the code.
