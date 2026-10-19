# Samples.Generics — Generic methods, constraints & variance (`in` / `out`)

| | |
|---|---|
| **Type** | Console app (`net6.0`) |
| **Path** | [`Samples.OOP/Samples.OOP/Samples.Generic`](.) (project file: `Samples.Generics.csproj`) |
| **Entry point** | [`Program.cs`](Program.cs) — currently runs `ScenarioTwo()` |
| **Reference** | [Microsoft Docs — Generics](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/), [Covariance and contravariance](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/) (both linked in the source) |
| **Exercises** | 8 scenarios: 2 on generics basics (G1–G2), 6 on variance (V1–V6) |
| **Overall level** | Beginner → Advanced |

## How to run

Change the call in `Main` (`ScenarioOne()` / `ScenarioTwo()`, or `VarianceSamples.ScenarioOne()` … `VarianceSamples.ScenarioSix()`), then from this folder:

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
| Covariance `out T` (built‑in `IEnumerable<out T>`, `IReadOnlyList<out T>`) | V1, V4 |
| Contravariance `in T` (built‑in `Action<in T>`, `IComparer<in T>`) | V2 |
| Declaring your own variant interfaces / `Func<in T, out TResult>` | [`VarianceContracts.cs`](Variance/VarianceContracts.cs), V3 |
| Variance pitfalls: array covariance, value types, classes | V4 |
| Contravariance + generic method + constraint (validation pipeline) | V5 |
| Generic inheritance (`SalsaRepository : InMemoryRepository<SalsaDancer>`) with `in` and `out` | V6 |

## Files

| File | Role |
|---|---|
| [`SampleGeneric.cs`](SampleGeneric.cs) | Static helper with generic methods `Swap<T>` and `DisplayBaseClass<T>` |
| [`GenericList.cs`](GenericList.cs) | Singly linked list constrained to `Dancer` |
| [`Dancer.cs`](Dancer.cs) | Element type with `Name` and `ID` (base class of the variance samples) |
| [`SalsaDancer.cs`](SalsaDancer.cs), [`BachataDancer.cs`](BachataDancer.cs) | `Dancer` subtypes used to show variance (`SalsaDancer` adds `Level`) |
| [`Programmer.cs`](Programmer.cs) | Empty class used to show a type that **violates** the constraint |
| [`Program.cs`](Program.cs) | Entry point + scenarios G1–G2 |
| [`Variance/VarianceContracts.cs`](Variance/VarianceContracts.cs) | Variant interfaces: `IProducer<out T>`, `IConsumer<in T>`, `IValidator<in T>`, `IReadOnlyRepository<out T>`, `IWriteRepository<in T>` |
| [`Variance/VarianceImplementations.cs`](Variance/VarianceImplementations.cs) | Factory, printer, validators, `InMemoryRepository<T>`, `SalsaRepository`, `AuditLogRepository` |
| [`Variance/VarianceSamples.cs`](Variance/VarianceSamples.cs) | Scenarios V1–V6 |

---

## Exercises

| # | Exercise | Level | Question it answers |
|---|---|---|---|
| G1 | `ScenarioOne` | 🟢 Beginner | How do I write one method that works for any type? |
| G2 | `ScenarioTwo` | 🟡 Intermediate | How do I restrict a generic type so I can use its members? |
| V1 | `VarianceSamples.ScenarioOne` | 🟢 Beginner | Why can I pass a `List<SalsaDancer>` as `IEnumerable<Dancer>` but not as `List<Dancer>`? |
| V2 | `VarianceSamples.ScenarioTwo` | 🟢 Beginner | Why can an `Action<Dancer>` / `IComparer<Dancer>` be used for `SalsaDancer`? |
| V3 | `VarianceSamples.ScenarioThree` | 🟡 Intermediate | How do I declare my own `out` / `in` interfaces, and what are the rules? |
| V4 | `VarianceSamples.ScenarioFour` | 🟡 Intermediate | Where does variance *not* work or not protect me? |
| V5 | `VarianceSamples.ScenarioFive` | 🔴 Advanced | How do I reuse base‑type validators/handlers for every subtype? |
| V6 | `VarianceSamples.ScenarioSix` | 🔴 Advanced | How do generic inheritance, `in` and `out` work together in a repository? |

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

## Variance — `in` and `out` with generic inheritance

**Idea in one line:** `SalsaDancer : Dancer` is inheritance between *classes*. Variance decides whether that
relationship carries over to *generic types built from them*:

