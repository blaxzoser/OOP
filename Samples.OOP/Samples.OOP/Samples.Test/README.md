# Samples.Test — OOP Exercises (console runner)

| | |
|---|---|
| **Type** | Console app (`net6.0`), references [Samples.OOP](../Samples.OOP/README.md) |
| **Path** | [`Samples.OOP/Samples.OOP/Samples.Test`](.) |
| **Entry point** | [`Program.cs`](Program.cs) — currently runs `PolymorphismSamples.ScenarioNine()` |
| **C# features used** | `global using` ([`GlobalSettings.cs`](GlobalSettings.cs)), file‑scoped namespaces (C# 10) |
| **Exercises** | 20 scenarios in 5 groups |

## How to run an exercise

1. Open [`Program.cs`](Program.cs) and change the call inside `Main`, e.g. `InheritanceSamples.ScenarioFour();`
2. Run it from this folder:

```bash
dotnet run
```

Most scenarios end with `Console.ReadKey()` so the window stays open — press a key to finish.

> **Static constructor note:** `Teacher` has a static constructor. When a scenario that uses any `Teacher`
> subclass is run **on its own**, the very first line printed is
> `Assigned CurrentID=0-CALL STATIC CONSTRUCTOR`. It is shown in the expected outputs below.
> If you run several scenarios in one process it only appears once.

## Levels

| Level | Meaning |
|---|---|
| 🟢 **Beginner** | One concept, output is what you'd guess. |
| 🟡 **Intermediate** | Needs you to know a rule (static, `base(...)`, `new` vs `override`). |
| 🔴 **Advanced** | Interview‑style trap: casting + hiding + virtual dispatch combined. |

## Exercise overview

| # | Exercise | Topic | Level | Question it answers |
|---|---|---|---|---|
| E1 | `PlayerConsole.ScenarioReadonly` | Encapsulation | 🟢 | When can a `readonly` field be assigned? |
| E2 | `PlayerConsole.ScenarioPrivateSet` | Encapsulation | 🟢 | How do I expose a property read‑only to callers? |
| E3 | `PlayerConsole.ScenarioInit` | Encapsulation | 🟡 | What does the C# 9 `init` accessor allow? |
| I1 | `InheritanceSamples.ScenarioOne` | Inheritance | 🟢 | What does a derived class inherit, and in which order do constructors run? |
| I2 | `InheritanceSamples.ScenarioTwo` | Inheritance | 🟢 | Which base constructor runs if the child doesn't call one? |
| I3 | `InheritanceSamples.ScenarioThree` | Static members | 🟡 | Is static data per object or per class? |
| I4 | `InheritanceSamples.ScenarioFour` | `override` vs `new` | 🟡 | Does casting to the base change which overridden method runs? |
| I5 | `InheritanceSamples.ScenarioFive` | Method hiding | 🔴 | What does a non‑virtual base method call when the child *hides* instead of overriding? |
| P4 | `PolymorphismSamples.ScenarioFour` | Polymorphism | 🟢 | Why can a `List<Teacher>` hold different teachers and call the right `Dance()`? |
| P5 | `PolymorphismSamples.ScenarioFive` | Polymorphism | 🟢 | Does the variable's declared type matter for a `virtual` method? |
| P6 | `PolymorphismSamples.ScenarioSix` | Method hiding | 🟡 | What changes when the child uses `new` instead of `override`? |
| P7 | `PolymorphismSamples.ScenarioSeven` | `base` keyword | 🟡 | How does an override reuse the parent implementation? |
| P8 | `PolymorphismSamples.ScenarioEight` | `override` vs `new` | 🔴 | When to use `override` and when `new`, seen through a base method that calls `Dance()`. |
| P9 | `PolymorphismSamples.ScenarioNine` | `override` vs `new` + cast | 🟡 | What does `((Base)obj).Method()` call for `new` vs `override`? |
| A1 | `AbstractSamples.Scenario1` | Abstract class | 🟢 | Is each instance's state independent? |
| A2 | `AbstractSamples.Scenario2` | Abstract members | 🟢 | What must a child of an abstract class implement? |
| A3 | `AbstractSamples.Scenario3` | Constructor chaining | 🟡 | How do I pass arguments to the abstract base constructor? |
| A4 | `AbstractSamples.Scenario4` | Shadowing | 🔴 | How do I "replace" a parent method I can't mark `virtual`, and what does a cast do? |
| F1 | `InterfaceSamples.ScenarioOne` | Interface + inheritance | 🔴 | Class and interface both define `Dance()` — which runs? |
| F2 | `InterfaceSamples.ScenarioTwo` | Interfaces | 🟢 | How does a class implement an interface member? |

---

## 1. Encapsulation — [`Encapsulation/PlayerConsole.cs`](Encapsulation/PlayerConsole.cs)

