# Samples.Generics — Generic methods & constraints

| | |
|---|---|
| **Type** | Console app (`net6.0`) |
| **Path** | [`Samples.OOP/Samples.OOP/Samples.Generic`](.) (project file: `Samples.Generics.csproj`) |
| **Entry point** | [`Program.cs`](Program.cs) — currently runs `ScenarioTwo()` |
| **Reference** | [Microsoft Docs — Generics](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/) (linked in the source) |
| **Exercises** | 2 scenarios |
| **Overall level** | Beginner → Intermediate |

## How to run

Change the call in `Main` (`ScenarioOne()` or `ScenarioTwo()`), then from this folder:

```bash
dotnet run
```

## Topics covered

| Topic | Where |
|---|---|
| Generic method with a type parameter `<T>` | [`SampleGeneric.Swap<T>`](SampleGeneric.cs:9) |
| Passing by reference with `ref` | `Swap<T>(ref T a, ref T b)` |
| Inspecting a type parameter at runtime with `typeof(T)` | [`SampleGeneric.DisplayBaseClass<T>`](SampleGeneric.cs:17) (not called by any scenario) |
| Generic class | [`GenericList<T>`](GenericList.cs:7) |
| Type‑parameter constraint `where T : Dancer` | `GenericList<T>` |
| Private nested class (linked‑list `Node`) | `GenericList<T>.Node` |
| Iterator with `yield return` / `foreach`‑able class | `GenericList<T>.GetEnumerator()` |
| Tuple deconstruction in a constructor | `Dancer(string s, int i) => (Name, ID) = (s, i);` |

## Files

| File | Role |
|---|---|
| [`SampleGeneric.cs`](SampleGeneric.cs) | Static helper with generic methods `Swap<T>` and `DisplayBaseClass<T>` |
| [`GenericList.cs`](GenericList.cs) | Singly linked list constrained to `Dancer` |
| [`Dancer.cs`](Dancer.cs) | Element type with `Name` and `ID` |
| [`Programmer.cs`](Programmer.cs) | Empty class used to show a type that **violates** the constraint |
| [`Program.cs`](Program.cs) | The two scenarios |

---

## Exercises

| # | Exercise | Level | Question it answers |
|---|---|---|---|
| G1 | `ScenarioOne` | 🟢 Beginner | How do I write one method that works for any type? |
| G2 | `ScenarioTwo` | 🟡 Intermediate | How do I restrict a generic type so I can use its members? |

### G1 · `ScenarioOne` — generic method 🟢 — [line 20](Program.cs:20)
**Question:** How do I write one `Swap` that works for `int`, `string`, or any other type?
**What it does:** Swaps two `int`s using `SampleGeneric.Swap<int>(ref a, ref b)`.

```
Value of a should be A: 10
Value of b should be B:  20
----------
Value of a should be A: 20
Value of b should be B:  10
Press any key to exit.
```
**Takeaways:**
- `T` is fixed at the call site; the compiler checks types — no boxing, no casts.
- `<int>` could be omitted: the compiler **infers** `T` from the arguments.
- `ref` is required, otherwise the method would swap its local copies only.

**Try it:** `SampleGeneric.DisplayBaseClass<Dancer>();` prints
`Base class of Samples.Generics.Dancer is: System.Object.`

### G2 · `ScenarioTwo` — constraints on type parameters 🟡 — [line 39](Program.cs:39)
**Question:** Inside a generic class, how can I access members like `.Name` on a `T`?
**What it does:** Adds three dancers to the head of a `GenericList<Dancer>` and searches for `"kakaroto"`.

```
3
Press any key to exit.
```
**Takeaways:**
- `where T : Dancer` tells the compiler every `T` **is a** `Dancer`, so `current.Data.Name` compiles.
- It also implies `T` is a reference type, so `T t = null;` is legal.
- `GenericList<Programmer>` (commented out in the code) fails with **CS0311** — `Programmer` isn't a `Dancer`.
- `GetEnumerator()` with `yield return` makes the list usable in `foreach` even though it doesn't
  implement `IEnumerable<T>` (C# uses the *pattern*, not the interface).

**Try it / gotchas:**
- `foreach (var d in list) Console.WriteLine(d.Name);` prints `kakaroto`, `carlos`, `miguel` — `AddHead` inserts at the front.
- `list.FindFirstOccurrence("nobody").ID` throws `NullReferenceException` because the method returns `null` when nothing matches.

---

## Interview questions this project prepares you for
1. What are generics and why are they better than using `object`? (type safety, no boxing)
2. Difference between a generic **method** and a generic **class**?
3. What constraints exist? (`class`, `struct`, `new()`, base class, interface, `notnull`, `unmanaged`)
4. Why can you write `T t = null` here but not in an unconstrained generic?
5. How does `yield return` work, and what does `foreach` need from a type?

## Notes
- Namespaces are mixed: `Program` is in `Samples.Generic`, everything else in `Samples.Generics`.
- `ScenarioOne`/`ScenarioTwo` end with `Console.ReadKey()` to keep the window open.