| Keyword | Name | Direction | Allowed when `T` is… | Example |
|---|---|---|---|---|
| `out T` | Covariance | `I<SalsaDancer>` → `I<Dancer>` (same direction) | only **returned** | `IEnumerable<out T>`, `IReadOnlyList<out T>`, `Func<out TResult>` |
| `in T` | Contravariance | `I<Dancer>` → `I<SalsaDancer>` (reversed) | only **accepted** as a parameter | `Action<in T>`, `IComparer<in T>`, `IEqualityComparer<in T>` |
| *(none)* | Invariance | no conversion | both returned and accepted | `List<T>`, `Dictionary<TKey,TValue>` |

Mnemonic: **out = things come out (producers), in = things go in (consumers).**

Model used: `Dancer` ← `SalsaDancer` (has `Level`) and `Dancer` ← `BachataDancer`.

### V1 · Covariance with `IEnumerable<out T>` 🟢 — [VarianceSamples.cs:17](Variance/VarianceSamples.cs:17)
**Question:** Why can a `List<SalsaDancer>` be passed as `IEnumerable<Dancer>`, but not assigned to `List<Dancer>`?
```
SalsaDancer: miguel
SalsaDancer: carlos
BachataDancer: kakaroto
```
**Takeaways:**
- `IEnumerable<T>` only *gives* items, so a sequence of salsa dancers is safely a sequence of dancers.
- `List<Dancer> list = salsaDancers;` is **CS0029**. If it compiled, `list.Add(new BachataDancer(...))` would
  put a bachata dancer into a salsa list.

**Real world:** write one `PrintNames(IEnumerable<Dancer>)`, `ExportToCsv(IEnumerable<Person>)` or
`SendNewsletter(IEnumerable<Customer>)` and pass lists of any subtype to it. **Take `IEnumerable<T>` /
`IReadOnlyList<T>` parameters, not `List<T>`**, so callers aren't forced to convert.

### V2 · Contravariance with `Action<in T>` and `IComparer<in T>` 🟢 — [VarianceSamples.cs:40](Variance/VarianceSamples.cs:40)
**Question:** Why can code written for `Dancer` be used where a `SalsaDancer` handler or comparer is expected?
```
Welcome diana!
Sorted: ana, luis, zoe
```
**Takeaways:**
- Anything that can handle *any* `Dancer` can certainly handle a `SalsaDancer`, which is why the direction is reversed.
- `List<SalsaDancer>.Sort` wants an `IComparer<SalsaDancer>`, and an `IComparer<Dancer>` is accepted.

**Real world:** one `IComparer<Product>` sorting by price used for `List<Book>` and `List<Laptop>`; one
`IEqualityComparer<Entity>` comparing by `Id` used in `Distinct()` / `HashSet` for every entity type; one
`Action<Exception>` logger wired to more specific error callbacks.

### V3 · Your own `out` / `in` interfaces + `Func<in T, out TResult>` 🟡 — [VarianceSamples.cs:61](Variance/VarianceSamples.cs:61)
**Question:** How do I declare variant interfaces, and what does the compiler enforce?
```
Produced SalsaDancer: salsa-101
Printing SalsaDancer: salsa-102
Described: leo#8
```
**Takeaways:**
- `IProducer<out T>` only returns `T`, so `IProducer<SalsaDancer>` can be used as `IProducer<Dancer>`.
- `IConsumer<in T>` only accepts `T`, so `IConsumer<Dancer>` can be used as `IConsumer<SalsaDancer>`.
- Using `T` in the wrong position is **CS1961** (see the commented lines in [`VarianceContracts.cs`](Variance/VarianceContracts.cs)).
- The opposite conversions (`IProducer<Dancer>` → `IProducer<SalsaDancer>`) are **CS0266**.
- `Func<Dancer, string>` can be used as `Func<SalsaDancer, object>`: the input becomes narrower and the
  output wider, in the same assignment.

**Real world:** factories and read‑only sources (`IFactory<out T>`, `IQuery<out TResult>`) are `out`;
handlers, sinks and comparers (`IHandler<in TMessage>`, `IObserver<in T>`) are `in`.

### V4 · Pitfalls: arrays, value types, classes 🟡 — [VarianceSamples.cs:85](Variance/VarianceSamples.cs:85)
**Question:** Where does variance not work, or not protect me?
```
ArrayTypeMismatchException: Attempted to access an element as a type incompatible with the array.
words: 2, boxed numbers: 3
IReadOnlyList<Dancer> first: ana
```
**Takeaways:**
1. **Arrays are covariant but unsafe**: `Dancer[] dancers = new SalsaDancer[2];` compiles, but storing a
   `BachataDancer` throws **at runtime**. Generic interfaces catch this at compile time instead.
2. **Reference types only**: `IEnumerable<string>` → `IEnumerable<object>` works, but `List<int>` →
   `IEnumerable<object>` is **CS0266** because `int` would need boxing. Use `.Cast<object>()`.
3. **Interfaces and delegates only**: `class Box<out T>` is **CS1960**. That's why `List<T>` is invariant
   while its read‑only view `IReadOnlyList<out T>` is covariant.

