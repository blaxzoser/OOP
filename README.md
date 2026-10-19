# OOP

![.NET](https://img.shields.io/badge/.NET-6.0-512BD4) ![C#](https://img.shields.io/badge/C%23-10-239120)

C# samples for **object‑oriented programming and common interview questions**, organised as small
console exercises.

Every exercise is a short `ScenarioX()` method that prints to the console. Each one answers one question,
such as *"What runs when I cast a child to its parent?"*, *"`override` or `new`?"* or *"In which order do
constructors run?"*. Each project's README has the expected output, a difficulty level and the main points to take away.

## What you'll learn

- **The four OOP pillars:** encapsulation, inheritance, polymorphism and abstraction
- **`virtual` / `override` vs `new`:** method hiding (shadowing) and calling the parent with `base`
- **Constructors:** chaining, `: base(args)`, static constructors and shared static state
- **Interfaces:** a class that inherits a class *and* implements interfaces
- **Encapsulation in modern C#:** `readonly`, `private set`, `init` (C# 9)
- **Generics:** generic methods, generic classes, `where` constraints, `yield return`
- **Delegates:** custom delegates, `Action`, `Func`, and a template‑parser interview problem with a suggested refactor

## Projects

The solution is [`Samples.BlaxZoser.sln`](Samples.OOP/Samples.OOP/Samples.BlaxZoser.sln) and contains four projects:

| Project | Type | Topics | Exercises | Level |
|---|---|---|---|---|
| [Samples.OOP](Samples.OOP/Samples.OOP/Samples.OOP/README.md) | Class library | Teacher/student class model used by the OOP exercises | — | 🟢 → 🔴 |
| [Samples.Test](Samples.OOP/Samples.OOP/Samples.Test/README.md) | Console | Encapsulation, inheritance, polymorphism, abstract classes, interfaces | 20 | 🟢 → 🔴 |
| [Samples.Generics](Samples.OOP/Samples.OOP/Samples.Generic/README.md) | Console | Generic methods/classes, constraints, iterators | 2 | 🟢 → 🟡 |
| [Samples.Delegates](Samples.OOP/Samples.OOP/Samples.Delegates/README.md) | Console | Delegates, `Action`, `Func`, interview problem | 4 | 🟢 → 🔴 |

🟢 Beginner · 🟡 Intermediate · 🔴 Advanced / interview trap

For a suggested order to work through the exercises, see the
[solution overview](Samples.OOP/Samples.OOP/README.md#suggested-learning-path).

## Repository layout

```
OOP/
├── README.md                      ← you are here
└── Samples.OOP/Samples.OOP/
    ├── Samples.BlaxZoser.sln
    ├── README.md                  ← solution overview & learning path
    ├── CLAUDE.md                  ← guidance for Claude Code
    ├── Samples.OOP/               ← class library (the model)
    ├── Samples.Test/              ← OOP exercises
    ├── Samples.Generic/           ← generics exercises
    └── Samples.Delegates/         ← delegates exercises
```

## Getting started

Requirements: the [.NET 6 SDK](https://dotnet.microsoft.com/download) or newer. You can also open the
`.sln` in Visual Studio.

```bash
git clone https://github.com/blaxzoser/OOP.git
cd OOP/Samples.OOP/Samples.OOP
dotnet build Samples.BlaxZoser.sln
dotnet run --project Samples.Test
```

Each console app runs the scenario called in its `Program.Main`. To try a different exercise, change that
line (for example to `InheritanceSamples.ScenarioFour();`) and run it again. Most scenarios wait for a key
press before they exit.

## Example

[Samples.Test](Samples.OOP/Samples.OOP/Samples.Test/README.md) exercise P9 shows the key difference between `override` and `new`:

```csharp
var derived = new NewStyleTeacher();      // Dance() declared with 'new'
derived.Dance();                          // New Style-DANCE
((Teacher)derived).Dance();               // Dance()-BASE          ← declared type wins

var derived2 = new BachataTeacher();      // Dance() declared with 'override'
derived2.Dance();                         // Dance - BACHATA
((Teacher)derived2).Dance();              // Dance - BACHATA       ← runtime type wins
```

**`override` → the runtime type wins. `new` → the declared type wins.**
