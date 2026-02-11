namespace Universal.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public Guid RoleId { get; private set; }
        public Role Role { get; private set; } = default!;

        private User() { }
        public User(string email, string passwordHash, Guid roleId)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
            RoleId = roleId;
        }
    }
}