namespace LAB4AProject.Data
{
    public class AtlasSettings : IAtlasSettings
    {
        public string Collection { get; set; }
        public string Database { get; set; }
        public string ConnectionString { get; set; }
    }
    public interface IAtlasSettings
    {
        string Collection { get; set; }
        string Database { get; set; }
        string ConnectionString { get; set; }
    }
}