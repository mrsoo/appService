using System;

namespace EMSystem.Domain.Interface
{
    public interface IEntity
    {
        int id { get; set; }
        DateTime CreatedAt { get; set; }
        bool dis { get; set; }
    }
}
