using System;

namespace Mafia.Application.Paggination
{
    public class SortableAttribute : Attribute
    {
        public string OrderBy { get; set; }
    }
}
