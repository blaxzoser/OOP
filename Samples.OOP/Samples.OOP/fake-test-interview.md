# Fake Technical Interview — C# OOP, Generics & Delegates

A multiple‑choice mock interview built **only from what this repository demonstrates**. Every answer links
to the README section that explains it (📖) and to the code that proves it (💻).

**How to use it:** answer each question before opening **Answer**. Count your score with the
[answer key](#answer-key) at the end.

📝 **Need a quick refresher first?** Read the **[cheat code](cheatcode.md)**: the rules, snippets and
diagrams behind every question.

| # | Topic | Questions | Level |
|---|---|---|---|
| 1 | [Encapsulation](#1-encapsulation) | Q1–Q4 | 🟢 |
| 2 | [Constructors & static members](#2-constructors--static-members) | Q5–Q8 | 🟢 🟡 |
| 3 | [`override` vs `new` (method hiding)](#3-override-vs-new-method-hiding) | Q9–Q15 | 🟡 🔴 |
| 4 | [Polymorphism](#4-polymorphism) | Q16–Q17 | 🟢 |
| 5 | [Abstract classes](#5-abstract-classes) | Q18–Q23 | 🟢 🟡 🔴 |
| 6 | [Interfaces](#6-interfaces) | Q24–Q26 | 🟢 🔴 |
| 7 | [Generics](#7-generics) | Q27–Q32 | 🟢 🟡 |
| 8 | [Variance (`in` / `out`)](#8-variance-in--out) | Q33–Q41 | 🟢 🟡 🔴 |
| 9 | [Delegates](#9-delegates) | Q42–Q48 | 🟢 🔴 |

🟢 Beginner · 🟡 Intermediate · 🔴 Advanced / interview trap

---

## 1. Encapsulation

**Q1. 🟢** `Player` declares `public readonly int Age;`. Where can `Age` be assigned?
- A) Anywhere inside `Player`
- B) Only in a field initializer or a constructor
- C) Only in an object initializer: `new Player { Age = 23 }`
- D) Anywhere, because it is `public`

<details><summary>Answer</summary>

**B.** After construction a `readonly` field is fixed, and `player.Age = 23` doesn't compile.
📖 [Samples.Test · E1](Samples.Test/README.md#1-encapsulation--encapsulationplayerconsolecs) ·
💻 [Player.cs](Samples.OOP/Encapsulation/Player.cs#L8)
</details>

**Q2. 🟢** `Player` declares `public string Name { get; init; }` (C# 9). Which line compiles?
- A) `var p = new Player() { Name = "Beto" };`
- B) `var p = new Player(); p.Name = "Beto";`
- C) Both
- D) Neither

<details><summary>Answer</summary>

**A.** `init` allows setting during object creation (constructor or object initializer) only.
📖 [Samples.Test · E3](Samples.Test/README.md#1-encapsulation--encapsulationplayerconsolecs) ·
💻 [PlayerConsole.cs](Samples.Test/Encapsulation/PlayerConsole.cs#L33)
</details>

**Q3. 🟢** What does `public Guid ID { get; private set; }` mean?
- A) Nobody can change `ID` after construction, not even `Player`
- B) Only derived classes can set `ID`
- C) Everyone can read `ID`; only code inside `Player` can set it, at any time
- D) It's the same as `init`

<details><summary>Answer</summary>

**C.** It's public to read and private to write. Unlike `init`, the class can change it again later.
📖 [Samples.Test · E2](Samples.Test/README.md#1-encapsulation--encapsulationplayerconsolecs) ·
💻 [Player.cs](Samples.OOP/Encapsulation/Player.cs#L5)
</details>

**Q4. 🟡** What is the value of `new Player(23).ID`?
```csharp
public class Player
{
    public Guid ID { get; private set; }
    public readonly int Age;

    public Player()          { ID = Guid.NewGuid(); }   // constructor 1: sets ID
    public Player(int age)   { this.Age = age; }        // constructor 2: only sets Age
}

var p = new Player(23);     // which constructor runs? does anything set ID?
Console.WriteLine(p.ID);
```
🔗 Source: [Player.cs · both constructors](Samples.OOP/Encapsulation/Player.cs#L9-L17)

> 💡 Context: `Guid` is a **struct** (value type), so it can never be `null`. An unassigned struct property holds its default value.

- A) A new random GUID
- B) `null`
- C) It doesn't compile
- D) `00000000-0000-0000-0000-000000000000` (`Guid.Empty`)

<details><summary>Answer</summary>

**D.** Only the parameterless constructor calls `Guid.NewGuid()`. `Player(int age)` leaves `ID` at its default.
📖 [Samples.Test · E2 "Try it"](Samples.Test/README.md#1-encapsulation--encapsulationplayerconsolecs) ·
💻 [Player.cs](Samples.OOP/Encapsulation/Player.cs#L14)
</details>

---

## 2. Constructors & static members

**Q5. 🟢** `Student : Teacher`. In a fresh program, `new Student("t", "d", span, 1)` runs. In what order?
```csharp
class Teacher
{
    static Teacher() { Print("Assigned CurrentID=0-CALL STATIC CONSTRUCTOR"); }   // static ctor
    public Teacher() { Print("Created Teacher-CALL CONSTRUCTOR BASE"); }          // parameterless ctor
    public Teacher(string title, string desc, TimeSpan joblen) { /* ... */ }
}

class Student : Teacher
{
    public Student(string title, string desc, TimeSpan jobLen, int originalID)    // no ': base(...)'
    {
        // ... sets ID, Title, Description, jobLength, originalItemID
        Print("Created Student by args-CHILD");
    }
}

// First line of the program: nothing has touched Teacher yet
new Student("t", "d", span, 1);
```
🔗 Source: [Teacher.cs · constructors](Samples.OOP/Teacher.cs#L35-L61) · [Student.cs · 4‑arg constructor](Samples.OOP/Student.cs#L16-L31) · [InheritanceSamples.cs · the call](Samples.Test/InheritanceSamples.cs#L43-L46)

- A) `Student` ctor → `Teacher()` → static `Teacher()`
- B) static `Teacher()` → `Teacher()` → `Student` ctor body
- C) `Teacher()` → static `Teacher()` → `Student` ctor body
- D) static `Teacher()` → `Student` ctor body → `Teacher()`

<details><summary>Answer</summary>

**B.** The static constructor runs first (once per program), then the base constructor, then the derived constructor body.
📖 [Samples.Test · I1](Samples.Test/README.md#2-inheritance--inheritancesamplescs) ·
💻 [Teacher.cs](Samples.OOP/Teacher.cs#L57)
</details>

**Q6. 🟢** `Student`'s 4‑argument constructor has no `: base(...)`. Which `Teacher` constructor runs?
```csharp
class Teacher
{
    public Teacher() { ... }                                          // (1) parameterless
    public Teacher(string title, string desc, TimeSpan joblen) { ... } // (2) three arguments
}

class Student : Teacher
{
    // Takes title, desc, jobLen (+ originalID) but does NOT write ': base(title, desc, jobLen)'
    public Student(string title, string desc, TimeSpan jobLen, int originalID)
    {
        this.Title = title;          // sets the inherited properties itself
        ...
    }
}
```
🔗 Source: [Teacher.cs · constructor (1)](Samples.OOP/Teacher.cs#L35-L42) · [Teacher.cs · constructor (2)](Samples.OOP/Teacher.cs#L45-L52) · [Student.cs · 4‑arg constructor](Samples.OOP/Student.cs#L16-L31)

- A) None
- B) `Teacher(string, string, TimeSpan)`, because the arguments match
- C) The parameterless `Teacher()`, implicitly
- D) It's a compile error

