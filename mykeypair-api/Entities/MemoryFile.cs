namespace mykeypair_api.Entities
{
    public class MemoryFile
    {
        public string Name { get; set; }
        public byte[] Data { get; set; }
        public MemoryFile(string name, byte[] data)
        {
            this.Name = name;
            this.Data = data;
        }
    }
}