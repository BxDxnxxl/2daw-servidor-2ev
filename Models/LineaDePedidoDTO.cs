using Models;

public class LienaDePedidoDTO {

    public string PlatoPrincipalNombre{get; set;}
    public string BebidaNombre{get; set;}
    public string PostreNombre{get; set;}

public LienaDePedidoDTO(string platoPrincipalNombre, string bebidaNombre, string postreNombre) {
   PlatoPrincipalNombre = platoPrincipalNombre;
   BebidaNombre = bebidaNombre;
   PostreNombre = postreNombre;
   
}

public LienaDePedidoDTO(){
    
}

}