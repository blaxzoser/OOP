# Graph Report - OOP  (2026-10-21)

## Corpus Check
- Corpus is ~19,715 words - fits in a single context window. You may not need a graph.

## Summary
- 333 nodes · 694 edges · 12 communities
- Extraction: 82% EXTRACTED · 18% INFERRED · 0% AMBIGUOUS · INFERRED: 124 edges (avg confidence: 0.85)
- Token cost: 240,788 input · 0 output

## Community Hubs (Navigation)
- Repo Docs & Delegate Notes
- Interview Cheat Sheet
- Generic Variance Interfaces
- Projects & Source Files
- Teacher Dance Hierarchy
- Abstract Teacher Classes
- Interfaces & Student
- TemplateParser Delegates
- GenericList Linked List
- Dispatch & Inheritance Rules
- Calculator Delegates
- Player Encapsulation

## God Nodes (most connected - your core abstractions)
1. `Teacher` - 23 edges
2. `Samples.OOP` - 16 edges
3. `C# Interview Cheat Code` - 16 edges
4. `SalsaDancer` - 15 edges
5. `Fake Technical Interview (C# OOP, Generics & Delegates)` - 15 edges
6. `Dancer` - 14 edges
7. `Student` - 12 edges
8. `The 12 Rules to Remember` - 12 edges
9. `Samples.Generics project` - 11 edges
10. `Samples.Test OOP exercises console` - 11 edges

## Surprising Connections (you probably didn't know these)
- `Dispatch cheat sheet (child vs base reference)` --semantically_similar_to--> `override: runtime type wins; new: declared type wins`  [INFERRED] [semantically similar]
  Samples.OOP/Samples.OOP/Samples.Test/README.md → README.md
- `OOP Repository (C# OOP & interview samples)` --references--> `Fake Technical Interview (C# OOP, Generics & Delegates)`  [EXTRACTED]
  README.md → Samples.OOP/Samples.OOP/fake-test-interview.md
- `OOP Repository (C# OOP & interview samples)` --references--> `C# Interview Cheat Code`  [EXTRACTED]
  README.md → Samples.OOP/Samples.OOP/cheatcode.md
- `Four OOP Pillars (encapsulation, inheritance, polymorphism, abstraction)` --conceptually_related_to--> `Samples.Test OOP exercises console`  [INFERRED]
  README.md → Samples.OOP/Samples.OOP/Samples.Test/README.md
- `Encapsulation exercises E1-E3 (PlayerConsole)` --conceptually_related_to--> `Four OOP Pillars (encapsulation, inheritance, polymorphism, abstraction)`  [INFERRED]
  Samples.OOP/Samples.OOP/Samples.Test/README.md → README.md

## Import Cycles
- None detected.

## Hyperedges (group relationships)
- **C# Method Dispatch Rules (override / new / hiding)** — samples_oop_samples_oop_cheatcode_override_vs_new_rule, samples_oop_samples_oop_cheatcode_method_lookup_algorithm, samples_oop_samples_oop_cheatcode_cs0114_implicit_hiding, samples_oop_samples_oop_cheatcode_virtual_call_inside_base_method, samples_oop_samples_oop_cheatcode_method_shadowing_new [INFERRED 0.85]
- **Generic Variance Model (in / out / invariant)** — samples_oop_samples_oop_cheatcode_covariance_out, samples_oop_samples_oop_cheatcode_contravariance_in, samples_oop_samples_oop_cheatcode_invariance, samples_oop_samples_oop_cheatcode_array_covariance_unsafe, samples_oop_samples_oop_cheatcode_variance_restrictions [INFERRED 0.85]
- **TemplateParser Interview Problem (C1)** — samples_oop_samples_oop_fake_test_interview_delegates_questions, samples_oop_samples_oop_cheatcode_templateparser_overload_ambiguity, samples_oop_samples_oop_cheatcode_templateparser_open_closed_fix, samples_oop_samples_oop_cheatcode_action_vs_func [INFERRED 0.75]
- **Samples.Test exercise groups covering the OOP pillars** — samples_oop_samples_oop_samples_test_readme_encapsulation_exercises, samples_oop_samples_oop_samples_test_readme_inheritance_exercises, samples_oop_samples_oop_samples_test_readme_polymorphism_exercises, samples_oop_samples_oop_samples_test_readme_abstract_exercises, samples_oop_samples_oop_samples_test_readme_interface_exercises, readme_four_oop_pillars [INFERRED 0.85]
- **Generic variance (in/out/invariance) and its applications** — samples_oop_samples_oop_samples_generic_readme_covariance, samples_oop_samples_oop_samples_generic_readme_contravariance, samples_oop_samples_oop_samples_generic_readme_invariance, samples_oop_samples_oop_samples_generic_readme_variant_interfaces, samples_oop_samples_oop_samples_generic_readme_validation_pipeline, samples_oop_samples_oop_samples_generic_readme_repository_variance [EXTRACTED 1.00]
- **Method dispatch modifiers: override, new, base call, no modifier** — samples_oop_samples_oop_samples_oop_readme_virtual_override, samples_oop_samples_oop_samples_oop_readme_method_hiding_new, samples_oop_samples_oop_samples_oop_readme_base_call, samples_oop_samples_oop_samples_oop_readme_cs0114_warning, samples_oop_samples_oop_samples_test_readme_dispatch_cheat_sheet [EXTRACTED 1.00]

