﻿namespace AgenciaOpen.Domain
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
