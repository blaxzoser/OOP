# C# Interview Cheat Code — OOP, Generics & Delegates

The rules you need to answer every question in the **[fake technical interview](fake-test-interview.md)**,
with short snippets and diagrams. Each section names the questions it solves (🎯) and links to the code in
this repo (💻).

> Read this first, then take the quiz. If you miss a question, come back to the section marked 🎯 for it.

---

## ⚡ The 12 rules to remember

| # | Rule | 🎯 |
|---|---|---|
| 1 | `readonly` → set only in a field initializer or constructor. `init` → set only while the object is being created. `private set` → only the class can set it, at any time. | Q1–Q3 |
| 2 | Constructor order: **static ctor (once) → base ctor → derived ctor body**. | Q5, Q21 |
| 3 | With no `: base(...)`, the **parameterless** base constructor runs. | Q6, Q20 |
| 4 | `static` data has **one copy per class**, shared by every instance. | Q8 |
| 5 | **`override` → the runtime type wins. `new` → the declared type wins.** | Q9–Q11 |
| 6 | No modifier on a method that matches a base `virtual` method → warning **CS0114**, treated as **`new`**. | Q12, Q25 |
| 7 | A virtual call **inside** a base method still dispatches to the override. | Q13, Q14 |
| 8 | `abstract` members **must** be implemented; `virtual` members **may** be overridden. | Q18 |
| 9 | One base class (listed **first**), then any number of interfaces. | Q24 |
| 10 | Constraints (`where T : Dancer`) let generic code use the base type's members, and `T t = null` (because `T` is a class). | Q29–Q30 |
| 11 | **`out T` = producer** (`I<Child>` → `I<Base>`). **`in T` = consumer** (`I<Base>` → `I<Child>`). Interfaces and delegates only, and reference types only. | Q33–Q41 |
| 12 | `Action` returns `void`; `Func`'s **last** type argument is the return type. | Q44–Q45 |

---

## 1. Encapsulation 🎯 Q1–Q4

```csharp
public class Player
{
    public Guid ID { get; private set; }   // anyone reads · only Player writes, any time
    public string Name { get; init; }      // set only while the object is created (C# 9)
    public readonly int Age;               // set only in a field initializer or constructor

    public Player()        { ID = Guid.NewGuid(); }
    public Player(int age) { Age = age; }        // ⚠ ID is never set here → Guid.Empty
}
```

| | Who can set it | When |
|---|---|---|
| `readonly` field | the class | field initializer / constructor only |
| `{ get; init; }` | anyone | constructor or object initializer `new Player { Name = "Beto" }` only |
| `{ get; private set; }` | the class | any time |
| `{ get; }` (get‑only) | the class | constructor only |

🪤 **Traps**
- `p.Name = "x";` after creation → compile error (`init`).
- `new Player(23).ID` → `00000000-0000-0000-0000-000000000000`: only the parameterless constructor assigns it.

