using System;
using System.Collections.Generic;

namespace Samples.Generics
{
    // 'out' (covariance): T is only RETURNED.
    // An IProducer<SalsaDancer> can be used as an IProducer<Dancer>.
    public interface IProducer<out T>
    {
        T Produce();
        // void Accept(T item); // ERROR CS1961: an 'out' T can't be used as a parameter
    }

    // 'in' (contravariance): T is only ACCEPTED.
    // An IConsumer<Dancer> can be used as an IConsumer<SalsaDancer>.
    public interface IConsumer<in T>
    {
        void Consume(T item);
        // T Last(); // ERROR CS1961: an 'in' T can't be returned
    }

    // Real world: a validator written for the base type is reused for every subtype
    public interface IValidator<in T>
    {
        // Returns the error message, or null when the item is valid
        string Validate(T item);
    }

    // Real world: read side of a repository, safe to treat as a repository of the base type
    public interface IReadOnlyRepository<out T>
    {
        IEnumerable<T> GetAll(); // IEnumerable<out T> keeps T in an output position
    }

    // Real world: write side of a repository / sink that accepts the base type
    public interface IWriteRepository<in T>
    {
        void Add(T item);
    }
}