<details><summary>Answer</summary>

**C.** The parameterless base constructor is called implicitly. If `Teacher` had no parameterless constructor, `Student` wouldn't compile.
📖 [Samples.Test · I2](Samples.Test/README.md#2-inheritance--inheritancesamplescs) ·
💻 [Student.cs](Samples.OOP/Student.cs#L16)
</details>

**Q7. 🟡** Which statement about **static constructors** is **false**?
- A) A class can have only one
- B) It can take parameters and an access modifier
- C) It runs exactly once
- D) It runs before the first instance is created or a static member is used

<details><summary>Answer</summary>

**B.** A static constructor has no access modifier and no parameters.
📖 [Samples.Test · I3](Samples.Test/README.md#2-inheritance--inheritancesamplescs) ·
💻 [InheritanceSamples.cs](Samples.Test/InheritanceSamples.cs#L54)
</details>

**Q8. 🟡** `Teacher.money` is `static`. What does this print?
```csharp
Teacher teacher1 = new Teacher();
teacher1.SetMoney(0.001);
Teacher teacher2 = new Teacher();
teacher1.SetMoney(0.002);
Console.WriteLine($"{teacher1.GetMoney()} {teacher2.GetMoney()}");
```
- A) `0.002 0.03`
- B) `0.002 0.001`
- C) `0.001 0.002`
- D) `0.002 0.002`

<details><summary>Answer</summary>

**D.** Static data belongs to the class and is shared by every instance.
📖 [Samples.Test · I3](Samples.Test/README.md#2-inheritance--inheritancesamplescs) ·
💻 [Teacher.cs](Samples.OOP/Teacher.cs#L18)
</details>

---

## 3. `override` vs `new` (method hiding)

### Classes used in this section

Pseudo‑code of the real classes, without constructors and unrelated members. `Print(x)` stands for `Console.WriteLine(x)`.

```csharp
class Teacher                                         // the base class of every class below
{
    public virtual void Dance() => Print("Dance()-BASE");

    public void ShowMeDance()                         // NOT virtual
    {
        Print("Show me Dance-BASE");
        Dance();                                      // virtual call made from inside the base class
    }
}

class EnglishTeacher : Teacher { public override void Dance() => Print("I don't know dance again how my father"); }
class BachataTeacher : Teacher { public override void Dance() => Print("Dance - BACHATA"); }
class StupidTeacher  : Teacher { public override void Dance() => base.Dance(); }

class NewStyleTeacher : Teacher
{
    public new void Dance()       => Print("New Style-DANCE");
    public new void ShowMeDance() => Print("New Show me dance foreverrrrrrrrrr");
}

class Student : Teacher, IMoveShoulder, IAction
{
    public void Dance() => Print("Move Dance Student by Class");   // no 'override', no 'new'
}
```

How the classes relate:

```
                              Teacher
               virtual Dance() · ShowMeDance() (not virtual)
                                 │
     ┌───────────────┬───────────┼───────────────┬─────────────────┐
EnglishTeacher  BachataTeacher  StupidTeacher  NewStyleTeacher     Student
override        override        override       new Dance()         Dance() (no modifier)
Dance()         Dance()         Dance()        new ShowMeDance()   + implements IAction
```

| Class | `Dance()` | `ShowMeDance()` | Source |
|---|---|---|---|
| `Teacher` | `virtual` | its own, not virtual | [Teacher.cs](Samples.OOP/Teacher.cs#L89-L98) |
| `EnglishTeacher` | `override` | inherited | [EnglishTeacher.cs](Samples.OOP/EnglishTeacher.cs#L23-L26) |
| `BachataTeacher` | `override` | inherited | [BachataTeacher.cs](Samples.OOP/BachataTeacher.cs#L9-L12) |
| `StupidTeacher` | `override` → `base.Dance()` | inherited | [StupidTeacher.cs](Samples.OOP/StupidTeacher.cs#L15-L18) |
| `NewStyleTeacher` | `new` | `new` | [NewStyleTeacher.cs](Samples.OOP/NewStyleTeacher.cs#L9-L17) |
| `Student` | no modifier (warning CS0114) | inherited | [Student.cs](Samples.OOP/Student.cs#L40-L43) |

Two words used in the questions:
- **Declared type:** the type of the variable or cast (`Teacher t = …` or `(Teacher)x`).
- **Runtime type:** the class actually created with `new …()`.

**Q9. 🟢** `EnglishTeacher` **overrides** `Dance()`. What runs for `Teacher t = new EnglishTeacher(); t.Dance();`?
```csharp
class Teacher                  { public virtual  void Dance() => Print("Dance()-BASE"); }
class EnglishTeacher : Teacher { public override void Dance() => Print("I don't know dance again how my father"); }

Teacher t = new EnglishTeacher();   // declared type: Teacher · runtime type: EnglishTeacher
t.Dance();                          // which Dance() runs?
```
🔗 Source: [Teacher.cs · `virtual Dance`](Samples.OOP/Teacher.cs#L89) · [EnglishTeacher.cs · `override Dance`](Samples.OOP/EnglishTeacher.cs#L23) · [InheritanceSamples.cs · the call](Samples.Test/InheritanceSamples.cs#L91-L92)

- A) `EnglishTeacher.Dance()`
- B) `Teacher.Dance()`
- C) Both
- D) It needs an explicit cast to compile

<details><summary>Answer</summary>

**A.** With `override`, the **runtime type** decides, whatever the variable type or cast.
📖 [Samples.Test · I4](Samples.Test/README.md#2-inheritance--inheritancesamplescs) ·
💻 [EnglishTeacher.cs](Samples.OOP/EnglishTeacher.cs#L23)
</details>

**Q10. 🟡** `NewStyleTeacher` declares `public new void Dance()`. What does `((Teacher)new NewStyleTeacher()).Dance();` print?
```csharp
class Teacher                   { public virtual void Dance() => Print("Dance()-BASE"); }
class NewStyleTeacher : Teacher { public new     void Dance() => Print("New Style-DANCE"); }

var derived = new NewStyleTeacher();
((Teacher)derived).Dance();          // after the cast, the declared type is Teacher
```
🔗 Source: [NewStyleTeacher.cs · `new Dance`](Samples.OOP/NewStyleTeacher.cs#L9) · [PolymorphismSamples.cs · the call](Samples.Test/PolymorphismSamples.cs#L123-L125)

- A) `New Style-DANCE`
- B) `Dance()-BASE`
- C) Both lines
- D) Runtime `InvalidCastException`

<details><summary>Answer</summary>

**B.** `new` opts out of virtual dispatch, so through a `Teacher` reference the base method runs.
📖 [Samples.Test · P6 / P9](Samples.Test/README.md#3-polymorphism--polymorphismsamplescs) ·
💻 [NewStyleTeacher.cs](Samples.OOP/NewStyleTeacher.cs#L9)
</details>

**Q11. 🟡** Which one‑liner correctly summarises `override` vs `new`? Both objects below are called through a `Teacher` cast:
```csharp
var derived  = new NewStyleTeacher();   // new      Dance()
var derived2 = new BachataTeacher();    // override Dance()

((Teacher)derived).Dance();             // prints "Dance()-BASE"
((Teacher)derived2).Dance();            // prints "Dance - BACHATA"
```
🔗 Source: [PolymorphismSamples.cs · ScenarioNine](Samples.Test/PolymorphismSamples.cs#L121-L130) · [NewStyleTeacher.cs](Samples.OOP/NewStyleTeacher.cs#L9) · [BachataTeacher.cs](Samples.OOP/BachataTeacher.cs#L9)

- A) `override` → declared type wins; `new` → runtime type wins
- B) Both always run the most‑derived method
- C) `override` → runtime type wins; `new` → declared (compile‑time) type wins
- D) `new` only works on abstract methods

<details><summary>Answer</summary>

**C.** This is the key takeaway of exercise P9.

| Called through… | `override` (English, Bachata, Stupid) | `new` / no modifier (NewStyle, Student) |
|---|---|---|
| A child variable | child method | child method |
| A `Teacher` variable or cast | **child method** (runtime type) | **`Teacher` method** (declared type) |

📖 [Samples.Test · cheat sheet](Samples.Test/README.md#cheat-sheet) ·
💻 [PolymorphismSamples.cs](Samples.Test/PolymorphismSamples.cs#L121)
</details>

**Q12. 🔴** `Teacher.Dance()` is `virtual`. `Student` declares `public void Dance()` with **no** modifier. What happens?
```csharp
class Teacher { public virtual void Dance() => Print("Dance()-BASE"); }

class Student : Teacher, IMoveShoulder, IAction
{
    public void Dance() => Print("Move Dance Student by Class");   // no 'override', no 'new'
}
```
🔗 Source: [Student.cs · `Dance`](Samples.OOP/Student.cs#L40) · [Teacher.cs · `virtual Dance`](Samples.OOP/Teacher.cs#L89) · [IAction.cs](Samples.OOP/IAction.cs#L9)

- A) Compile error
- B) It silently becomes an `override`
- C) The base method is called and `Student.Dance()` is ignored
- D) It compiles with warning **CS0114** and *hides* `Teacher.Dance()`, like `new`

<details><summary>Answer</summary>

**D.** With no modifier the method hides the base one. The compiler warns and treats it as `new`. The same method also implements `IAction.Dance()`.
📖 [Samples.Test · F1](Samples.Test/README.md#5-interfaces--interfacesamplescs) ·
💻 [Student.cs](Samples.OOP/Student.cs#L40)
</details>

**Q13. 🔴** `Teacher.ShowMeDance()` is **not** virtual and calls the virtual `Dance()`. `BachataTeacher` overrides `Dance()`. What does `new BachataTeacher().ShowMeDance()` print?
```csharp
class Teacher
{
    public virtual void Dance() => Print("Dance()-BASE");
    public void ShowMeDance() { Print("Show me Dance-BASE"); Dance(); }   // NOT virtual
}
class BachataTeacher : Teacher { public override void Dance() => Print("Dance - BACHATA"); }

new BachataTeacher().ShowMeDance();   // ShowMeDance is inherited, and Dance() inside it is a virtual call
```
🔗 Source: [Teacher.cs · `ShowMeDance`](Samples.OOP/Teacher.cs#L94-L98) · [BachataTeacher.cs · `override Dance`](Samples.OOP/BachataTeacher.cs#L9) · [PolymorphismSamples.cs · the call](Samples.Test/PolymorphismSamples.cs#L105-L106)

- A) `Show me Dance-BASE` then `Dance - BACHATA`
- B) `Show me Dance-BASE` then `Dance()-BASE`
- C) `Dance - BACHATA` only
- D) It doesn't compile

<details><summary>Answer</summary>

**A.** The base method runs, but its call to `Dance()` is virtually dispatched to the override.
📖 [Samples.Test · P8](Samples.Test/README.md#3-polymorphism--polymorphismsamplescs) ·
💻 [PolymorphismSamples.cs](Samples.Test/PolymorphismSamples.cs#L94)
</details>

**Q14. 🔴** `NewStyleTeacher` hides **both** methods with `new`. Which output comes from `Teacher t = new NewStyleTeacher(); t.ShowMeDance();`?
```csharp
class Teacher
{
    public virtual void Dance() => Print("Dance()-BASE");
    public void ShowMeDance() { Print("Show me Dance-BASE"); Dance(); }
}
class NewStyleTeacher : Teacher
{
    public new void Dance()       => Print("New Style-DANCE");
    public new void ShowMeDance() => Print("New Show me dance foreverrrrrrrrrr");
}

Teacher t = new NewStyleTeacher();   // declared type: Teacher · runtime type: NewStyleTeacher
t.ShowMeDance();                     // which ShowMeDance()? and which Dance() inside it?
```
🔗 Source: [NewStyleTeacher.cs](Samples.OOP/NewStyleTeacher.cs#L9-L17) · [Teacher.cs · `ShowMeDance`](Samples.OOP/Teacher.cs#L94-L98) · [InheritanceSamples.cs · the call](Samples.Test/InheritanceSamples.cs#L115-L117)

- A) `New Show me dance foreverrrrrrrrrr`
- B) `Show me Dance-BASE` then `New Style-DANCE`
- C) `Show me Dance-BASE` then `Dance()-BASE`
- D) `New Show me dance foreverrrrrrrrrr` then `New Style-DANCE`

