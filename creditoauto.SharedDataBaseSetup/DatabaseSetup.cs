﻿using Bogus;
using creditoauto.Entity.Models;
using creditoauto.Repository.Context;

namespace creditoauto.SharedDataBaseSetup
{
    public static class DatabaseSetup
    {
        public static void SeedData(DataContext context)
        {
            #region Clientes
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
            #endregion

            #region Patio
            context.Patios.RemoveRange(context.Patios);

            var patioIds = 1;
            var fakePatios = new Faker<Patio>()
                .RuleFor(o => o.Codigo, f => $"Codigo {patioIds}")
                .RuleFor(o => o.Descripcion, f => $"Descripcion {patioIds}")
                .RuleFor(o => o.Nombre, f => $"Nombre {patioIds}")
                .RuleFor(o => o.Id, f => patioIds++);
                

            var patios = fakePatios.Generate(10);

            context.AddRange(patios);
            #endregion

            #region Marca
            context.Marcas.RemoveRange(context.Marcas);

            var marcaIds = 1;
            var fakeMarca = new Faker<Marca>()
                .RuleFor(o => o.Descripcion, f => $"Descripcion {marcaIds}")
                .RuleFor(o => o.Nombre, f => $"Nombre {marcaIds}")
                .RuleFor(o => o.Id, f => marcaIds++);


            var marcas = fakeMarca.Generate(1);

            context.AddRange(marcas);
            #endregion

            #region Vehiculo
            context.Vehiculos.RemoveRange(context.Vehiculos);

            var vehiculoIds = 1;
            var fakeVehiculo = new Faker<Vehiculo>()
                .RuleFor(o => o.Avaluo, f => 2000)
                .RuleFor(o => o.NumeroChasis, f => $"NumeroChasis {vehiculoIds}")
                .RuleFor(o => o.Cilindraje, f => $"Cilindraje {vehiculoIds}")
                .RuleFor(o => o.MarcaId, 1)
                .RuleFor(o => o.Tipo, f => $"Tipo, {vehiculoIds}")
                .RuleFor(o => o.Modelo, f => $"Modelo {vehiculoIds}")
                .RuleFor(o => o.Placa, f => $"Placa {vehiculoIds}")
                .RuleFor(o => o.Id, f => vehiculoIds++);


            var vehiculos = fakeVehiculo.Generate(10);

            context.AddRange(vehiculos);
            #endregion

            context.SaveChanges();
        }
    }
}