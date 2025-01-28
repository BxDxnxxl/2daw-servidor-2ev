using Models;

public class Usuario {

    public int Id {get; set;}
    public string Nombre{get; set;}
    public string Apellidos{get; set;}
    public string UsuarioNombre{get; set;}

public Usuario(int id, string nombre, string apellidos, string usuarioNombre) {
   Id = id;
   Nombre = nombre;
   Apellidos = apellidos;
   UsuarioNombre = usuarioNombre;
   
}

public Usuario(){
    
}

}