<details><summary>Answer</summary>

**C.** Through `Teacher` you get the base `ShowMeDance()`. Its virtual `Dance()` call finds no override, because `new` isn't an override, so the base `Dance()` runs.
📖 [Samples.Test · I5](Samples.Test/README.md#2-inheritance--inheritancesamplescs) ·
💻 [InheritanceSamples.cs](Samples.Test/InheritanceSamples.cs#L107)
</details>

**Q15. 🟡** `StupidTeacher` overrides `Dance()` with `base.Dance();`. What does `new StupidTeacher().Dance()` print (ignoring constructor lines)?
```csharp
class Teacher { public virtual void Dance() => Print("Dance()-BASE"); }

class StupidTeacher : Teacher
{
    public override void Dance() { base.Dance(); }   // 'base' = Teacher
}

new StupidTeacher().Dance();
```
🔗 Source: [StupidTeacher.cs · `override Dance`](Samples.OOP/StupidTeacher.cs#L15-L18) · [Teacher.cs · `virtual Dance`](Samples.OOP/Teacher.cs#L89) · [PolymorphismSamples.cs · the call](Samples.Test/PolymorphismSamples.cs#L81-L84)

- A) Nothing
- B) `Dance()-BASE`
- C) Infinite recursion
- D) Compile error: you can't call `base` from an override

<details><summary>Answer</summary>

