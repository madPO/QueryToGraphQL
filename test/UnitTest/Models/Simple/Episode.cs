namespace UnitTest.Models.Simple
{
    public class Episode: BaseModel
    {
        public string Name { get; set; }
        
        public Person Author { get; set; }
    }
}