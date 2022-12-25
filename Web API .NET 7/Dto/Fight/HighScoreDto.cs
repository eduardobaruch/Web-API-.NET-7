namespace Web_API_.NET_7.Dto.Fight
{
    public class HighScoreDto
    {
        public string Name { get; set; } = string.Empty;
        public int Fight { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}