**B.** `base.Dance()` calls the parent implementation. It's the usual way to *extend* behaviour rather than replace it.
📖 [Samples.Test · P7](Samples.Test/README.md#3-polymorphism--polymorphismsamplescs) ·
💻 [StupidTeacher.cs](Samples.OOP/StupidTeacher.cs#L15)
</details>

---

## 4. Polymorphism

**Q16. 🟢** What does the loop print (ignoring constructor lines)?
```csharp
class Teacher                  { public virtual  void Dance() => Print("Dance()-BASE"); }
class SalsaTeacher   : Teacher { public override void Dance() => Print("Dance - SALSA"); }
class HipHopTeacher  : Teacher { public override void Dance() => Print("Dance - HIPHOP"); }
class BachataTeacher : Teacher { public override void Dance() => Print("Dance - BACHATA"); }

var teachers = new List<Teacher> { new SalsaTeacher(), new HipHopTeacher(), new BachataTeacher() };
foreach (var t in teachers) t.Dance();   // declared type of t: Teacher · runtime type: changes each loop
```
🔗 Source: [SalsaTeacher.cs](Samples.OOP/SalsaTeacher.cs#L9-L12) · [HipHopTeacher.cs](Samples.OOP/HipHopTeacher.cs#L9-L12) · [BachataTeacher.cs](Samples.OOP/BachataTeacher.cs#L9-L12) · [PolymorphismSamples.cs · the loop](Samples.Test/PolymorphismSamples.cs#L17-L29)

- A) `Dance()-BASE` three times
- B) `Dance - SALSA`, `Dance - HIPHOP`, `Dance - BACHATA`
- C) Compile error: you need to cast each item
- D) Only `Dance - SALSA`

<details><summary>Answer</summary>

**B.** Each object runs its own override. This is classic runtime polymorphism.
📖 [Samples.Test · P4](Samples.Test/README.md#3-polymorphism--polymorphismsamplescs) ·
💻 [PolymorphismSamples.cs](Samples.Test/PolymorphismSamples.cs#L10)
</details>

**Q17. 🟢** Why can a `SalsaTeacher` be added to a `List<Teacher>` without a cast?
```csharp
public class SalsaTeacher : Teacher { ... }      // SalsaTeacher derives from Teacher

var teachers = new List<Teacher>();              // List<T>.Add(T item) → here Add(Teacher item)
teachers.Add(new SalsaTeacher());                // no (Teacher) cast written
Teacher teacher = new SalsaTeacher();            // same idea with a plain variable
```
🔗 Source: [SalsaTeacher.cs · class declaration](Samples.OOP/SalsaTeacher.cs#L7) · [PolymorphismSamples.cs · list and variable](Samples.Test/PolymorphismSamples.cs#L12-L32)

- A) Because `List<T>` accepts any object
- B) Because of `dynamic`
- C) Because an implicit conversion (upcast) exists from a derived class to its base class: a `SalsaTeacher` *is a* `Teacher`
- D) Because `SalsaTeacher` implements `IList`

<details><summary>Answer</summary>

**C.**
📖 [Samples.Test · P4](Samples.Test/README.md#3-polymorphism--polymorphismsamplescs) ·
💻 [SalsaTeacher.cs](Samples.OOP/SalsaTeacher.cs#L9)
</details>

---

## 5. Abstract classes

**Q18. 🟢** `AbstractTeacher` has `abstract void SetMoney()`, `abstract string Schedule { get; }` and `virtual void Dance()`. What **must** a non‑abstract child implement?
```csharp
public abstract class AbstractTeacher
{
    public virtual void Dance()   => Print("BASE-Dance Abstract");     // has a body
    public virtual void DoClass() => Print("BASE-DoClass Abstract");   // has a body
    public abstract void SetMoney();                                   // no body
    public abstract string Schedule { get; }                           // no body
}

public class BalletTeacher : AbstractTeacher   // a concrete (non-abstract) child
{
    // ??? which members must appear here for this class to compile?
}
```
🔗 Source: [AbstractTeacher.cs · members](Samples.OOP/AbstractTeacher.cs#L21-L34) · [BalletTeacher.cs · a real child](Samples.OOP/BalletTeacher.cs#L7-L19) · [AbstractSamples.cs · Scenario2](Samples.Test/AbstractSamples.cs#L26-L44)

- A) `SetMoney()` and `Schedule`
- B) `SetMoney()`, `Schedule` and `Dance()`
- C) Only `Dance()`
- D) Nothing

<details><summary>Answer</summary>

**A.** `abstract` members are mandatory; `virtual` ones are optional. `BalletTeacher` keeps the base `Dance()`.
📖 [Samples.Test · A2](Samples.Test/README.md#4-abstract-classes--abstractsamplescs) ·
💻 [AbstractTeacher.cs](Samples.OOP/AbstractTeacher.cs#L29)
</details>

**Q19. 🟢** What happens with `var t = new AbstractTeacher();`?
- A) It works and uses the parameterless constructor
- B) Compile error: an abstract class can't be instantiated
- C) Runtime exception
- D) It creates an anonymous subclass

<details><summary>Answer</summary>

**B.** Abstract classes only serve as base classes, though they *can* have constructors that children call.
📖 [Samples.OOP · class map](Samples.OOP/README.md#class-map) ·
💻 [AbstractTeacher.cs](Samples.OOP/AbstractTeacher.cs#L7)
</details>

**Q20. 🟡** How does `ReguetonTeacher(string name)` pass `name` to the `AbstractTeacher(string name)` constructor?
- A) `this.Name = name;` is the only way
- B) `super(name)`
- C) It's automatic, because the parameter names match
- D) `public ReguetonTeacher(string name) : base(name)`

<details><summary>Answer</summary>

**D.** `: base(args)` picks which base constructor runs. Without it, the parameterless one is used.
📖 [Samples.Test · A3](Samples.Test/README.md#4-abstract-classes--abstractsamplescs) ·
💻 [ReguetonTeacher.cs](Samples.OOP/ReguetonTeacher.cs#L12)
</details>

**Q21. 🟡** In what order do the constructors print for `new SuperAfroTeacher()` (`SuperAfroTeacher : AfroTeacher : AbstractTeacher`)?
```csharp
public abstract class AbstractTeacher
{
    public AbstractTeacher() { Print("BASE-Create Abstract class"); }
}
public class AfroTeacher : AbstractTeacher
{
    public AfroTeacher() { Print("Create AfroTeacher"); }        // implicit ': base()'
}
public class SuperAfroTeacher : AfroTeacher
{
    public SuperAfroTeacher() { Print("Created SuperAfroTeacher"); }   // implicit ': base()'
}

new SuperAfroTeacher();   // three constructors print — in which order?
```
🔗 Source: [AbstractTeacher.cs](Samples.OOP/AbstractTeacher.cs#L11-L14) · [AfroTeacher.cs](Samples.OOP/AfroTeacher.cs#L9-L12) · [SuperAfroTeacher.cs](Samples.OOP/SuperAfroTeacher.cs#L9-L12) · [AbstractSamples.cs · the call](Samples.Test/AbstractSamples.cs#L83)

- A) `Created SuperAfroTeacher` → `Create AfroTeacher` → `BASE-Create Abstract class`
- B) `BASE-Create Abstract class` → `Create AfroTeacher` → `Created SuperAfroTeacher`
- C) `Create AfroTeacher` → `BASE-Create Abstract class` → `Created SuperAfroTeacher`
- D) Only `Created SuperAfroTeacher`

<details><summary>Answer</summary>

