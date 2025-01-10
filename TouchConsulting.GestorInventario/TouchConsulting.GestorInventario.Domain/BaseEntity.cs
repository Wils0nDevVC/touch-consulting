using System;

namespace TouchConsulting.GestorInventario.Domain
{
    public class BaseEntity
    {
        public DateTimeOffset createAt { get; set; }
        public DateTimeOffset? updateAt { get; set; }
    }
}
