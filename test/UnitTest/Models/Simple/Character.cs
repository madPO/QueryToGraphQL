namespace UnitTest.Models.Simple
{
    public class Character: BaseModel
    {
        public string Name { get; set; }
        
        public Episode[] AppearsIn { get; set; }
    }
}