### V5 · Validation pipeline (contravariance + generic method + constraint) 🔴 — [VarianceSamples.cs:117](Variance/VarianceSamples.cs:117)
**Question:** How do I write a rule once for the base type and mix it with type‑specific rules?
```
SalsaDancer 'miguel' is valid
SalsaDancer '' is invalid: Name is required; Salsa level must be 1 or higher
BachataDancer 'carlos' is valid
```
**How it works:**
- `NameRequiredValidator : IValidator<Dancer>` goes into a `List<IValidator<SalsaDancer>>` next to
  `SalsaLevelValidator : IValidator<SalsaDancer>`, because `IValidator<in T>` allows it.
- The same validator class is reused in `List<IValidator<BachataDancer>>`.
- `Validate<T>(T dancer, IEnumerable<IValidator<T>> validators) where T : Dancer` is a generic method
  with a constraint, running a list that mixes base‑type and subtype validators.

**Real world:** shared validation rules (for example "every `Entity` needs an `Id`" plus "an `Order` needs
at least one line"); domain‑event / notification handlers where an `IHandler<IEvent>` audit logger
receives every event type, while `IHandler<OrderPlaced>` handles one type.

### V6 · Generic inheritance + `in` and `out` in a repository 🔴 — [VarianceSamples.cs:138](Variance/VarianceSamples.cs:138)
**Question:** How do a generic base class, a closed subclass, a covariant read interface and a
contravariant write interface fit together?
```
[audit] saved SalsaDancer miguel
[audit] saved SalsaDancer diana
Level 2+: miguel
Report: SalsaDancer miguel
Report: SalsaDancer diana
Report: BachataDancer carlos
```
**Design:**
```
IReadOnlyRepository<out T>   IWriteRepository<in T>
            ▲                          ▲
            └───── InMemoryRepository<T> where T : Dancer      (generic base class)
                          ▲
                   SalsaRepository : InMemoryRepository<SalsaDancer>   (closed generic subclass,
                                                                         adds GetByMinLevel)
AuditLogRepository : IWriteRepository<Dancer>   (accepts any dancer)
```
**Takeaways:**
- **Generic inheritance:** `SalsaRepository` closes `T = SalsaDancer`, inherits `Add`/`GetAll`, and adds a
  query that needs `Level`.
- **`out`:** `SalsaRepository` and `InMemoryRepository<BachataDancer>` both become
  `IReadOnlyRepository<Dancer>` for one `PrintReport`.
- **`in`:** `AuditLogRepository` (writes `Dancer`) is used as `IWriteRepository<SalsaDancer>`.
- `IWriteRepository<Dancer> w = salsaRepository;` is **CS0266**, because a salsa repository can't store a
  bachata dancer. Splitting read and write interfaces is what makes the read side covariant.

**Real world:** a CQRS‑style split of reads and writes. Dashboards and exports read many repositories
through one base‑type interface, and an audit or outbox writer accepts every entity type.

### Variance cheat sheet
| You have | You can use it as | Why |
|---|---|---|
| `IEnumerable<SalsaDancer>` | `IEnumerable<Dancer>` | `out`: it only gives you items |
| `Action<Dancer>` | `Action<SalsaDancer>` | `in`: it can take any dancer |
| `Func<Dancer, string>` | `Func<SalsaDancer, object>` | `in` on input, `out` on output |
| `List<SalsaDancer>` | ~~`List<Dancer>`~~ | invariant: it reads **and** writes |
| `SalsaDancer[]` | `Dancer[]` ⚠️ | compiles, but bad writes throw at runtime |
| `List<int>` | ~~`IEnumerable<object>`~~ | value types aren't variant |

---

## Interview questions this project prepares you for
1. What are generics and why are they better than using `object`? (type safety, no boxing)
2. Difference between a generic **method** and a generic **class**?
3. What constraints exist? (`class`, `struct`, `new()`, base class, interface, `notnull`, `unmanaged`)
4. Why can you write `T t = null` here but not in an unconstrained generic?
5. How does `yield return` work, and what does `foreach` need from a type?
6. What is the difference between covariance and contravariance? Give a .NET example of each.
7. Why is `IEnumerable<T>` covariant but `List<T>` is not?
8. Why can't a class be declared `class Box<out T>`?
9. Why can array covariance throw `ArrayTypeMismatchException`?
10. When would you split an interface into a read (`out`) part and a write (`in`) part?

## Notes
- Namespaces are mixed: `Program` is in `Samples.Generic`, everything else in `Samples.Generics`.
- `ScenarioOne`/`ScenarioTwo` end with `Console.ReadKey()` to keep the window open; the variance scenarios don't.
- Each `ERROR CSxxxx` comment in the variance code was checked against the compiler.
