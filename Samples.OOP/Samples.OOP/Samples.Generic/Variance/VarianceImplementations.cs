using System;
using System.Collections.Generic;
using System.Linq;

namespace Samples.Generics
{
    public class SalsaDancerFactory : IProducer<SalsaDancer>
    {
        private int _nextId = 100;

        public SalsaDancer Produce()
        {
            _nextId++;
            return new SalsaDancer($"salsa-{_nextId}", _nextId);
        }
    }

    public class DancerPrinter : IConsumer<Dancer>
    {
        public void Consume(Dancer item) => Console.WriteLine($"Printing {item.GetType().Name}: {item.Name}");
    }

    public class NameRequiredValidator : IValidator<Dancer>
    {
        public string Validate(Dancer item) => string.IsNullOrWhiteSpace(item.Name) ? "Name is required" : null;
    }

    public class SalsaLevelValidator : IValidator<SalsaDancer>
    {
        public string Validate(SalsaDancer item) => item.Level < 1 ? "Salsa level must be 1 or higher" : null;
    }

    // Generic base class implementing a covariant (read) and a contravariant (write) interface
    public class InMemoryRepository<T> : IReadOnlyRepository<T>, IWriteRepository<T> where T : Dancer
    {
        private readonly List<T> _items = new List<T>();

        public void Add(T item) => _items.Add(item);

        public IEnumerable<T> GetAll() => _items;
    }

    // Generic inheritance: closes the generic base for one type and adds type-specific queries
    public class SalsaRepository : InMemoryRepository<SalsaDancer>
    {
        public IEnumerable<SalsaDancer> GetByMinLevel(int level) => GetAll().Where(d => d.Level >= level);
    }

    // Accepts any dancer, so it can be plugged in wherever a writer of a subtype is expected
    public class AuditLogRepository : IWriteRepository<Dancer>
    {
        public void Add(Dancer item) => Console.WriteLine($"[audit] saved {item.GetType().Name} {item.Name}");
    }
}