**B.** Constructors always run from the top of the hierarchy down.
📖 [Samples.Test · A4](Samples.Test/README.md#4-abstract-classes--abstractsamplescs) ·
💻 [AbstractSamples.cs](Samples.Test/AbstractSamples.cs#L81)
</details>

**Q22. 🔴** `SuperAfroTeacher` declares `public new void Dance()`. What does `((AfroTeacher)super).Dance();` print?
```csharp
public abstract class AbstractTeacher { public virtual  void Dance() => Print("BASE-Dance Abstract"); }
public class AfroTeacher : AbstractTeacher { public override void Dance() => Print("Dance AFRO"); }
public class SuperAfroTeacher : AfroTeacher { public new void Dance() => Print("Dance SuperAfroTeacher"); }

SuperAfroTeacher super = new SuperAfroTeacher();
super.Dance();                 // declared type: SuperAfroTeacher
((AfroTeacher)super).Dance();  // declared type after the cast: AfroTeacher · runtime type: SuperAfroTeacher
```
🔗 Source: [AbstractTeacher.cs · `virtual Dance`](Samples.OOP/AbstractTeacher.cs#L21-L24) · [AfroTeacher.cs · `override Dance`](Samples.OOP/AfroTeacher.cs#L22-L25) · [SuperAfroTeacher.cs · `new Dance`](Samples.OOP/SuperAfroTeacher.cs#L14-L17) · [AbstractSamples.cs · the calls](Samples.Test/AbstractSamples.cs#L83-L89)

> 💡 Context: `new` hides `AfroTeacher.Dance()` instead of overriding it, so the "most‑derived override" seen through an `AfroTeacher` reference is still `AfroTeacher`'s own.

- A) `Dance SuperAfroTeacher`
- B) `BASE-Dance Abstract`
- C) `Dance AFRO`
- D) Both A and C

<details><summary>Answer</summary>

**C.** Shadowing with `new` only works through the child type. Cast to the parent, you get the parent's `Dance()`.
📖 [Samples.Test · A4](Samples.Test/README.md#4-abstract-classes--abstractsamplescs) ·
💻 [SuperAfroTeacher.cs](Samples.OOP/SuperAfroTeacher.cs#L14)
</details>

**Q23. 🔴** When is shadowing with `new` the right tool rather than `override`?
```csharp
// Imagine AfroTeacher ships in a NuGet package you bought: you only have the compiled DLL.
public class AfroTeacher                           // simplified: base class and other members left out
{
    public void Dance() => Print("Dance AFRO");    // suppose it is NOT virtual → you can't override it
}

// Your code:
public class SuperAfroTeacher : AfroTeacher
{
    public override void Dance() { ... }   // ❌ CS0506: cannot override, the member isn't virtual
    public new      void Dance() { ... }   // ✅ compiles: hides the parent's method
}
```
🔗 Source: [AbstractSamples.cs · the explanation in the comment](Samples.Test/AbstractSamples.cs#L70-L80) · [SuperAfroTeacher.cs · `new Dance`](Samples.OOP/SuperAfroTeacher.cs#L14-L17)

> 💡 Context: in the repo `AfroTeacher.Dance()` *is* an `override` (so it's virtual). The snippet above is the scenario the comment in `AbstractSamples.cs` describes: a base class you don't own.

- A) Always, because it's faster
- B) When you can't make the base method `virtual`, for example because it's in a third‑party library you can't edit
- C) When the base method is `abstract`
- D) Never, because it doesn't compile

<details><summary>Answer</summary>

**B.** If you own the base class, prefer `virtual`/`override`.
📖 [Samples.Test · A4](Samples.Test/README.md#4-abstract-classes--abstractsamplescs) ·
💻 [AbstractSamples.cs (comment)](Samples.Test/AbstractSamples.cs#L70)
</details>

---

## 6. Interfaces

**Q24. 🟢** Which declaration is valid C#?
- A) `class Student : Teacher, AbstractTeacher`
- B) `class Student : IMoveShoulder, Teacher`
- C) `class Student : Teacher, IMoveShoulder, IAction`
- D) `interface IAction : Teacher`

<details><summary>Answer</summary>

**C.** One base class, listed first, plus any number of interfaces.
📖 [Samples.OOP · class map](Samples.OOP/README.md#class-map) ·
💻 [Student.cs](Samples.OOP/Student.cs#L7)
</details>

**Q25. 🔴** `Student.Dance()` (no modifier) implements `IAction.Dance()` and hides `Teacher.Dance()`. What does `((Teacher)student).Dance();` print?
- A) `Move Dance Student by Class`
- B) `Dance()-BASE`
- C) Compile error: the call is ambiguous
- D) Nothing

<details><summary>Answer</summary>

**B.** The hidden method isn't part of virtual dispatch. Through `Student` or `IAction` you'd get `Move Dance Student by Class`.
📖 [Samples.Test · F1](Samples.Test/README.md#5-interfaces--interfacesamplescs) ·
💻 [InterfaceSamples.cs](Samples.Test/InterfaceSamples.cs#L4)
</details>

**Q26. 🟢** How does `Student` satisfy `IMoveShoulder.MoveOn()`?
```csharp
public interface IMoveShoulder
{
    void MoveOn();                         // the contract: no body, no access modifier
}

public class Student : Teacher, IMoveShoulder, IAction
{
    public void MoveOn() => Print("Move Shoulders Student");   // no 'override', no 'implements'
}

var student = new Student();
student.MoveOn();
IMoveShoulder mover = student;   // the same object seen through the interface
mover.MoveOn();
```
🔗 Source: [IMoveShoulder.cs](Samples.OOP/IMoveShoulder.cs#L7-L10) · [Student.cs · `MoveOn`](Samples.OOP/Student.cs#L33-L36) · [InterfaceSamples.cs · ScenarioTwo](Samples.Test/InterfaceSamples.cs#L14-L17)

- A) With the `implements` keyword
- B) Implicitly, with a `public void MoveOn()` method that has the same signature
- C) With `override void MoveOn()`
- D) It inherits it from `Teacher`

<details><summary>Answer</summary>

**B.**
📖 [Samples.Test · F2](Samples.Test/README.md#5-interfaces--interfacesamplescs) ·
💻 [Student.cs](Samples.OOP/Student.cs#L33)
</details>

---

## 7. Generics

**Q27. 🟢** Why does `Swap<T>(ref T a, ref T b)` need `ref`?
```csharp
public static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}

var a = 10;
var b = 20;
SampleGeneric.Swap<int>(ref a, ref b);
Console.WriteLine($"{a} {b}");   // prints "20 10"

// Thought experiment: remove both 'ref' keywords (signature and call).
// void Swap<T>(T a, T b) { ... } → what would a and b print after the call?
```
🔗 Source: [SampleGeneric.cs · `Swap<T>`](Samples.Generic/SampleGeneric.cs#L9-L15) · [Program.cs · ScenarioOne](Samples.Generic/Program.cs#L20-L29)

> 💡 Context: `int` is a value type, so passing it *by value* hands the method a copy. `ref` passes the caller's variable itself.

- A) Because generics require `ref`
- B) Without `ref` it would only swap its own local copies, so the caller's variables wouldn't change
- C) To allow `null`
- D) For performance only

<details><summary>Answer</summary>

