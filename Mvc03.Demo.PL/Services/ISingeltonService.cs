namespace Mvc03.Demo.PL.Services
{
    public interface ISingeltonService
    {
        public Guid Guid { get; set; }
        string GetGuid();
    }
}
