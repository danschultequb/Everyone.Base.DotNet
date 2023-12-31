﻿using System.Collections.Generic;
using System.Linq;

namespace Everyone
{
    public static class MutableSet
    {
        /// <summary>
        /// Create a new <see cref="Set{T}"/> with the provided <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">The type of values stored in the returned <see cref="Set{T}"/>.</typeparam>
        /// <param name="values">The initial values of the returned <see cref="Set{T}"/>.</param>
        public static MutableSet<T> Create<T>(params T[] values)
        {
            Pre.Condition.AssertNotNull(values, nameof(values));

            return MutableSet.Create<T>(values.ToList());
        }

        /// <summary>
        /// Create a new <see cref="Set{T}"/> with the provided <paramref name="values"/>.
        /// </summary>
        /// <typeparam name="T">The type of values stored in the returned <see cref="Set{T}"/>.</typeparam>
        /// <param name="values">The initial values of the returned <see cref="Set{T}"/>.</param>
        public static MutableSet<T> Create<T>(System.Collections.Generic.IEnumerable<T> values)
        {
            return SystemHashSet.Create(values);
        }
    }

    /// <summary>
    /// A <see cref="Set{T}"/> that can change its values.
    /// </summary>
    /// <typeparam name="T">The type of values stored in this <see cref="MutableSet{T}"/>.</typeparam>
    public interface MutableSet<T> : Set<T>, System.Collections.Generic.ISet<T>
    {
        /// <summary>
        /// Get the number of values in this <see cref="MutableSet{T}"/>.
        /// </summary>
        public new int Count { get; }

        /// <summary>
        /// Get whether this <see cref="MutableSet{T}"/> contains the provided
        /// <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to look for.</param>
        public new bool Contains(T value);

        /// <summary>
        /// Add the provided value to this <see cref="Set{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <returns>This object for method chaining.</returns>
        public new MutableSet<T> Add(T value);

        /// <summary>
        /// Add the provided value to this <see cref="Set{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <param name="added">Whether the value was added. This will be false if the value
        /// already existed in this <see cref="Set{T}"/>.</param>
        /// <returns>This object for method chaining.</returns>
        public MutableSet<T> Add(T value, out bool added);

        /// <summary>
        /// Add all of the provided <paramref name="values"/> to this <see cref="Set{T}"/>.
        /// </summary>
        /// <param name="values">The values to add.</param>
        /// <returns>This object for method chaining.</returns>
        public MutableSet<T> AddAll(System.Collections.Generic.IEnumerable<T> values);

        /// <summary>
        /// Add all of the provided <paramref name="values"/> to this <see cref="Set{T}"/>.
        /// </summary>
        /// <param name="values">The values to add.</param>
        /// <param name="added">Whether each value was added. The <see cref="Map{TKey, TValue}"/>'s
        /// values will be false if the corresponding key already existed in this
        /// <see cref="Set{T}"/>.
        /// <returns>This object for method chaining.</returns>
        public MutableSet<T> AddAll(System.Collections.Generic.IEnumerable<T> values, out System.Collections.Generic.IEnumerable<(T, bool)> added);

        /// <summary>
        /// Remove the provided <paramref name="value"/> from this <see cref="Set{T}"/>.
        /// </summary>
        /// <param name="value">The value to remove.</param>
        /// <returns>Whether the <paramref name="value"/> existed in this <see cref="Set{T}"/>.</returns>
        public new bool Remove(T value);

        /// <summary>
        /// Remove the provided <paramref name="values"/> from this <see cref="Set{T}"/>.
        /// </summary>
        /// <param name="values">The values to remove.</param>
        /// <returns>Whether the provided <paramref name="values"/> existed in this
        /// <see cref="Set{T}"/>.</returns>
        public System.Collections.Generic.IEnumerable<(T, bool)> RemoveAll(System.Collections.Generic.IEnumerable<T> values);

        public new bool IsProperSubsetOf(IEnumerable<T> other);

        public new bool IsProperSupersetOf(IEnumerable<T> other);

        public new bool IsSubsetOf(IEnumerable<T> other);

        public new bool IsSupersetOf(IEnumerable<T> other);

        public new bool Overlaps(IEnumerable<T> other);

        public new bool SetEquals(IEnumerable<T> other);
    }