**B.**
📖 [Samples.Generics · G1](Samples.Generic/README.md#exercises) ·
💻 [SampleGeneric.cs](Samples.Generic/SampleGeneric.cs#L9)
</details>

**Q28. 🟢** Can you write `SampleGeneric.Swap(ref a, ref b);` (with no `<int>`) when `a` and `b` are `int`?
- A) Yes, the compiler **infers** `T` from the arguments
- B) No, type arguments are always required
- C) Only if `T` has a constraint
- D) Only in C# 10 and later

<details><summary>Answer</summary>

**A.**
📖 [Samples.Generics · G1](Samples.Generic/README.md#exercises) ·
💻 [Program.cs](Samples.Generic/Program.cs#L20)
</details>

**Q29. 🟡** `GenericList<T> where T : Dancer`. What happens with `new GenericList<Programmer>()`?
```csharp
public class Dancer                              // the constraint's base type
{
    public string Name { get; set; }
    public int ID { get; set; }
}
public class Programmer { }                      // does NOT inherit from Dancer

public class GenericList<T> where T : Dancer     // T must be Dancer or a subclass of it
{
    public void AddHead(T t) { ... }
    public T FindFirstOccurrence(string s) { ... }
}

GenericList<Dancer> ok = new GenericList<Dancer>();          // used in the repo
GenericList<Programmer> list = new GenericList<Programmer>(); // ← this line
```
🔗 Source: [GenericList.cs · the constraint](Samples.Generic/GenericList.cs#L7) · [Dancer.cs](Samples.Generic/Dancer.cs#L7-L12) · [Programmer.cs](Samples.Generic/Programmer.cs#L7-L9) · [Program.cs · the commented‑out line](Samples.Generic/Program.cs#L41-L42)

- A) It works, because constraints are only hints
- B) Runtime `InvalidCastException`
- C) It works, but `FindFirstOccurrence` returns `null`
- D) Compile error **CS0311**: `Programmer` doesn't satisfy `where T : Dancer`

<details><summary>Answer</summary>

**D.**
📖 [Samples.Generics · G2](Samples.Generic/README.md#exercises) ·
💻 [GenericList.cs](Samples.Generic/GenericList.cs#L7)
</details>

**Q30. 🟡** Why does `T t = null;` compile inside `GenericList<T>`?
```csharp
public class GenericList<T> where T : Dancer     // Dancer is a class
{
    public T FindFirstOccurrence(string s)
    {
        Node current = head;
        T t = null;                              // ← this line
        while (current != null)
        {
            if (current.Data.Name == s) { t = current.Data; break; }   // .Name comes from Dancer
            current = current.Next;
        }
        return t;
    }
}

// Compare with an unconstrained method:
static T First<T>() { T t = null; return t; }   // ❌ CS0403: T could be int, and an int can't be null
```
🔗 Source: [GenericList.cs · `FindFirstOccurrence`](Samples.Generic/GenericList.cs#L36-L55) · [GenericList.cs · the constraint](Samples.Generic/GenericList.cs#L7)

- A) Because `where T : Dancer` makes `T` a reference type
- B) Every `T` can be `null`
- C) Because of `#nullable disable`
- D) It doesn't compile

<details><summary>Answer</summary>

**A.** In an unconstrained generic you'd need `default(T)`. The same constraint is also what lets the code use `current.Data.Name`.
📖 [Samples.Generics · G2](Samples.Generic/README.md#exercises) ·
💻 [GenericList.cs](Samples.Generic/GenericList.cs#L39)
</details>

**Q31. 🟡** `GenericList<T>` doesn't implement `IEnumerable<T>`, yet `foreach (var d in list)` works. Why?
- A) The compiler adds the interface automatically
- B) `foreach` is pattern‑based: a public `GetEnumerator()` (here built with `yield return`) is enough
- C) Through reflection at runtime
- D) It doesn't work

<details><summary>Answer</summary>

**B.**
📖 [Samples.Generics · G2](Samples.Generic/README.md#exercises) ·
💻 [GenericList.cs](Samples.Generic/GenericList.cs#L25)
</details>

**Q32. 🟢** You call `AddHead` with `miguel`, then `carlos`, then `kakaroto`. In what order does `foreach` print them?
```csharp
public class GenericList<T> where T : Dancer
{
    private class Node { public Node Next; public T Data; }
    private Node head;                            // first node of a singly linked list

    public void AddHead(T t)
    {
        Node n = new Node(t) { Next = head };     // the new node points to the old head...
        head = n;                                 // ...and becomes the new head
    }

    public IEnumerator<T> GetEnumerator()         // foreach walks from head following Next
    {
        Node current = head;
        while (current != null) { yield return current.Data; current = current.Next; }
    }
}

var list = new GenericList<Dancer>();
list.AddHead(new Dancer("miguel", 1));
list.AddHead(new Dancer("carlos", 2));
list.AddHead(new Dancer("kakaroto", 3));
foreach (var d in list) Console.WriteLine(d.Name);
```
🔗 Source: [GenericList.cs · `AddHead` and `GetEnumerator`](Samples.Generic/GenericList.cs#L17-L34) · [Program.cs · the three `AddHead` calls](Samples.Generic/Program.cs#L42-L45)

> 💡 Context: `Program.cs` calls `FindFirstOccurrence` instead of `foreach`. The `foreach` line is added here to test the order.

- A) miguel, carlos, kakaroto
- B) Random
- C) kakaroto, carlos, miguel
- D) carlos, kakaroto, miguel

<details><summary>Answer</summary>

**C.** `AddHead` inserts at the front of the linked list.
📖 [Samples.Generics · G2 "Try it"](Samples.Generic/README.md#exercises) ·
💻 [GenericList.cs](Samples.Generic/GenericList.cs#L19)
</details>

---

## 8. Variance (`in` / `out`)

`SalsaDancer : Dancer` and `BachataDancer : Dancer` are the model for every question in this section.

### Classes and .NET types used in this section

```csharp
public class Dancer        { public string Name; public int ID; }   // base class (reference type)
public class SalsaDancer   : Dancer { public int Level; }           // subtype 1
public class BachataDancer : Dancer { }                             // subtype 2

// .NET declarations (simplified) — look at the 'out' / 'in' keywords:
public interface IEnumerable<out T>   { IEnumerator<T> GetEnumerator(); }   // only gives T back
public interface IReadOnlyList<out T> { T this[int index] { get; } }        // only gives T back
public class     List<T>              { void Add(T item); T this[int i] { get; set; } }  // takes AND gives T
public delegate void    Action<in T>(T obj);                                  // only takes T
public delegate TResult Func<in T, out TResult>(T arg);                       // takes T, gives TResult
```

Two words used in the questions:
- **Covariance (`out`):** `Something<SalsaDancer>` can be used as `Something<Dancer>` (same direction as inheritance).
- **Contravariance (`in`):** `Something<Dancer>` can be used as `Something<SalsaDancer>` (opposite direction).

🔗 Source: [Dancer.cs](Samples.Generic/Dancer.cs#L7-L12) · [SalsaDancer.cs](Samples.Generic/SalsaDancer.cs#L7-L11) · [BachataDancer.cs](Samples.Generic/BachataDancer.cs#L7-L10) · [VarianceContracts.cs · the repo's own `in`/`out` interfaces](Samples.Generic/Variance/VarianceContracts.cs#L8-L40)

**Q33. 🟢** Which line compiles?
```csharp
var salsa = new List<SalsaDancer>();
IEnumerable<Dancer> a = salsa;   // (1) target is an interface declared with 'out T'
List<Dancer> b = salsa;          // (2) target is a class with plain 'T'

// Why it matters: if (2) compiled, this would be legal
b.Add(new BachataDancer("carlos", 2));   // a BachataDancer inside a List<SalsaDancer>?!
```
🔗 Source: [VarianceSamples.cs · ScenarioOne](Samples.Generic/Variance/VarianceSamples.cs#L19-L30)

- A) Only (1)
- B) Only (2)
- C) Both
- D) Neither

