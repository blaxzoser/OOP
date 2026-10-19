# Samples.OOP — Class Library (the domain model)

| | |
|---|---|
| **Type** | Class library (`net6.0`, no `OutputType`) |
| **Path** | [`Samples.OOP/Samples.OOP/Samples.OOP`](.) |
| **Used by** | [Samples.Test](../Samples.Test/README.md) — every exercise that uses these classes lives there |
| **Overall level** | Beginner → Advanced |

This project contains **no runnable code**. It is the set of classes (a "dance academy" of teachers and
students) that the exercises in [Samples.Test](../Samples.Test/README.md) instantiate, cast and call to show how
C# object‑oriented features behave. Each class is deliberately small and exists to demonstrate one idea.

---

## Topics covered

| Topic | Where it is shown |
|---|---|
| Base class, instance + static members, constructors | [`Teacher`](Teacher.cs) |
| Static constructor and shared static state | [`Teacher.cs:57`](Teacher.cs:57) |
| `virtual` / `override` (runtime polymorphism) | `SalsaTeacher`, `HipHopTeacher`, `BachataTeacher`, `EnglishTeacher` |
| `new` modifier (method hiding / shadowing) | `NewStyleTeacher`, `SuperAfroTeacher` |
| Calling the parent with `base.Method()` | `StupidTeacher` |
| Abstract classes, abstract methods and abstract properties | `AbstractTeacher`, `AfroTeacher`, `BalletTeacher`, `ReguetonTeacher` |
| Constructor chaining with `: base(args)` | `ReguetonTeacher` |
| Interfaces + inheriting a class at the same time | `Student : Teacher, IMoveShoulder, IAction` |
| Overriding `System.Object.ToString()` | `Teacher`, `AbstractTeacher` |
| Encapsulation: `private set`, `init` (C# 9), `readonly` | [`Encapsulation/Player.cs`](Encapsulation/Player.cs) |
| File‑scoped namespace (C# 10) | `Player.cs` |

---

## Class map

```
System.Object
├── Teacher                         (concrete base; virtual Dance(), non-virtual ShowMeDance())
│   ├── SalsaTeacher                override Dance()
│   ├── HipHopTeacher               override Dance()
│   ├── BachataTeacher              override Dance()
│   ├── EnglishTeacher              override Dance()   (a "new" version is left commented out)
│   ├── StupidTeacher               override Dance() => base.Dance()
│   ├── NewStyleTeacher             new Dance(), new ShowMeDance()   ← hiding, not overriding
│   └── Student                     : IMoveShoulder, IAction
│                                   Dance() with NO modifier → hides Teacher.Dance (warning CS0114)
│
├── AbstractTeacher  (abstract)     virtual Dance(), virtual DoClass(),
│   │                               abstract SetMoney(), abstract string Schedule { get; }
│   ├── AfroTeacher                 override Dance(), SetMoney(), Schedule = "11:00"
│   │   └── SuperAfroTeacher        new Dance()   ← shadowing a parent you "don't own"
│   ├── BalletTeacher               SetMoney(), Schedule = "12:00" (inherits base Dance())
│   └── ReguetonTeacher             ctor(string name) : base(name), Schedule = "21:00"
│
└── Player  (Encapsulation)         ID { get; private set; }, Name { get; init; }, readonly Age

interface IAction        { void Dance(); }   (internal)
interface IMoveShoulder  { void MoveOn(); }
```

---

## Class‑by‑class notes

### `Teacher` — the concrete base class
- `private static int currentID` + `protected int GetNextID()` give each instance created with the
  parameterised constructor an auto‑incremented ID. Static data is **shared by every instance**.
- `public static double money` is read/written through the **instance** methods `SetMoney(double)` /
  `GetMoney()`. Changing it through one object changes it for all (see Inheritance Scenario 3).
- `static Teacher()` runs **once**, before the first instance is created or a static member is touched.
- The default constructor does **not** call `GetNextID()`, so objects built with `new Teacher()` keep `ID = 0`.
- `Dance()` is `virtual`; `ShowMeDance()` is **not** virtual and internally calls `Dance()` — this is
  what makes the `override` vs `new` exercises interesting.

### Derived teachers
| Class | Modifier on `Dance()` | What a `Teacher`‑typed reference calls |
|---|---|---|
| `SalsaTeacher`, `HipHopTeacher`, `BachataTeacher`, `EnglishTeacher` | `override` | the derived version |
| `StupidTeacher` | `override` → `base.Dance()` | the base text, but through the override |
| `NewStyleTeacher` | `new` | **the base version** (`Dance()-BASE`) |
| `Student` | *(none — implicit hide, warning CS0114)* | **the base version** |

### `AbstractTeacher` and its children
- Cannot be instantiated; forces children to implement `SetMoney()` and the read‑only property `Schedule`.
- Provides two constructors; `ReguetonTeacher` picks the one with arguments via `: base(name)`,
  every other child silently uses the parameterless one.
- `SuperAfroTeacher` uses `new` on `Dance()` to show *shadowing*: the comment in
  [`AbstractSamples.cs`](../Samples.Test/AbstractSamples.cs) explains this is
  what you do when you cannot edit the parent (e.g. a third‑party library).

### `Student`
- Inherits a class **and** implements two interfaces.
- Its `Dance()` has no `override`/`new` keyword. That single method both **hides** `Teacher.Dance()` and
  **implements** `IAction.Dance()`. A classic interview trap — see Interface Scenario 1.

### `Player` — encapsulation
| Member | Can be set from outside? | When can it be set? |
|---|---|---|
| `Guid ID { get; private set; }` | No | Anywhere inside `Player` |
| `string Name { get; init; }` | Only in an object initializer | During construction only |
| `public readonly int Age` | No | Only in a constructor / field initializer |

Gotcha: `new Player(23)` does **not** assign an `ID`, so its `ID` is `Guid.Empty`
(`00000000-0000-0000-0000-000000000000`). Only the parameterless constructor generates a GUID.

---

## Questions this project helps answer

1. What is the difference between `override` and `new` on a method?
2. What happens when a derived class re‑declares a method with **no** modifier?
3. In which order do static constructor, base constructor and derived constructor run?
4. What does `static` data look like when several instances modify it?
5. When must a class be `abstract`, and what must its children implement?
6. How do you pass arguments to a base‑class constructor?
7. Can a class inherit from a class and implement interfaces at the same time? Which `Dance()` wins?
8. What is the difference between `readonly`, `private set` and `init`?

All of these are answered with runnable code in **[Samples.Test](../Samples.Test/README.md)**.

---

## Known quirks (kept on purpose / worth knowing)
- Building produces warning **CS0114** for `Student.Dance()` — intentional, it is the point of the
  interface exercise.
- `IAction` is `internal`, so it can only be used inside this library (the test project can't cast to it).
