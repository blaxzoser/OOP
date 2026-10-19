using System;
using System.Collections.Generic;
using System.Linq;

namespace Samples.Generics
{
    /// <summary>
    /// Generic variance: 'out' (covariance) and 'in' (contravariance)
    /// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/covariance-contravariance/
    /// </summary>
    public static class VarianceSamples
    {
        /// <summary>
        /// BASIC - Covariance (out) with IEnumerable&lt;out T&gt;
        /// Real world: one method that takes IEnumerable&lt;Dancer&gt; works for lists of every subtype
        /// </summary>
        public static void ScenarioOne()
        {
            List<SalsaDancer> salsaDancers = new List<SalsaDancer>
            {
                new SalsaDancer("miguel", 1, level: 3),
                new SalsaDancer("carlos", 2, level: 1)
            };

            IEnumerable<Dancer> dancers = salsaDancers; // GOOD: a sequence of SalsaDancer IS a sequence of Dancer
            PrintNames(dancers);

            // List<T> is invariant because it reads AND writes:
            // List<Dancer> list = salsaDancers; // ERROR CS0029
            // If it compiled, list.Add(new BachataDancer(...)) would put a bachata dancer in a salsa list

            List<BachataDancer> bachataDancers = new List<BachataDancer> { new BachataDancer("kakaroto", 3) };
            PrintNames(bachataDancers); // same method, other subtype
        }

        /// <summary>
        /// BASIC - Contravariance (in) with Action&lt;in T&gt; and IComparer&lt;in T&gt;
        /// Real world: one comparer / handler written for the base type is reused for every subtype
        /// </summary>
        public static void ScenarioTwo()
        {
            Action<Dancer> greet = d => Console.WriteLine($"Welcome {d.Name}!");
            Action<SalsaDancer> greetSalsa = greet; // GOOD: code that handles any Dancer can handle a SalsaDancer
            greetSalsa(new SalsaDancer("diana", 4, level: 2));

            IComparer<Dancer> byName = Comparer<Dancer>.Create((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));
            List<SalsaDancer> salsaDancers = new List<SalsaDancer>
            {
                new SalsaDancer("zoe", 5),
                new SalsaDancer("ana", 6),
                new SalsaDancer("luis", 7)
            };
            salsaDancers.Sort(byName); // Sort expects IComparer<SalsaDancer>, IComparer<Dancer> is accepted
            Console.WriteLine("Sorted: " + string.Join(", ", salsaDancers.Select(d => d.Name)));
        }

        /// <summary>
        /// INTERMEDIATE - Your own variant interfaces and delegates
        /// Real world: factories / read-only sources are 'out', handlers / sinks are 'in'
        /// </summary>
        public static void ScenarioThree()
        {
            IProducer<SalsaDancer> salsaFactory = new SalsaDancerFactory();
            IProducer<Dancer> anyFactory = salsaFactory; // out: Producer<SalsaDancer> -> Producer<Dancer>
            Dancer produced = anyFactory.Produce();
            Console.WriteLine($"Produced {produced.GetType().Name}: {produced.Name}");

            IConsumer<Dancer> printer = new DancerPrinter();
            IConsumer<SalsaDancer> salsaPrinter = printer; // in: Consumer<Dancer> -> Consumer<SalsaDancer>
            salsaPrinter.Consume(salsaFactory.Produce());

            // The opposite directions don't compile:
            // IProducer<SalsaDancer> p = anyFactory;  // ERROR CS0266: a Dancer producer could return a BachataDancer
            // IConsumer<Dancer> c = salsaPrinter;     // ERROR CS0266: a SalsaDancer consumer can't take a BachataDancer

            // Func<in T, out TResult> is both at once: narrower input, wider output
            Func<Dancer, string> describe = d => $"{d.Name}#{d.ID}";
            Func<SalsaDancer, object> describeSalsa = describe;
            Console.WriteLine($"Described: {describeSalsa(new SalsaDancer("leo", 8))}");
        }