<details><summary>Answer</summary>

**A.** `IEnumerable<out T>` is covariant. `List<T>` is invariant because it reads **and** writes, so (2) is **CS0029**.
📖 [Samples.Generics · V1](Samples.Generic/README.md#variance--in-and-out-with-generic-inheritance) ·
💻 [VarianceSamples.cs](Samples.Generic/Variance/VarianceSamples.cs#L17)
</details>

**Q34. 🟢** `Action<SalsaDancer> greetSalsa = greet;` where `greet` is an `Action<Dancer>`. What is this called?
- A) Covariance
- B) Invariance
- C) Boxing
- D) Contravariance (`in`): code that handles any `Dancer` can handle a `SalsaDancer`

<details><summary>Answer</summary>

**D.** The same reasoning lets one `IComparer<Dancer>` sort a `List<SalsaDancer>`.
📖 [Samples.Generics · V2](Samples.Generic/README.md#variance--in-and-out-with-generic-inheritance) ·
💻 [VarianceSamples.cs](Samples.Generic/Variance/VarianceSamples.cs#L40)
</details>

**Q35. 🟡** What's wrong with `interface IProducer<out T> { void Accept(T item); }`?
- A) Nothing
- B) **CS1961**: an `out` type parameter can't be used as an input parameter
- C) Interfaces can't be generic
- D) It needs `where T : class`

<details><summary>Answer</summary>

**B.** `out T` may only be returned, and `in T` may only be accepted.
📖 [Samples.Generics · V3](Samples.Generic/README.md#variance--in-and-out-with-generic-inheritance) ·
💻 [VarianceContracts.cs](Samples.Generic/Variance/VarianceContracts.cs#L8)
</details>

**Q36. 🟡** `Func<Dancer, string> describe` can be assigned to which type?
- A) `Func<object, string>`
- B) `Func<Dancer, SalsaDancer>`
- C) `Func<SalsaDancer, object>`
- D) None, because delegates aren't variant

<details><summary>Answer</summary>

**C.** `Func<in T, out TResult>` takes a narrower input and gives a wider output, both in the same assignment.
📖 [Samples.Generics · V3](Samples.Generic/README.md#variance--in-and-out-with-generic-inheritance) ·
💻 [VarianceSamples.cs](Samples.Generic/Variance/VarianceSamples.cs#L61)
</details>

**Q37. 🟡** What happens here?
```csharp
Dancer[] dancers = new SalsaDancer[2];
dancers[1] = new BachataDancer("carlos", 2);
```
- A) Compile error
- B) It works, and the array now holds a `BachataDancer`
- C) Runtime **`ArrayTypeMismatchException`**: array covariance is only checked at runtime
- D) `dancers[1]` stays `null`

<details><summary>Answer</summary>

**C.** Arrays are covariant but not type‑safe. Generic interfaces catch this at compile time instead.
📖 [Samples.Generics · V4](Samples.Generic/README.md#variance--in-and-out-with-generic-inheritance) ·
💻 [VarianceSamples.cs](Samples.Generic/Variance/VarianceSamples.cs#L85)
</details>

**Q38. 🟡** Why doesn't `IEnumerable<object> boxed = new List<int>();` compile?
```csharp
IEnumerable<object> words = new List<string> { "salsa", "bachata" };   // ✅ compiles
List<int> numbers = new List<int> { 1, 2, 3 };
IEnumerable<object> boxed = numbers;                                    // ❌ but this doesn't
IEnumerable<object> boxed2 = numbers.Cast<object>();                    // ✅ and this does
```
🔗 Source: [VarianceSamples.cs · ScenarioFour, part 2](Samples.Generic/Variance/VarianceSamples.cs#L99-L104)

> 💡 Context: `string` is a **class** (reference type); `int` is a **struct** (value type). A `string` is already an object reference, but an `int` has to be *boxed* (copied into a new object on the heap) before it can be treated as `object`.

- A) Variance only works for **reference types**. `int` would need boxing, so use `.Cast<object>()`
- B) `List<int>` doesn't implement `IEnumerable<int>`
- C) `object` isn't a valid type argument
- D) It does compile

<details><summary>Answer</summary>

**A.** It's **CS0266**.
📖 [Samples.Generics · V4](Samples.Generic/README.md#variance--in-and-out-with-generic-inheritance) ·
💻 [VarianceSamples.cs](Samples.Generic/Variance/VarianceSamples.cs#L85)
</details>

**Q39. 🟡** Why can't you write `public class Box<out T> { }`?
```csharp
public interface IProducer<out T> { T Produce(); }          // ✅ interface with 'out' (from the repo)
public delegate TResult Func<in T, out TResult>(T arg);      // ✅ delegate with 'in' / 'out' (.NET)
public class Box<out T> { }                                  // ❌ class with 'out'

// The same split exists in .NET:
IReadOnlyList<Dancer> readOnly = new List<SalsaDancer>();    // ✅ IReadOnlyList<out T> is an interface
List<Dancer> list = new List<SalsaDancer>();                 // ❌ List<T> is a class
```
🔗 Source: [VarianceContracts.cs · `IProducer<out T>`](Samples.Generic/Variance/VarianceContracts.cs#L8) · [VarianceSamples.cs · ScenarioFour, part 3](Samples.Generic/Variance/VarianceSamples.cs#L106-L110)

- A) `Box` is a reserved word
- B) Only **interfaces and delegates** can declare variant type parameters (**CS1960**)
- C) Variance needs C# 11
- D) You can, if `T` is a class

<details><summary>Answer</summary>

**B.** That's why `List<T>` is invariant while its read‑only view `IReadOnlyList<out T>` is covariant.
📖 [Samples.Generics · V4](Samples.Generic/README.md#variance--in-and-out-with-generic-inheritance) ·
💻 [VarianceSamples.cs](Samples.Generic/Variance/VarianceSamples.cs#L85)
</details>

**Q40. 🔴** `NameRequiredValidator : IValidator<Dancer>` sits in a `List<IValidator<SalsaDancer>>`. Which declaration makes that possible?
- A) `interface IValidator<out T>`
- B) `interface IValidator<T> where T : Dancer`
- C) `interface IValidator<in T>`
- D) An explicit cast on each item

<details><summary>Answer</summary>

**C.** With contravariance you write a rule once for the base type and mix it with subtype‑specific rules.
📖 [Samples.Generics · V5](Samples.Generic/README.md#variance--in-and-out-with-generic-inheritance) ·
💻 [VarianceSamples.cs](Samples.Generic/Variance/VarianceSamples.cs#L117)
</details>

**Q41. 🔴** `SalsaRepository : InMemoryRepository<SalsaDancer>` implements `IReadOnlyRepository<out T>` and `IWriteRepository<in T>`. Which assignment is **invalid**?
- A) `IReadOnlyRepository<Dancer> r = salsaRepository;`
- B) `IWriteRepository<SalsaDancer> w = new AuditLogRepository();` (an `IWriteRepository<Dancer>`)
- C) `IReadOnlyRepository<SalsaDancer> r = salsaRepository;`
- D) `IWriteRepository<Dancer> w = salsaRepository;`

