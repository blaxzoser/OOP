# Samples.Delegates — Delegates, `Action`, `Func` and an interview problem

| | |
|---|---|
| **Type** | Console app (`net6.0`) |
| **Path** | [`Samples.OOP/Samples.OOP/Samples.Delegates`](.) |
| **Entry point** | [`Program.cs`](Program.cs) — currently runs `ComplexDelegate.PrintResult()` |
| **Exercises** | 3 basic scenarios + 1 interview scenario |
| **Overall level** | Beginner → Advanced |

## How to run

Change the call in `Main` (`DelegateSamples.ScenarioOne()`, `ScenarioTwo()`, `ScenarioThree()` or
`ComplexDelegate.PrintResult()`), then from this folder:

```bash
dotnet run
```

## Topics covered

| Topic | Where |
|---|---|
| Declaring a custom delegate type | `public delegate int RuleDelegate(int x, int y);` in [`DelegateSamples.cs`](DelegateSamples.cs:9) |
| Lambdas assigned to delegates | all scenarios |
| Built‑in `Action<...>` (no return value) | `ScenarioTwo` |
| Built‑in `Func<..., TResult>` (returns a value) | `ScenarioThree` |
| Passing behaviour as a parameter (Strategy pattern) | [`Calculator.Math`](Calculator.cs) overloads |
| Method overloading | three `Math` overloads in `Calculator` |
| **Generic** delegate type | `convertToString<O>` in [`ComplexDelegate.cs`](ComplexDelegate.cs:20) |
| Storing delegates in a `Dictionary` (registry / lookup table) | `TemplateParser` |
| Runtime type checks with `GetType()` / `typeof` | `TemplateParser.Parse` |
| Overload resolution driven by lambda bodies | `TemplateParser.Register<T>` (see C1) |

## Files

| File | Role |
|---|---|
| [`DelegateSamples.cs`](DelegateSamples.cs) | `RuleDelegate` + scenarios 1–3 |
| [`Calculator.cs`](Calculator.cs) | Receives the behaviour (delegate / `Action` / `Func`) and executes it |
| [`ComplexDelegate.cs`](ComplexDelegate.cs) | Interview scenario: `User`, `Company`, `TemplateParser` (namespace `Solution`) |
| [`Program.cs`](Program.cs) | Entry point |

---

## Exercises

| # | Exercise | Level | Question it answers |
|---|---|---|---|
| D1 | `DelegateSamples.ScenarioOne` | 🟢 Beginner | What is a delegate and how do I pass behaviour into a method? |
| D2 | `DelegateSamples.ScenarioTwo` | 🟢 Beginner | When do I use `Action<T>`? |
| D3 | `DelegateSamples.ScenarioThree` | 🟢 Beginner | When do I use `Func<T, TResult>`? |
| C1 | `ComplexDelegate.PrintResult` | 🔴 Advanced | *Interview:* build a template engine where each type registers how to render its own placeholders. |

### D1 · `ScenarioOne` — custom delegate 🟢 — [line 16](DelegateSamples.cs:16)
**Question:** What is a delegate, and how does it make a method's behaviour pluggable?
**What it does:** Two lambdas (`x + y`, `x * y`) are stored in `RuleDelegate` variables and passed to the
same `Calculator.Math(10, 10, del)`.
```
100
20
```
**Takeaway:** a delegate is a **type‑safe reference to a method**. `Calculator` doesn't know *what*
operation it runs — the caller decides (Strategy pattern).

### D2 · `ScenarioTwo` — `Action<T1,T2,T3>` 🟢 — [line 29](DelegateSamples.cs:29)
**Question:** How do I pass behaviour that returns nothing, without declaring my own delegate type?
```
30
Do it
1000
Do it
```
**Takeaway:** `Action<...>` = built‑in delegate returning `void` (up to 16 parameters). The lambda prints
the result itself; `Calculator` then prints `Do it`.

### D3 · `ScenarioThree` — `Func<T1..T4,TResult>` 🟢 — [line 43](DelegateSamples.cs:43)
**Question:** How do I pass behaviour that returns a value?
```
40
10000
```
**Takeaway:** `Func<...>` = built‑in delegate whose **last** type argument is the return type.
`RuleDelegate` from D1 is equivalent to `Func<int, int, int>`.

---

### C1 · `ComplexDelegate.PrintResult` — template parser (interview question) 🔴 — [line 77](ComplexDelegate.cs:77)

**The problem:** given objects of different types (`User`, `Company`) and a template like
`"Dear {{UserFirstName}} from  {{CompanyName}}"`, let callers *register* how each placeholder is
produced from each type, then render the template from a list of objects.

**Usage in the code:**
```csharp
templateParser.Register<User>("{{UserFirstName}}", (user) => user.FirstName);
templateParser.Register<Company>("{{CompanyName}}", (comp) => comp.Name);
templateParser.Parse("Dear {{UserFirstName}} from  {{CompanyName}}", objects);
```
**Output:**
```
Dear John from  ABC Company
```

**Questions it answers:**
- How do I store "how to turn a `T` into a string" for later use? → a **generic delegate**
  `convertToString<O>` kept in a `Dictionary<string, delegate>`.
- How do I pick the right delegate for an `object` at runtime? → compare `shape.GetType()` with `typeof(...)`
  and cast.

**Things an interviewer will ask about this solution** (the code marks them `// TODO: It should be Refactor`):
1. **One dictionary and one `Register` overload per type.** Adding `Order` means editing the class in
   three places — it breaks the Open/Closed principle.
2. **`T` in `Register<T>` is never used.** The right overload is actually chosen by the *lambda body*:
   `user => user.FirstName` only compiles against `convertToString<User>` (Company has no `FirstName`),
   and `comp => comp.Name` only against `convertToString<Company>`. A lambda valid for both types
   (e.g. `x => x.ToString()`) would give an ambiguity error (**CS0121**), whatever you put in `<T>`.
3. **The type check is inside the inner loop**, so it is re‑evaluated for every registration — and the
   whole dictionary is iterated even for objects of the other type.
4. `Dictionary.Add` throws if the same placeholder is registered twice.
5. `string?` annotations are used without `<Nullable>enable</Nullable>` in the `.csproj`, producing
   warning **CS8632** at build time.

**One possible refactor** — a single registry keyed by `Type`, where `T` really matters:

```csharp
public class TemplateParser
{
    private readonly Dictionary<Type, List<(string Token, Func<object, string?> Resolve)>> _resolvers = new();

    public void Register<T>(string token, Func<T, string?> resolve)
    {
        if (!_resolvers.TryGetValue(typeof(T), out var list))
            _resolvers[typeof(T)] = list = new();
        list.Add((token, o => resolve((T)o)));
    }

    public string Parse(string template, IEnumerable<object> objects)
    {
        var result = template;
        foreach (var obj in objects)
        {
            if (!_resolvers.TryGetValue(obj.GetType(), out var list)) continue;
            foreach (var (token, resolve) in list)
                result = result.Replace(token, resolve(obj) ?? string.Empty);
        }
        return result;
    }
}
```
Callers don't change (`Register<User>(..., u => u.FirstName)`), new types need **no** parser changes,
and each object only looks at its own registrations.

**Follow‑up ideas to practise:** support base types/interfaces (walk `GetType().BaseType`), use
`StringBuilder` for long templates, or report placeholders that were never replaced.

---

## Notes
- `using static Samples.Delegates.Program;` in `Calculator.cs` and `DelegateSamples.cs` is unused.
- `PrintResult()` and `Main` both call `Console.ReadLine()`, so you press Enter twice to exit.