Model: [`Player`](../Samples.OOP/Encapsulation/Player.cs).
`PlayerConsole` is `internal` and has namespace `Samples.Test.Encapsulation`.

### E1 · `ScenarioReadonly` 🟢 — [line 10](Encapsulation/PlayerConsole.cs:10)
**Question:** When can a `readonly` field be assigned?
**What it does:** Creates one player with the default constructor and one with `new Player(23)`.
`player._age = 23` is commented out because it would not compile.

```
New Player1 age:0
New Player2 age:23
```
**Takeaway:** `readonly` fields can only be set in a field initializer or a constructor; afterwards they're
fixed. Unset value types keep their default (`0`).

### E2 · `ScenarioPrivateSet` 🟢 — [line 22](Encapsulation/PlayerConsole.cs:22)
**Question:** How do I let a class change a property internally but keep it read‑only to callers?
**What it does:** The parameterless constructor assigns `ID = Guid.NewGuid()`; assigning `player.ID`
from outside is commented out (compile error).

```
New Player ID:<a new random GUID>
```
**Takeaway:** `{ get; private set; }` = public read, private write.
**Try it:** print `new Player(23).ID` — it's `00000000-0000-0000-0000-000000000000` because that
constructor never sets `ID`.

### E3 · `ScenarioInit` 🟡 — [line 33](Encapsulation/PlayerConsole.cs:33)
**Question:** What does the C# 9 `init` accessor allow?
**What it does:** Sets `Name` in an object initializer `new Player() { Name = "Beto" }`; changing it later is
commented out (compile error).

```
New Player Name:Beto
```
**Takeaway:** `init` = settable during object creation (constructor or object initializer), immutable after.
Compare with `readonly` (fields, constructor only) and `private set` (mutable from inside the class at any time).

---

## 2. Inheritance — [`InheritanceSamples.cs`](InheritanceSamples.cs)

### I1 · `ScenarioOne` 🟢 — [line 4](InheritanceSamples.cs:4)
**Question:** What does a derived class inherit, and in what order do constructors run?
**What it does:** Builds a `Teacher` with the 3‑arg constructor and a `Student` with its 4‑arg constructor,
then uses the inherited `Update()` and `ToString()`.

```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
++currentID--CALL GETNextID BASE
Created Teacher by args
Created Teacher-CALL CONSTRUCTOR BASE
++currentID--CALL GETNextID BASE
Created Student by args-CHILD
Override from Object
1 - Fix Bugs
Update
Override from Object
2 - Change the Design of the Base Class
```
**Takeaways:**
- Order: **static ctor (once) → base ctor → derived ctor body**.
- `Student`'s constructor has no `: base(...)`, so `Teacher()` (parameterless) runs first.
- `Student` reuses the base `protected` members (`ID`, `Title`, `GetNextID()`) and the base `ToString()` override.

### I2 · `ScenarioTwo` 🟢 — [line 37](InheritanceSamples.cs:37)
**Question:** If a child constructor doesn't call `base(...)`, which parent constructor runs?
```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
++currentID--CALL GETNextID BASE
Created Student by args-CHILD
```
**Takeaway:** the parameterless base constructor is called implicitly. If the base had no parameterless
constructor, the child would fail to compile.

### I3 · `ScenarioThree` (static data) 🟡 — [line 54](InheritanceSamples.cs:54)
**Question:** Is a `static` field stored per object or per class?
**What it does:** Two teachers, money set to `0.001` then `0.002` (both calls go through `teacher1`).

```
***** Fun with Static Data *****

Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
Created Teacher-CALL CONSTRUCTOR BASE
teacher 1: 0.002
teacher 2: 0.002
```
**Takeaways:** static data is shared by all instances; the static constructor runs once, just before the
first instance is created (note it prints *after* the header line). The long comment in the method lists
the static‑constructor rules (single per class, no access modifier, no parameters, runs once).
**Interview follow‑up:** `SetMoney`/`GetMoney` are *instance* methods touching a *static* field — legal,
but surprising for readers. Making them `static` would make the sharing explicit.

### I4 · `ScenarioFour` (`override` vs `new`) 🟡 — [line 85](InheritanceSamples.cs:85)
**Question:** If a child **overrides** a method, does casting it to the base type change what runs?
**What it does:** Calls `Dance()` on an `EnglishTeacher` through the child type, a base‑typed variable,
an explicit cast stored in a variable, and an inline cast.

```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
I don't know dance again how my father
------------------
Created Teacher-CALL CONSTRUCTOR BASE
I don't know dance again how my father
------------------
I don't know dance again how my father
------------------
I don't know dance again how my father
```
**Takeaway:** with `override`, the **runtime type** decides — every call runs `EnglishTeacher.Dance()`.
**Try it:** in [`EnglishTeacher.cs`](../Samples.OOP/EnglishTeacher.cs) swap to the commented
`public new void Dance()`. Then only the first call prints the child text; the three `Teacher`‑typed calls
print `Dance()-BASE`.

