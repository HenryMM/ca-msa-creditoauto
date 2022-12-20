using creditoauto.Entity.Models;
using FluentValidation;

namespace creditoauto.Common.Validators
{
    public class VehiculoValidator : AbstractValidator<Vehiculo>
    {
        public VehiculoValidator()
        {
            RuleFor(x=>x.Placa).NotNull().NotEmpty();
            RuleFor(x => x.Avaluo).NotNull().NotEmpty();
            RuleFor(x => x.Modelo).NotNull().NotEmpty();
            RuleFor(x => x.Cilindraje).NotNull().NotEmpty();
            RuleFor(x => x.NumeroChasis).NotNull().NotEmpty();
        }
    }
}
