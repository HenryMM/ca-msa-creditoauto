
namespace creditoauto.Entity.DTO
{
    public class RespuestaGenerica<T> where T : class
    {
        public bool IsSuccessfull { get; set; }
        public string Mensaje { get; set; }
        public T Data { get; set; }
        public string Exception { get; set; }
    }
}