    public abstract class MutableSetBase<T,TMutableSet> : SetBase<T>, MutableSet<T>
        where TMutableSet : class, MutableSet<T>
    {
        public bool IsReadOnly => false;

        MutableSet<T> MutableSet<T>.Add(T value)
        {
            return this.Add(value);
        }

        /// <summary>
        /// Add the provided value to this <see cref="Set{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <returns>This object for method chaining.</returns>
        public abstract TMutableSet Add(T value);

        MutableSet<T> MutableSet<T>.Add(T value, out bool added)
        {
            return this.Add(value, out added);
        }

        /// <summary>
        /// Add the provided value to this <see cref="Set{T}"/>.
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <param name="added">Whether the value was added. This will be false if the value
        /// already existed in this <see cref="Set{T}"/>.</param>
        /// <returns>This object for method chaining.</returns>
        public abstract TMutableSet Add(T value, out bool added);

        MutableSet<T> MutableSet<T>.AddAll(System.Collections.Generic.IEnumerable<T> values)
        {
            return this.AddAll(values);
        }

        /// <summary>
        /// Add all of the provided <paramref name="values"/> to this <see cref="Set{T}"/>.
        /// </summary>
        /// <param name="values">The values to add.</param>
        /// <returns>This object for method chaining.</returns>
        public virtual TMutableSet AddAll(System.Collections.Generic.IEnumerable<T> values)
        {
            Pre.Condition.AssertNotNull(values, nameof(values));

            foreach (T value in values)
            {
                this.Add(value);
            }

            return (this as TMutableSet)!;
        }

        MutableSet<T> MutableSet<T>.AddAll(System.Collections.Generic.IEnumerable<T> values, out System.Collections.Generic.IEnumerable<(T, bool)> added)
        {
            return this.AddAll(values, out added);
        }

        /// <summary>
        /// Add all of the provided <paramref name="values"/> to this <see cref="Set{T}"/>.
        /// </summary>
        /// <param name="values">The values to add.</param>
        /// <param name="added">Whether each value was added. The <see cref="Map{TKey, TValue}"/>'s
        /// values will be false if the corresponding key already existed in this
        /// <see cref="Set{T}"/>.
        /// <returns>This object for method chaining.</returns>
        public virtual TMutableSet AddAll(System.Collections.Generic.IEnumerable<T> values, out System.Collections.Generic.IEnumerable<(T, bool)> added)
        {
            Pre.Condition.AssertNotNull(values, nameof(values));

            List<(T, bool)> addedList = List.Create<(T, bool)>();
            bool valueAdded;
            foreach (T value in values)
            {
                this.Add(value, out valueAdded);
                addedList.Add((value, valueAdded));
            }
            added = addedList;

            return (this as TMutableSet)!;
        }

        bool ISet<T>.Add(T item)
        {
            this.Add(item, out bool added);
            return added;
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            this.RemoveAll(other);
        }

        public abstract void IntersectWith(IEnumerable<T> other);

        public abstract void SymmetricExceptWith(IEnumerable<T> other);

        public abstract void UnionWith(IEnumerable<T> other);

        void ICollection<T>.Add(T item)
        {
            this.Add(item);
        }

        public abstract void Clear();

        public abstract void CopyTo(T[] array, int arrayIndex);

        public abstract bool Remove(T value);

        public virtual IEnumerable<(T, bool)> RemoveAll(IEnumerable<T> values)
        {
            Pre.Condition.AssertNotNull(values, nameof(values));

            List<(T, bool)> result = List.Create<(T, bool)>();
            foreach (T value in values)
            {
                result.Add((value, this.Remove(value)));
            }
            return result;
        }
    }

    public abstract class MutableSetDecorator<T,TMutableSet> : MutableSetBase<T,TMutableSet>
        where TMutableSet : class, MutableSet<T>
    {
        private readonly MutableSet<T> innerSet;

        protected MutableSetDecorator(MutableSet<T> innerSet)
        {
            Pre.Condition.AssertNotNull(innerSet, nameof(innerSet));

            this.innerSet = innerSet;
        }

        public override int Count => this.innerSet.Count;

        public override TMutableSet Add(T value)
        {
            this.innerSet.Add(value);

            return (this as TMutableSet)!;
        }

        public override TMutableSet Add(T value, out bool added)
        {
            this.innerSet.Add(value, out added);

            return (this as TMutableSet)!;
        }

        public override void Clear()
        {
            this.innerSet.Clear();
        }

        public override bool Contains(T value)
        {
            return this.innerSet.Contains(value);
        }

        public override void CopyTo(T[] array, int arrayIndex)
        {
            this.innerSet.CopyTo(array, arrayIndex);
        }

        public override void IntersectWith(IEnumerable<T> other)
        {
            this.innerSet.IntersectWith(other);
        }

        public override bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return this.innerSet.IsProperSubsetOf(other);
        }

        public override bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return this.innerSet.IsProperSupersetOf(other);
        }

        public override bool IsSubsetOf(IEnumerable<T> other)
        {
            return this.innerSet.IsSubsetOf(other);
        }

        public override bool IsSupersetOf(IEnumerable<T> other)
        {
            return this.innerSet.IsSupersetOf(other);
        }

        public override Iterator<T> Iterate()
        {
            return this.innerSet.Iterate();
        }

        public override bool Overlaps(IEnumerable<T> other)
        {
            return this.innerSet.Overlaps(other);
        }

        public override bool Remove(T value)
        {
            return this.innerSet.Remove(value);
        }

        public override bool SetEquals(IEnumerable<T> other)
        {
            return this.innerSet.SetEquals(other);
        }

        public override void SymmetricExceptWith(IEnumerable<T> other)
        {
            this.innerSet.SymmetricExceptWith(other);
        }

        public override void UnionWith(IEnumerable<T> other)
        {
            this.innerSet.UnionWith(other);
        }
    }
}