💻 [Player.cs](Samples.OOP/Encapsulation/Player.cs#L3-L18) · [PlayerConsole.cs](Samples.Test/Encapsulation/PlayerConsole.cs#L10-L40)

---

## 2. Constructors & static members 🎯 Q5–Q8, Q20–Q21

**Order when an object is created** (here the first time `Teacher` is used in the program):

```
new Student("t", "d", span, 1)
   │
   ├─ ① static Teacher()     ← once per program, before first use
   ├─ ② Teacher()            ← base constructor (implicit: no ": base(...)")
   └─ ③ Student(...) body    ← derived constructor runs LAST
```

Three levels work the same way, from the top of the hierarchy down:

```
new SuperAfroTeacher()
   ① AbstractTeacher()   "BASE-Create Abstract class"
   ② AfroTeacher()       "Create AfroTeacher"
   ③ SuperAfroTeacher()  "Created SuperAfroTeacher"
```

**Choosing the base constructor:**
```csharp
public ReguetonTeacher(string name) : base(name) { }   // picks AbstractTeacher(string name)
public BalletTeacher() { }                              // implicit : base() → AbstractTeacher()
```

**Static data is shared:**
```
 teacher1 ──┐
            ├──►  Teacher.money   (ONE copy)  = 0.002
 teacher2 ──┘
```
```csharp
teacher1.SetMoney(0.001);
teacher1.SetMoney(0.002);
teacher1.GetMoney();   // 0.002
teacher2.GetMoney();   // 0.002 ← same field
```

**Static constructor rules:** one per class · no access modifier · no parameters · runs exactly once ·
runs before the first instance or static member access.

💻 [Teacher.cs · static ctor](Samples.OOP/Teacher.cs#L57-L61) · [Student.cs · ctor](Samples.OOP/Student.cs#L16-L31) · [ReguetonTeacher.cs](Samples.OOP/ReguetonTeacher.cs#L11-L15)

---

## 3. `override` vs `new` 🎯 Q9–Q15, Q22, Q25

### The one rule
> **`override` → runtime type wins.  `new` (or no modifier) → declared type wins.**

```csharp
Teacher t = new EnglishTeacher();
//  ▲               ▲
//  declared type   runtime type
```

### How C# picks the method (do this for every "what prints?" question)
```
obj.Method()
  │
  ① Look at the DECLARED type of obj and find Method there.
  │
  ② Is that method virtual / abstract / override?
  │     ├── NO  → run it. Done.                    (new / no-modifier hide stops here)
  │     └── YES → ③
  │
  ③ Go to the RUNTIME type and run the most-derived OVERRIDE of it.
       A `new` method in a subclass is NOT an override, so it is skipped.
```

### The repo's classes
```
                              Teacher
               virtual Dance() · ShowMeDance() (not virtual, calls Dance())
     ┌───────────────┬───────────┼───────────────┬─────────────────┐
EnglishTeacher  BachataTeacher  StupidTeacher  NewStyleTeacher     Student
override        override        override       new Dance()         Dance() no modifier
                                → base.Dance() new ShowMeDance()   (= new, CS0114)
```

| Call | Through child variable | Through `Teacher` variable / cast |
|---|---|---|
| `EnglishTeacher.Dance()` (override) | child | **child** |
| `BachataTeacher.Dance()` (override) | `Dance - BACHATA` | **`Dance - BACHATA`** |
| `StupidTeacher.Dance()` (override → `base.Dance()`) | `Dance()-BASE` | `Dance()-BASE` |
| `NewStyleTeacher.Dance()` (new) | `New Style-DANCE` | **`Dance()-BASE`** |
| `Student.Dance()` (no modifier) | `Move Dance Student by Class` | **`Dance()-BASE`** |

### The hardest trap: a base method that calls a virtual method
```csharp
class Teacher
{
    public virtual void Dance() => Print("Dance()-BASE");
    public void ShowMeDance() { Print("Show me Dance-BASE"); Dance(); }   // NOT virtual
}
```
| Call | Output | Why |
|---|---|---|
| `new BachataTeacher().ShowMeDance()` | `Show me Dance-BASE` → `Dance - BACHATA` | base method, but `Dance()` is virtual → override |
| `Teacher t = new NewStyleTeacher(); t.ShowMeDance()` | `Show me Dance-BASE` → `Dance()-BASE` | base method; `new Dance()` isn't an override |
| `new NewStyleTeacher().ShowMeDance()` | `New Show me dance foreverrrrrrrrrr` | declared type is `NewStyleTeacher`, so its `new ShowMeDance()` runs |
| `new StupidTeacher().ShowMeDance()` | `Show me Dance-BASE` → `Dance()-BASE` | override calls `base.Dance()` |

🪤 **Traps**
- No modifier ≠ override. It compiles with **CS0114** and behaves like `new`.
- `base.Method()` inside an override calls the parent version. That's how you *extend* behaviour, and it doesn't recurse.
- Shadowing with `new` is for when you **can't** make the base method `virtual` (third‑party code). If you own the base, use `virtual`/`override`.

💻 [Teacher.cs](Samples.OOP/Teacher.cs#L89-L98) · [NewStyleTeacher.cs](Samples.OOP/NewStyleTeacher.cs#L9-L17) · [Student.cs](Samples.OOP/Student.cs#L40-L43) · [PolymorphismSamples.cs · ScenarioNine](Samples.Test/PolymorphismSamples.cs#L121-L130)

---

## 4. Polymorphism 🎯 Q16–Q17

```csharp
var teachers = new List<Teacher> { new SalsaTeacher(), new HipHopTeacher(), new BachataTeacher() };
foreach (var t in teachers) t.Dance();   // Dance - SALSA · Dance - HIPHOP · Dance - BACHATA
```
```
List<Teacher>  ──holds──►  SalsaTeacher  ─┐
                           HipHopTeacher ─┼─ each "is a" Teacher (implicit upcast, no cast needed)
                           BachataTeacher ┘   and runs its OWN override at runtime
```
**Remember:** upcasting (child → base) is implicit and always safe. Downcasting (base → child) needs a cast and can fail.

💻 [PolymorphismSamples.cs · ScenarioFour](Samples.Test/PolymorphismSamples.cs#L10-L38)

---

## 5. Abstract classes 🎯 Q18–Q23

```csharp
public abstract class AbstractTeacher
{
    public AbstractTeacher() { }                       // abstract classes CAN have constructors
    public AbstractTeacher(string name) { Name = name; }
    public virtual void Dance() { }                    // optional to override
    public abstract void SetMoney();                   // MUST be implemented
    public abstract string Schedule { get; }           // MUST be implemented
}
```

| | `abstract` member | `virtual` member | normal member |
|---|---|---|---|
| Has a body in the base? | ✘ | ✔ | ✔ |
| Child must override? | **✔** | optional | can only hide with `new` |

| Abstract class | Interface |
|---|---|
| A class can inherit **one** | A class can implement **many** |
| Can have constructors, fields, state | No instance fields or constructors |
| `new AbstractTeacher()` → **CS0144** | can't be instantiated either |
| Use it for a shared base with common code | Use it for a capability contract (`IMoveShoulder`) |

🪤 **Trap (shadowing):**
```csharp
class SuperAfroTeacher : AfroTeacher { public new void Dance() => Print("Dance SuperAfroTeacher"); }

var super = new SuperAfroTeacher();
super.Dance();                  // Dance SuperAfroTeacher
((AfroTeacher)super).Dance();   // Dance AFRO   ← cast to parent = parent's method
```

💻 [AbstractTeacher.cs](Samples.OOP/AbstractTeacher.cs#L7-L36) · [SuperAfroTeacher.cs](Samples.OOP/SuperAfroTeacher.cs#L14-L17) · [AbstractSamples.cs](Samples.Test/AbstractSamples.cs#L70-L96)

---

## 6. Interfaces 🎯 Q24–Q26

```csharp
class Student : Teacher, IMoveShoulder, IAction   // ① base class FIRST ② then interfaces
{
    public void MoveOn() { }   // implicit implementation: public method, same signature
    public void Dance()  { }   // implements IAction.Dance() AND hides Teacher.Dance() (CS0114)
}
```
```
                Teacher.Dance()  (virtual)
                     ▲ hidden, not overridden
 Student.Dance() ────┤
                     ▼ implements
                IAction.Dance()
```
| Call | Output |
|---|---|
| `student.Dance()` | `Move Dance Student by Class` |
| `((IAction)student).Dance()` *(possible inside the library; `IAction` is internal)* | `Move Dance Student by Class` |
| `((Teacher)student).Dance()` | **`Dance()-BASE`** |

🪤 There's no `implements` keyword and no `override` for interface members in C#.

💻 [Student.cs](Samples.OOP/Student.cs#L7-L44) · [InterfaceSamples.cs](Samples.Test/InterfaceSamples.cs#L4-L22)

---

## 7. Generics 🎯 Q27–Q32

```csharp
public static void Swap<T>(ref T a, ref T b) { T tmp = a; a = b; b = tmp; }

Swap<int>(ref a, ref b);   // explicit type argument
Swap(ref a, ref b);        // T inferred from the arguments
```
- `ref` is needed, otherwise the method swaps its own **copies**.

**Constraints:**
| Constraint | Means | Lets you |
|---|---|---|
| `where T : Dancer` | `T` is `Dancer` or a subclass | use `.Name` and `.ID`, and write `T t = null` |
| `where T : class` | reference type | `T t = null` |
| `where T : struct` | value type | — |
| `where T : new()` | has a parameterless ctor | `new T()` |
| `where T : ISomething` | implements the interface | call interface members |

```csharp
new GenericList<Dancer>();       // ✔
new GenericList<Programmer>();   // ✘ CS0311: Programmer is not a Dancer
```

**`foreach` without `IEnumerable<T>`:** `foreach` only needs a public `GetEnumerator()` (the pattern), and `yield return` builds it for you.

**`AddHead` builds the list backwards:**
```
AddHead(miguel)     head → [miguel]
AddHead(carlos)     head → [carlos] → [miguel]
AddHead(kakaroto)   head → [kakaroto] → [carlos] → [miguel]
foreach             kakaroto, carlos, miguel
```

💻 [SampleGeneric.cs](Samples.Generic/SampleGeneric.cs#L9-L15) · [GenericList.cs](Samples.Generic/GenericList.cs#L7-L56)

---

## 8. Variance — `in` / `out` 🎯 Q33–Q41

```
                 SalsaDancer ──is a──► Dancer

 out (covariant):     I<SalsaDancer> ──────► I<Dancer>        same direction   (producer: only RETURNS T)
 in  (contravariant): I<Dancer>      ──────► I<SalsaDancer>   reversed         (consumer: only ACCEPTS T)
 invariant:           List<SalsaDancer>  ✘  List<Dancer>      neither way      (reads AND writes)
```

**Mnemonic: things come *out* of producers, things go *in* to consumers.**

```csharp
IEnumerable<Dancer> a = new List<SalsaDancer>();          // ✔ out
List<Dancer>        b = new List<SalsaDancer>();          // ✘ CS0029: List<T> is invariant
Action<SalsaDancer> c = (Action<Dancer>)(d => { });       // ✔ in
Func<SalsaDancer, object> f = (Func<Dancer, string>)(d => d.Name);   // ✔ in on input, out on output
```

**Declaring your own:**
```csharp
interface IProducer<out T> { T Produce(); }          // ✔   void Accept(T x) → CS1961
interface IConsumer<in T>  { void Consume(T item); } // ✔   T Last()         → CS1961
```

| Built‑in | Variance |
|---|---|
| `IEnumerable<out T>`, `IReadOnlyList<out T>`, `IReadOnlyCollection<out T>` | out |
| `Action<in T>`, `IComparer<in T>`, `IEqualityComparer<in T>`, `Predicate<in T>` | in |
| `Func<in T, out TResult>` | both |
| `List<T>`, `Dictionary<TKey, TValue>`, arrays of value types | invariant |

🪤 **Traps**
| Code | Result |
|---|---|
| `Dancer[] d = new SalsaDancer[2]; d[1] = new BachataDancer(...)` | compiles, then **`ArrayTypeMismatchException`** at runtime |
| `IEnumerable<object> o = new List<int>();` | **CS0266**: value types aren't variant, so use `.Cast<object>()` |
| `class Box<out T> { }` | **CS1960**: only interfaces and delegates can be variant |

**Real world (V5/V6):**
```
IValidator<in T>:   NameRequiredValidator : IValidator<Dancer>  ──fits──►  List<IValidator<SalsaDancer>>

Repository:         IReadOnlyRepository<out T>   IWriteRepository<in T>
                                   ▲                   ▲
                                   └─ InMemoryRepository<T> where T : Dancer
                                                 ▲
                                          SalsaRepository : InMemoryRepository<SalsaDancer>

 SalsaRepository   → IReadOnlyRepository<Dancer>    ✔ (out: reports over many repositories)
 AuditLogRepository (IWriteRepository<Dancer>) → IWriteRepository<SalsaDancer>  ✔ (in)
 SalsaRepository   → IWriteRepository<Dancer>        ✘ CS0266 (it can't store a BachataDancer)
```

💻 [VarianceContracts.cs](Samples.Generic/Variance/VarianceContracts.cs#L8-L40) · [VarianceSamples.cs](Samples.Generic/Variance/VarianceSamples.cs#L17-L161)

---

## 9. Delegates 🎯 Q42–Q48

A **delegate** is a type‑safe reference to a method. It lets the caller decide the behaviour (Strategy pattern).

```
caller ──(x, y) => x * y──►  Calculator.Math(10, 10, del)  ──► del(10, 10) ──► 100
caller ──(x, y) => x + y──►  Calculator.Math(10, 10, del)  ──► del(10, 10) ──► 20
```

| Type | Returns | Example |
|---|---|---|
| `delegate int RuleDelegate(int x, int y)` | `int` | custom |
| `Action<int, int, int>` | `void` | `(x, y, z) => Console.WriteLine(x + y + z)` |
| `Func<int, int, int, int, int>` | **last** type argument → `int` | `(x, y, z, w) => x * y * z * w` |
| `RuleDelegate` ≡ `Func<int, int, int>` | | |

🪤 **TemplateParser traps (interview problem C1):**
```csharp
void Register<T>(string key, convertToString<User> f)     // T is NEVER used
void Register<T>(string key, convertToString<Company> f)

Register<User>("{{UserFirstName}}", u => u.FirstName);  // picks the User overload: only User has FirstName
Register<User>("x", x => x.ToString());                  // ✘ CS0121 ambiguous: valid for both
```
**Fix (Open/Closed):** use one registry keyed by `Type`, where `T` really matters:
```csharp
private readonly Dictionary<Type, List<(string Token, Func<object, string?> Resolve)>> _resolvers = new();

public void Register<T>(string token, Func<T, string?> resolve)
{
    if (!_resolvers.TryGetValue(typeof(T), out var list))
        _resolvers[typeof(T)] = list = new();
    list.Add((token, o => resolve((T)o)));
}
```

💻 [DelegateSamples.cs](Samples.Delegates/DelegateSamples.cs#L8-L53) · [Calculator.cs](Samples.Delegates/Calculator.cs#L10-L26) · [ComplexDelegate.cs](Samples.Delegates/ComplexDelegate.cs#L18-L73)

---

## 🧯 Compiler errors to recognise on sight

| Code | Meaning | Seen in |
|---|---|---|
| **CS0114** *(warning)* | Method hides an inherited virtual member; add `override` or `new` | `Student.Dance()` · Q12 |
| **CS0144** | Can't create an instance of an abstract type or interface | `new AbstractTeacher()` · Q19 |
| **CS0311** | Type argument doesn't satisfy the constraint | `GenericList<Programmer>` · Q29 |
| **CS0029** | Can't implicitly convert (no conversion exists) | `List<Dancer> = List<SalsaDancer>` · Q33 |
| **CS0266** | Can't implicitly convert (an explicit cast exists) | wrong variance direction, `List<int>` → `IEnumerable<object>` · Q38, Q41 |
| **CS1961** | Variance used in the wrong position (`out T` as a parameter) | `IProducer<out T>.Accept(T)` · Q35 |
| **CS1960** | Variance on a class | `class Box<out T>` · Q39 |
| **CS0121** | Ambiguous call between overloads | `Register<User>("x", x => x.ToString())` · Q47 |

---

## ✅ Last‑minute checklist

- [ ] I can say **"override → runtime type, new → declared type"** and apply the 3‑step lookup.
- [ ] I know constructors run **static → base → derived**, and that `: base(args)` picks the base constructor.
- [ ] I can explain `readonly` vs `init` vs `private set` in one sentence each.
- [ ] I know what an abstract class **must** contain vs an interface.
- [ ] I can draw the **`out` / `in` arrows** and name one built‑in example of each.
- [ ] I know why **arrays** are covariant but unsafe, and why `List<T>` is invariant.
- [ ] I can explain `Action` vs `Func`, and why the TemplateParser breaks Open/Closed.

**Ready?** → Take the **[fake technical interview](fake-test-interview.md)**.
