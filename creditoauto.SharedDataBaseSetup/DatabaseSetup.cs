using Bogus;
using creditoauto.Entity.Models;
using creditoauto.Repository.Context;

namespace creditoauto.SharedDataBaseSetup
{
    public static class DatabaseSetup
    {
        public static void SeedData(DataContext context)
        {
            context.Clientes.RemoveRange(context.Clientes);

            var clienteIds = 1;
            var fakeClientes = new Faker<Cliente>()
                .RuleFor(o => o.Nombres, f => $"Nombres {clienteIds}")
                .RuleFor(o => o.Apellidos, f => $"Apellidos {clienteIds}")
                .RuleFor(o => o.Direccion, f => $"Direccion {clienteIds}")
                .RuleFor(o => o.EstadoCivil, f => $"EstadoCivil {clienteIds}")
                .RuleFor(o => o.Identificacion, f => $"Identificacion {clienteIds}")
                .RuleFor(o => o.IdentificacionConyuge, f => $"IdentificacionConyuge {clienteIds}")
                .RuleFor(o => o.NombreConyuge, f => $"NombreConyuge {clienteIds}")
                .RuleFor(o => o.SujetoCredito, f => $"SujetoCredito {clienteIds}")
                .RuleFor(o => o.Telefono, f => $"Telefono {clienteIds}")
                .RuleFor(o => o.Id, f => clienteIds++);

            var clientes = fakeClientes.Generate(10);

            context.AddRange(clientes);

            context.SaveChanges();
        }
    }
}