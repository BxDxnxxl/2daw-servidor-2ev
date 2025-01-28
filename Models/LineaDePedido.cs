public class LineaDePedido
{
    public int Id { get; set; }
    public int FkIdCompra { get; set; }
    public int PlatoPrincipalId { get; set; }
    public int PostreId { get; set; }
    public int BebidaId { get; set; }
    public int Cantidad { get; set; }

    // Constructor
    public LineaDePedido(int fkIdCompra, int platoPrincipalId, int postreId, int bebidaId, int cantidad)
    {
        FkIdCompra = fkIdCompra;
        PlatoPrincipalId = platoPrincipalId;
        PostreId = postreId;
        BebidaId = bebidaId;
        Cantidad = cantidad;
    }

    public LineaDePedido(){
        
    }
}
