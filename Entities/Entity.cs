namespace Entities
{
    public abstract class Entity : AccessControl
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual void EnableEntity()
        {
            this.CreatedAt = DateTime.Now;
            this.Active = true;
        }

        public virtual void DisableEntity()
        {
            this.Active = false;
        }
    }
    public abstract class AccessControl
    {
        public int AccessCount { get; set; }
        public int AccessUserId { get; set; }
        public DateTime LastAccess { get; set; }

        public virtual void AccessEntity()
        {
            LastAccess = DateTime.Now;
            AccessCount++;
        }
    }
}