## Communities (12 total, 0 thin omitted)

### Community 0 - "Repo Docs & Delegate Notes"
Cohesion: 0.06
Nodes (42): .NET 6 SDK, Four OOP Pillars (encapsulation, inheritance, polymorphism, abstraction), OOP Repository (C# OOP & interview samples), CLAUDE.md guidance for Claude Code, Console.ReadKey throws InvalidOperationException with redirected stdin, OOP Samples solution overview, Action<T> built-in delegate (D2), Calculator.Math overloads (+34 more)

### Community 1 - "Interview Cheat Sheet"
Cohesion: 0.09
Nodes (20): C# Interview Cheat Code, AddHead Builds Linked List Backwards, Compiler Errors to Recognise (CS0114, CS0144, CS0311, CS0029, CS0266, CS1961, CS1960, CS0121), IValidator<in T> / NameRequiredValidator, Last-Minute Interview Checklist, Read/Write Repository Split (IReadOnlyRepository<out T> / IWriteRepository<in T>), The 12 Rules to Remember, Fake Technical Interview (C# OOP, Generics & Delegates) (+12 more)

### Community 2 - "Generic Variance Interfaces"
Cohesion: 0.10
Nodes (19): BachataDancer, Dancer, ID, Name, SalsaDancer, Level, IConsumer, IProducer (+11 more)

### Community 3 - "Projects & Source Files"
Cohesion: 0.14
Nodes (7): Samples.OOP.Encapsulation, Samples.OOP, Samples.Delegates, Samples.Generic, Samples.Generics, Samples.Test.Encapsulation, Programmer

### Community 4 - "Teacher Dance Hierarchy"
Cohesion: 0.10
Nodes (13): BachataTeacher, EnglishTeacher, HipHopTeacher, NewStyleTeacher, SalsaTeacher, StupidTeacher, Teacher, Description (+5 more)

### Community 5 - "Abstract Teacher Classes"
Cohesion: 0.14
Nodes (11): AbstractTeacher, Name, Schedule, AfroTeacher, Schedule, BalletTeacher, Schedule, ReguetonTeacher (+3 more)

### Community 6 - "Interfaces & Student"
Cohesion: 0.13
Nodes (7): Samples.Test, IAction, IMoveShoulder, Student, originalItemID, InterfaceSamples, Program

### Community 7 - "TemplateParser Delegates"
Cohesion: 0.18
Nodes (11): Solution, Company, City, Name, ComplexDelegate, convertToString, TemplateParser, User (+3 more)

### Community 8 - "GenericList Linked List"
Cohesion: 0.16
Nodes (6): GenericList, Node, Data, Next, Program, SampleGeneric

### Community 9 - "Dispatch & Inheritance Rules"
Cohesion: 0.22
Nodes (13): AbstractTeacher hierarchy (Afro, Ballet, Regueton), Calling parent with base.Method() (StupidTeacher), Constructor chaining : base(args), new modifier (method hiding / shadowing), Static constructor and shared static state, Teacher concrete base class, virtual / override runtime polymorphism, Abstract class exercises A1-A4 (AbstractSamples) (+5 more)

### Community 10 - "Calculator Delegates"
Cohesion: 0.36
Nodes (3): Calculator, DelegateSamples, RuleDelegate

### Community 11 - "Player Encapsulation"
Cohesion: 0.32
Nodes (4): Player, ID, Name, PlayerConsole

## Knowledge Gaps
- **36 isolated node(s):** `FirstName`, `LastName`, `Name`, `City`, `net6.0` (+31 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 56 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `Teacher` connect `Teacher Dance Hierarchy` to `Projects & Source Files`, `Interfaces & Student`?**
  _High betweenness centrality (0.057) - this node is a cross-community bridge._
- **Are the 3 inferred relationships involving `Teacher` (e.g. with `.ScenarioOne()` and `.ScenarioThree()`) actually correct?**
  _`Teacher` has 3 INFERRED edges - model-reasoned connections that need verification._
- **What connects `FirstName`, `LastName`, `Name` to the rest of the system?**
  _36 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Repo Docs & Delegate Notes` be split into smaller, more focused modules?**
  _Cohesion score 0.06458635703918723 - nodes in this community are weakly interconnected._
- **Why does `Student` connect `Interfaces & Student` to `Projects & Source Files`, `Teacher Dance Hierarchy`?**
  _High betweenness centrality (0.039) - this node is a cross-community bridge._
- **Should `Interview Cheat Sheet` be split into smaller, more focused modules?**
  _Cohesion score 0.08788159111933395 - nodes in this community are weakly interconnected._
- **Why does `Suggested learning path` connect `Interview Cheat Sheet` to `Repo Docs & Delegate Notes`, `Dispatch & Inheritance Rules`?**
  _High betweenness centrality (0.039) - this node is a cross-community bridge._