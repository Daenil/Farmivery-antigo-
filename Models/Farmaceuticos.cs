public class Farmaceuticos : Pessoas
{
    public int FarmaceuticoId { get; set; }
    public Pessoas Pessoas  { get; set;}     
    public Farmacias Farmacias { get; set; }
}