<details><summary>Answer</summary>

**D.** It's **CS0266**, because a salsa repository can't store a `BachataDancer`. Splitting the read (`out`) and write (`in`) sides is what makes A and B possible.
📖 [Samples.Generics · V6](Samples.Generic/README.md#variance--in-and-out-with-generic-inheritance) ·
💻 [VarianceSamples.cs](Samples.Generic/Variance/VarianceSamples.cs#L138)
</details>

---

## 9. Delegates

**Q42. 🟢** What is a delegate?
- A) A type‑safe reference to a method, so behaviour can be passed as a parameter
- B) A class that runs on another thread
- C) A kind of interface
- D) A pointer to an object

<details><summary>Answer</summary>

**A.** `Calculator.Math` doesn't know which operation it runs; the caller decides, as in the Strategy pattern.
📖 [Samples.Delegates · D1](Samples.Delegates/README.md#exercises) ·
💻 [DelegateSamples.cs](Samples.Delegates/DelegateSamples.cs#L8)
</details>

**Q43. 🟢** What does this print? (`Math(x, y, del)` prints `del(x, y)`.)
```csharp
RuleDelegate multiply = (x, y) => x * y;
RuleDelegate add = (x, y) => x + y;
new Calculator().Math(10, 10, multiply);
new Calculator().Math(10, 10, add);
```
- A) `20` then `100`
- B) `100` then `20`
- C) `100` twice
- D) Compile error

<details><summary>Answer</summary>

**B.**
📖 [Samples.Delegates · D1](Samples.Delegates/README.md#exercises) ·
💻 [Calculator.cs](Samples.Delegates/Calculator.cs#L10)
</details>

**Q44. 🟢** What's the difference between `Action<...>` and `Func<...>`?
- A) `Action` is asynchronous
- B) `Func` can't take parameters
- C) There is none
- D) `Action` returns `void`; `Func`'s **last** type argument is its return type

<details><summary>Answer</summary>

**D.**
📖 [Samples.Delegates · D2 / D3](Samples.Delegates/README.md#exercises) ·
💻 [Calculator.cs](Samples.Delegates/Calculator.cs#L16)
</details>

**Q45. 🟢** `delegate int RuleDelegate(int x, int y);` matches which built‑in delegate?
- A) `Action<int, int>`
- B) `Func<int, int, int>`
- C) `Func<int, int>`
- D) `Predicate<int>`

<details><summary>Answer</summary>

**B.**
📖 [Samples.Delegates · D3](Samples.Delegates/README.md#exercises) ·
💻 [DelegateSamples.cs](Samples.Delegates/DelegateSamples.cs#L43)
</details>

**Q46. 🔴** `TemplateParser` has two overloads, `Register<T>(string, convertToString<User>)` and `Register<T>(string, convertToString<Company>)`. What decides which overload `Register<User>("{{UserFirstName}}", user => user.FirstName)` calls?
- A) The type argument `<User>`
- B) The order the overloads are declared in
- C) The **lambda body**: only `User` has `FirstName`, so the lambda only converts to `convertToString<User>`. `T` is never used
- D) The runtime type of the object

<details><summary>Answer</summary>

**C.**
📖 [Samples.Delegates · C1](Samples.Delegates/README.md#exercises) ·
💻 [ComplexDelegate.cs](Samples.Delegates/ComplexDelegate.cs#L31)
</details>

**Q47. 🔴** With the same two overloads, what happens with `Register<User>("x", x => x.ToString())`?
- A) It calls the `User` overload
- B) It calls the `Company` overload
- C) Runtime exception
- D) **CS0121**: an ambiguous call, because the lambda is valid for both delegate types

<details><summary>Answer</summary>

**D.**
📖 [Samples.Delegates · C1](Samples.Delegates/README.md#exercises) ·
💻 [ComplexDelegate.cs](Samples.Delegates/ComplexDelegate.cs#L36)
</details>

**Q48. 🔴** What's the main design problem with `TemplateParser`, and what's a good fix?
```csharp
public class TemplateParser
{
    public delegate string convertToString<O>(O item);

    // One dictionary PER supported type
    Dictionary<string, convertToString<User>>    _userDictionary    = new();
    Dictionary<string, convertToString<Company>> _companyDictionary = new();

    // One Register overload PER supported type (T is never used)
    public void Register<T>(string token, convertToString<User> f)    => _userDictionary.Add(token, f);
    public void Register<T>(string token, convertToString<Company> f) => _companyDictionary.Add(token, f);

    public string Parse(string template, object[] objects)
    {
        foreach (var obj in objects)
        {
            foreach (var r in _userDictionary)    if (obj.GetType() == typeof(User))    { /* replace */ }
            foreach (var r in _companyDictionary) if (obj.GetType() == typeof(Company)) { /* replace */ }
        }
        ...
    }
}

// Exercise: what must change in this class to support a new 'Product' type?
```
🔗 Source: [ComplexDelegate.cs · `TemplateParser`](Samples.Delegates/ComplexDelegate.cs#L18-L71) · [ComplexDelegate.cs · how it's used](Samples.Delegates/ComplexDelegate.cs#L79-L89)

> 💡 Context: the **Open/Closed principle** (the "O" in SOLID) says a class should be *open for extension* (support new cases) but *closed for modification* (without editing its existing code).

- A) It uses delegates; replace them with `switch` statements
- B) It needs one dictionary and one overload **per type**, which breaks Open/Closed. Fix it with a single `Dictionary<Type, List<(string Token, Func<object, string?>)>>` filled by `Register<T>(string, Func<T, string?>)`
- C) It's too slow; use `StringBuilder` and nothing else
- D) Nothing is wrong

<details><summary>Answer</summary>

**B.** The README includes the full refactored version.
📖 [Samples.Delegates · C1](Samples.Delegates/README.md#exercises) ·
💻 [ComplexDelegate.cs](Samples.Delegates/ComplexDelegate.cs#L23)
</details>

---

## Answer key

| Q | A | Q | A | Q | A | Q | A | Q | A | Q | A |
|---|---|---|---|---|---|---|---|---|---|---|---|
| 1 | B | 9 | A | 17 | C | 25 | B | 33 | A | 41 | D |
| 2 | A | 10 | B | 18 | A | 26 | B | 34 | D | 42 | A |
| 3 | C | 11 | C | 19 | B | 27 | B | 35 | B | 43 | B |
| 4 | D | 12 | D | 20 | D | 28 | A | 36 | C | 44 | D |
| 5 | B | 13 | A | 21 | B | 29 | D | 37 | C | 45 | B |
| 6 | C | 14 | C | 22 | C | 30 | A | 38 | A | 46 | C |
| 7 | B | 15 | B | 23 | B | 31 | B | 39 | B | 47 | D |
| 8 | D | 16 | B | 24 | C | 32 | C | 40 | C | 48 | B |

**Score:** 40–48 interview‑ready · 30–39 review the 🔴 exercises · below 30 go through the
[learning path](README.md#suggested-learning-path).