### I5 · `ScenarioFive` (hiding + non‑virtual caller) 🔴 — [line 107](InheritanceSamples.cs:107)
**Question:** `NewStyleTeacher` hides both `Dance()` and `ShowMeDance()` with `new`. What runs through the
child reference vs. a base reference?

```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
New Style-DANCE
New Show me dance foreverrrrrrrrrr
------------------
Dance()-BASE
Show me Dance-BASE
Dance()-BASE
------------------
Dance()-BASE
```
**Takeaways:**
- With `new`, the **compile‑time (declared) type** decides. Through `NewStyleTeacher` you get the new
  methods; through `Teacher` you get the originals.
- `Teacher.ShowMeDance()` calls the virtual `Dance()`, but since `NewStyleTeacher` never *overrode* it,
  dispatch lands on `Teacher.Dance()`.
- `teacher.ShowMeDance()` runs the **child's** `new ShowMeDance()`, because the variable is typed
  `NewStyleTeacher` — not the base method.

---

## 3. Polymorphism — [`PolymorphismSamples.cs`](PolymorphismSamples.cs)

> Numbering starts at Four because it continues the inheritance series.

### P4 · `ScenarioFour` 🟢 — [line 10](PolymorphismSamples.cs:10)
**Question:** Why can one `List<Teacher>` hold different teachers and still call each one's own `Dance()`?
```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
Created Teacher-CALL CONSTRUCTOR BASE
Created Teacher-CALL CONSTRUCTOR BASE
Dance - SALSA
Dance - HIPHOP
Dance - BACHATA
Created Teacher-CALL CONSTRUCTOR BASE
Dance - SALSA
```
**Takeaway:** implicit upcast (an `is‑a` relationship, no cast needed) + `virtual`/`override` = the
classic polymorphism pattern.

### P5 · `ScenarioFive` 🟢 — [line 43](PolymorphismSamples.cs:43)
**Question:** For a `virtual` method, does the declared type of the variable matter?
```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
Dance - BACHATA
Dance - BACHATA
Created Teacher-CALL CONSTRUCTOR BASE
Dance - BACHATA
```
**Takeaway:** no — an overridden method always runs the most‑derived version, whatever the variable type.

### P6 · `ScenarioSix` 🟡 — [line 63](PolymorphismSamples.cs:63)
**Question:** What changes if the child uses `new` instead of `override`?
```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
New Style-DANCE
Dance()-BASE
```
**Takeaway:** `new` opts **out** of virtual dispatch. Seen as a `Teacher`, the object uses the old method.

### P7 · `ScenarioSeven` 🟡 — [line 81](PolymorphismSamples.cs:81)
**Question:** How can an overriding method still reuse the parent's implementation?
```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
Constructor StupidTeacher -I don't wanna to teach them
Dance()-BASE
```
**Takeaway:** `base.Dance()` inside an override calls the parent version. Typical use: extend rather than
replace (`base.Dance(); /* extra work */`).

### P8 · `ScenarioEight` 🔴 — [line 94](PolymorphismSamples.cs:94)
**Question:** "Knowing when to use `override` and `new`" — predict the output of a base method
(`ShowMeDance`) that internally calls `Dance()` for four different classes.
```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
Show me Dance-BASE
Dance()-BASE
----------
Created Teacher-CALL CONSTRUCTOR BASE
New Show me dance foreverrrrrrrrrr
New Style-DANCE
----------
Created Teacher-CALL CONSTRUCTOR BASE
Show me Dance-BASE
Dance - BACHATA
----------
Created Teacher-CALL CONSTRUCTOR BASE
Constructor StupidTeacher -I don't wanna to teach them
Show me Dance-BASE
Dance()-BASE
```
**Takeaways:**
| Object | `ShowMeDance()` used | `Dance()` reached from it |
|---|---|---|
| `Teacher` | base | base |
| `NewStyleTeacher` | **its own** (`new`) — never calls `Dance()` | — |
| `BachataTeacher` | base | **child** (override → virtual dispatch) |
| `StupidTeacher` | base | child override → `base.Dance()` → base text |

### P9 · `ScenarioNine` 🟡 — [line 121](PolymorphismSamples.cs:121) · *(current `Main`)*
**Question:** What does `((Teacher)obj).Dance()` call for a `new` method vs an `override`?
```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
New Style-DANCE
Dance()-BASE
----------
Created Teacher-CALL CONSTRUCTOR BASE
Dance - BACHATA
Dance - BACHATA
```
**Takeaway (the one‑liner to remember):** **`override` → runtime type wins. `new` → declared type wins.**

