namespace Sids.Prodesp.Model.Entity.Seguranca
{
    public class Captcha
    {
        public int[] Image { get; set; }
        public string Audio { get; set; }
        public string Resposta { get; set; }
        public string Encoding { get; set; }
        public decimal SampleRate { get; set; }
        public decimal FrameRate { get; set; }
        public int SampleSizeInBits { get; set; }
        public int Channels { get; set; }
        public int FrameSize { get; set; }
        public bool BigEndian { get; set; }
    }
}
