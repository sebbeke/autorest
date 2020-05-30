using System;

namespace Autorest.Domain.Infrastructure
{
    public interface IRepositoryObservable<T>
    {
        IObservable<T> WhenCreated { get; }
        IObservable<T> WhenUpdated { get; }
        IObservable<T> WhenRemoved { get; }
        void Add(T model);
        void Edit(T model);
        void Delete(T model);
    }
}