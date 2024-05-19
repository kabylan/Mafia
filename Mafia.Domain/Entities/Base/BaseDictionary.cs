namespace Mafia.Domain.Entities.Base
{
    public abstract class BaseDictionary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public string KifCode { get; set; }
        public string OldCode { get; set; }
    }
}