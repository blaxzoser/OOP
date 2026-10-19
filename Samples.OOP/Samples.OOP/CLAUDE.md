# CLAUDE.md

C# (.NET 6) practice solution for OOP concepts and interview questions. Solution: `Samples.BlaxZoser.sln`.

@README.md

## Per-project docs — read the relevant one before changing a project

| Project | Doc | Contents |
|---|---|---|
| `Samples.OOP` (class library) | [Samples.OOP/README.md](Samples.OOP/README.md) | Class hierarchy, what each class demonstrates, known quirks |
| `Samples.Test` (console) | [Samples.Test/README.md](Samples.Test/README.md) | All 20 OOP exercises: question, level, expected output |
| `Samples.Generics` (console, folder `Samples.Generic`) | [Samples.Generic/README.md](Samples.Generic/README.md) | Generic method/class exercises + `in`/`out` variance (V1–V6) |
| `Samples.Delegates` (console) | [Samples.Delegates/README.md](Samples.Delegates/README.md) | Delegate/`Action`/`Func` exercises + `TemplateParser` interview problem |

## Build & run

Run from this folder:

```bash
dotnet build Samples.BlaxZoser.sln
dotnet run --project Samples.Test
```

- Each console app runs only the scenario called in its `Program.Main`; change that call to run another.
- Most scenarios end with `Console.ReadKey()` / `Console.ReadLine()`. With redirected stdin, `ReadKey()`
  throws `InvalidOperationException` *after* the output is printed, so wrap it in a try/catch in a throwaway harness rather than editing scenarios.

## Conventions

- An exercise is a `public static void ScenarioX()` method in a `*Samples` class (or `PlayerConsole`),
  printing to the console. Model classes go in `Samples.OOP`; scenarios that use them go in `Samples.Test`.
- Leave the deliberate oddities alone: warning **CS0114** on `Student.Dance()` and the
  `// TODO: It should be Refactor` code in `ComplexDelegate.cs`. They are part of what the exercises teach.
- If you change a console output string, update the expected output in the matching README too.
- `fake-test-interview.md` quotes outputs and links to code lines (`file.cs#L<n>`). Update it when you
  move code, rename things or add new topics.
- **Keep the READMEs in sync:** when you add or change a scenario, update that project's README (overview
  table row, level, question it answers, expected output). Take expected output from an actual run, not from
  reasoning about the code. Note that the `Teacher` static constructor line prints once per process.
- Levels: 🟢 Beginner · 🟡 Intermediate · 🔴 Advanced / interview trap.
