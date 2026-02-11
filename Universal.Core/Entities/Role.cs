namespace Universal.Core.Entities
{
    public class Role
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;
        private Role() { }

        public Role(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}