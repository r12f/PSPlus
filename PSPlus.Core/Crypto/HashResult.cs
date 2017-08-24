namespace PSPlus.Core.Crypto
{
    public class HashResult
    {
        public string Algorithm { get; set; }
        public string Hash { get; set; }
        public byte[] HashBuffer { get; set; }
    }
}