        /// <summary>
        /// INTERMEDIATE - Pitfalls: arrays, value types and classes
        /// </summary>
        public static void ScenarioFour()
        {
            // 1. Arrays are covariant too, but it's only checked at runtime
            Dancer[] dancers = new SalsaDancer[2];
            dancers[0] = new SalsaDancer("miguel", 1);
            try
            {
                dancers[1] = new BachataDancer("carlos", 2); // compiles, fails when it runs
            }
            catch (ArrayTypeMismatchException ex)
            {
                Console.WriteLine($"ArrayTypeMismatchException: {ex.Message}");
            }

            // 2. Variance only works for reference types
            IEnumerable<object> words = new List<string> { "salsa", "bachata" }; // GOOD: string is a reference type
            List<int> numbers = new List<int> { 1, 2, 3 };
            // IEnumerable<object> boxed = numbers; // ERROR CS0266: int is a value type, it would need boxing
            IEnumerable<object> boxed = numbers.Cast<object>(); // GOOD: box explicitly
            Console.WriteLine($"words: {words.Count()}, boxed numbers: {boxed.Count()}");

            // 3. Only interfaces and delegates can be variant, never classes
            // public class Box<out T> { } // ERROR CS1960
            // That's why List<T> is invariant but its read-only interface IReadOnlyList<out T> is not
            IReadOnlyList<Dancer> readOnly = new List<SalsaDancer> { new SalsaDancer("ana", 3) };
            Console.WriteLine($"IReadOnlyList<Dancer> first: {readOnly[0].Name}");
        }

        /// <summary>
        /// ADVANCED - Validation pipeline: contravariance + generic method + constraint
        /// Real world: shared validators / event handlers for a base type mixed with type-specific ones
        /// </summary>
        public static void ScenarioFive()
        {
            var salsaValidators = new List<IValidator<SalsaDancer>>
            {
                new NameRequiredValidator(), // IValidator<Dancer> used as IValidator<SalsaDancer>
                new SalsaLevelValidator()
            };
            var bachataValidators = new List<IValidator<BachataDancer>>
            {
                new NameRequiredValidator() // same validator class reused for another subtype
            };

            Validate(new SalsaDancer("miguel", 1, level: 2), salsaValidators);
            Validate(new SalsaDancer("", 2, level: 0), salsaValidators);
            Validate(new BachataDancer("carlos", 3), bachataValidators);
        }

        /// <summary>
        /// ADVANCED - Generic inheritance + 'in' and 'out' on the same class
        /// Real world: repository with separate read (out) and write (in) contracts, reports over many repositories
        /// </summary>
        public static void ScenarioSix()
        {
            var salsaRepository = new SalsaRepository();                   // : InMemoryRepository<SalsaDancer>
            IWriteRepository<SalsaDancer> audit = new AuditLogRepository(); // in: IWriteRepository<Dancer> -> <SalsaDancer>

            foreach (var dancer in new[] { new SalsaDancer("miguel", 1, level: 3), new SalsaDancer("diana", 2, level: 1) })
            {
                salsaRepository.Add(dancer);
                audit.Add(dancer);
            }

            foreach (var dancer in salsaRepository.GetByMinLevel(2)) // query only the subclass has
            {
                Console.WriteLine($"Level 2+: {dancer.Name}");
            }

            var bachataRepository = new InMemoryRepository<BachataDancer>();
            bachataRepository.Add(new BachataDancer("carlos", 3));

            // out: both repositories become IReadOnlyRepository<Dancer> for one report
            PrintReport(new IReadOnlyRepository<Dancer>[] { salsaRepository, bachataRepository });

            // IWriteRepository<Dancer> w = salsaRepository; // ERROR CS0266: it only accepts SalsaDancer
        }

        private static void PrintNames(IEnumerable<Dancer> dancers)
        {
            foreach (var dancer in dancers)
            {
                Console.WriteLine($"{dancer.GetType().Name}: {dancer.Name}");
            }
        }

        private static void Validate<T>(T dancer, IEnumerable<IValidator<T>> validators) where T : Dancer
        {
            var errors = validators.Select(v => v.Validate(dancer)).Where(e => e != null).ToList();
            Console.WriteLine(errors.Count == 0
                ? $"{typeof(T).Name} '{dancer.Name}' is valid"
                : $"{typeof(T).Name} '{dancer.Name}' is invalid: {string.Join("; ", errors)}");
        }

        private static void PrintReport(IEnumerable<IReadOnlyRepository<Dancer>> repositories)
        {
            foreach (var repository in repositories)
            {
                foreach (var dancer in repository.GetAll())
                {
                    Console.WriteLine($"Report: {dancer.GetType().Name} {dancer.Name}");
                }
            }
        }
    }
}
