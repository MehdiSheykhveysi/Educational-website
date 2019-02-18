﻿namespace Site.Core.Domain.Entities
{
    public interface IEntity
    {

    }
    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }
    public abstract class BaseEntity : BaseEntity<int>
    {

    }
}
