namespace Abyat.Da.Exceptions
{
    [Serializable]
    internal class EntityNotFoundException : Exception
    {
        private string name;
        private Guid id;

        public EntityNotFoundException()
        {
        }

        public EntityNotFoundException(string? message) : base(message)
        {
        }

        public EntityNotFoundException(string name, Guid id)
        {
            this.name = name;
            this.id = id;
        }

        public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}