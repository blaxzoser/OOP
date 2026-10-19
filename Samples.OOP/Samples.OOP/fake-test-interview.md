# Fake Technical Interview — C# OOP, Generics & Delegates

A multiple‑choice mock interview built **only from what this repository demonstrates**. Every answer links
to the README section that explains it (📖) and to the code that proves it (💻).

**How to use it:** answer each question before opening **Answer**. Count your score with the
[answer key](#answer-key) at the end.

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

**Q9. 🟢** `EnglishTeacher` **overrides** `Dance()`. What runs for `Teacher t = new EnglishTeacher(); t.Dance();`?
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
- A) `New Style-DANCE`
- B) `Dance()-BASE`
- C) Both lines
- D) Runtime `InvalidCastException`

<details><summary>Answer</summary>

**B.** `new` opts out of virtual dispatch, so through a `Teacher` reference the base method runs.
📖 [Samples.Test · P6 / P9](Samples.Test/README.md#3-polymorphism--polymorphismsamplescs) ·
💻 [NewStyleTeacher.cs](Samples.OOP/NewStyleTeacher.cs#L9)
</details>

**Q11. 🟡** Which one‑liner correctly summarises `override` vs `new`?
- A) `override` → declared type wins; `new` → runtime type wins
- B) Both always run the most‑derived method
- C) `override` → runtime type wins; `new` → declared (compile‑time) type wins
- D) `new` only works on abstract methods

<details><summary>Answer</summary>

**C.** This is the key takeaway of exercise P9.
📖 [Samples.Test · cheat sheet](Samples.Test/README.md#cheat-sheet) ·
💻 [PolymorphismSamples.cs](Samples.Test/PolymorphismSamples.cs#L121)
</details>

**Q12. 🔴** `Teacher.Dance()` is `virtual`. `Student` declares `public void Dance()` with **no** modifier. What happens?
- A) Compile error
- B) It silently becomes an `override`
- C) The base method is called and `Student.Dance()` is ignored
- D) It compiles with warning **CS0114** and *hides* `Teacher.Dance()`, like `new`

<details><summary>Answer</summary>

**D.** With no modifier the method hides the base one. The compiler warns and treats it as `new`.
📖 [Samples.Test · F1](Samples.Test/README.md#5-interfaces--interfacesamplescs) ·
💻 [Student.cs](Samples.OOP/Student.cs#L40)
</details>

**Q13. 🔴** `Teacher.ShowMeDance()` is **not** virtual and calls the virtual `Dance()`. `BachataTeacher` overrides `Dance()`. What does `new BachataTeacher().ShowMeDance()` print?
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
var teachers = new List<Teacher> { new SalsaTeacher(), new HipHopTeacher(), new BachataTeacher() };
foreach (var t in teachers) t.Dance();
```
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

**Q33. 🟢** Which line compiles?
```csharp
var salsa = new List<SalsaDancer>();
IEnumerable<Dancer> a = salsa;   // (1)
List<Dancer> b = salsa;          // (2)
```
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
