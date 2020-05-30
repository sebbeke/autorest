using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;

namespace Autorest.Domain.Infrastructure
{
    public class RepositoryObservable<T> : IRepositoryObservable<T>
    {
        private readonly Subject<T> _whenCreated;
        private readonly Subject<T> _whenUpdated;
        private readonly Subject<T> _whenRemoved;

        public IObservable<T> WhenCreated => _whenCreated.AsObservable();
        public IObservable<T> WhenUpdated => _whenUpdated.AsObservable();
        public IObservable<T> WhenRemoved => _whenRemoved.AsObservable();

        public RepositoryObservable()
        {
            _whenCreated = new Subject<T>();
            _whenUpdated = new Subject<T>();
            _whenRemoved = new Subject<T>();
        }

        public void Add(T model)
        {
            _whenCreated.OnNext(model);
        }

        public void Edit(T model)
        {
            _whenUpdated.OnNext(model);
        }

        public void Delete(T model)
        {
            _whenUpdated.OnNext(model);
        }
    }
}
