using AutoRest.Context.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoRest.Context.Contracts
{
    /// <summary>
    /// Контекст работы с сущностями
    /// </summary>
    public interface IAutoRestContext
    {
        /// <summary>Список <inheritdoc cref="Employee"/></summary>
        DbSet<Employee> Employees { get; }

        /// <summary>Список <inheritdoc cref="LoyaltyCard"/></summary>
        DbSet<LoyaltyCard> LoyaltyCards { get; }

        /// <summary>Список <inheritdoc cref="MenuItem"/></summary>
        DbSet<MenuItem> MenuItems { get; }

        /// <summary>Список <inheritdoc cref="OrderItem"/></summary>
        DbSet<OrderItem> OrderItems { get; }

        /// <summary>Список <inheritdoc cref="Person"/></summary>
        DbSet<Person> Persons { get; }

        /// <summary>Список <inheritdoc cref="Table"/></summary>
        DbSet<Table> Tables { get; }

    }
}