---

## 4. Abstract classes — [`AbstractSamples.cs`](AbstractSamples.cs)

These use the `AbstractTeacher` hierarchy, which has no static constructor, so no `Assigned CurrentID` line.

### A1 · `Scenario1` 🟢 — [line 4](AbstractSamples.cs:4)
**Question:** Do two instances of the same class keep independent state?
```
BASE-Create Abstract class
Create AfroTeacher
Dance AFRO
Set money Afro Teacher
BASE-Create Abstract class
Create AfroTeacher
Dance AFRO
Set money Afro Teacher
my name teacher 1 is:Erick
my name teacher 2 is:Danny
```
**Takeaway:** instance properties (`Name`) are per object, unlike the static field in I3. The abstract
base constructor always runs before the child's.
*(Small slip in the code: the second `SetMoney()` is called on `afroTeacher1`; the output is identical.)*

### A2 · `Scenario2` 🟢 — [line 26](AbstractSamples.cs:26)
**Question:** What must a child of an abstract class implement, and what can it inherit as‑is?
```
BASE-Create Abstract class
Create AfroTeacher
Dance AFRO
Set money Afro Teacher
11:00
----------
BASE-Create Abstract class
Create BalletTeacher
BASE-Dance Abstract
Set money Ballet Teacher
12:00
----------
```
**Takeaway:** `abstract` members (`SetMoney()`, the `Schedule` property) **must** be overridden;
`virtual` ones (`Dance()`) are optional — `BalletTeacher` keeps the base `Dance()`.

### A3 · `Scenario3` 🟡 — [line 47](AbstractSamples.cs:47)
**Question:** How do I pass constructor arguments to an abstract base class?
```
BASE-Create Abstract class with args
Regueton Teacher Created
BASE-Dance Abstract
Set money Regueton Teacher
21:00
the name is:Luis Pintado
----------
BASE-Create Abstract class
Create BalletTeacher
BASE-Dance Abstract
Set money Ballet Teacher
12:00
the name is:Diana
----------
```
**Takeaway:** `ReguetonTeacher(string name) : base(name)` selects the base constructor with arguments;
`BalletTeacher` gets the parameterless one and sets `Name` afterwards.

### A4 · `Scenario4` (shadowing) 🔴 — [line 81](AbstractSamples.cs:81)
**Question:** How do I replace a parent's method that I can't edit (e.g. third‑party code), and what
happens when the object is cast back to the parent?
```
BASE-Create Abstract class
Create AfroTeacher
Created SuperAfroTeacher
Dance SuperAfroTeacher
Set money Afro Teacher
11:00
the name is:
----------
Dance AFRO
```
**Takeaways:**
- Three‑level constructor chain: abstract base → `AfroTeacher` → `SuperAfroTeacher`.
- `new` (shadowing) only works through the child type; `((AfroTeacher)super).Dance()` runs the parent.
- `Name` was never set, so it prints empty.
- The comment above the method explains the real‑world reason: if you own the base, prefer `override`.

---

## 5. Interfaces — [`InterfaceSamples.cs`](InterfaceSamples.cs)

`Student : Teacher, IMoveShoulder, IAction`.

### F1 · `ScenarioOne` 🔴 — [line 4](InterfaceSamples.cs:4)
**Question (classic interview):** `Teacher` has `virtual Dance()`, `IAction` declares `Dance()`, and
`Student` declares `public void Dance()` with **no** modifier. Which one runs?
```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
Created Student
Move Dance Student by Class
```
**Takeaways:**
- `Student.Dance()` **implements** `IAction.Dance()` *and* **hides** `Teacher.Dance()` (compiler warning CS0114).
- Through a `Student` (or `IAction`) reference you get `Move Dance Student by Class`.
- **Try it:** `((Teacher)student).Dance()` prints `Dance()-BASE` — the hidden method is not part of
  virtual dispatch. Adding `override` instead would make all three paths print the student text.

### F2 · `ScenarioTwo` 🟢 — [line 14](InterfaceSamples.cs:14)
**Question:** How does a class fulfil an interface contract?
```
Assigned CurrentID=0-CALL STATIC CONSTRUCTOR
Created Teacher-CALL CONSTRUCTOR BASE
Created Student
Move Shoulders Student
```
**Takeaway:** `IMoveShoulder.MoveOn()` is implemented implicitly by a public method with the same signature.

---

## Cheat sheet

| Situation | Through child reference | Through base reference |
|---|---|---|
| `override` | child | **child** |
| `override` that calls `base.X()` | child → base | child → base |
| `new` | child | **base** |
| no modifier (warning CS0114) | child | **base** (same as `new`) |
| abstract member | child (required) | child |
