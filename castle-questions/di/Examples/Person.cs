using System;

namespace Di.Examples
{
    public class Person
    {
        public string Name { get; set; }
        public Guid Id { get; private set; }
        
        public Person()
        {
            Id = Guid.NewGuid();
        }
    }
}
