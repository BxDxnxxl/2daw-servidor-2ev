using Models;

public class Compra {

    public int Id {get;set;}
    public DateTime FechaCompra{get; set;}
    public int FkIdUsuario{get; set;}

    public Compra(int id, int fkIdUsuario, DateTime fechaCompra) {
        Id = id;
        FkIdUsuario = fkIdUsuario;
        FechaCompra = fechaCompra;
    }

    public Compra(){
        
    }


}