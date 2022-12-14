namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Data
{
    /// <summary>
    /// Used for adding AndAlso() method to the <see cref="Microsoft.EntityFrameworkCore.DbContext"/> builder.
    /// </summary>
    public interface IAndWithDbContextBuilder : IWithDbContextBuilder
    {
        /// <summary>
        /// AndAlso method for better readability when building <see cref="Microsoft.EntityFrameworkCore.DbContext"/>.
        /// </summary>
        /// <returns>The same <see cref="IWithDbContextBuilder"/>.</returns>
        IWithDbContextBuilder AndAlso();
    }
}
