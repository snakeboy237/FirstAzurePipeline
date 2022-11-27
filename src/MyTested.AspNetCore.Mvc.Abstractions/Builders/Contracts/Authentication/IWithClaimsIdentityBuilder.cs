﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication
{
    using System.Collections.Generic;
    using System.Security.Claims;

    /// <summary>
    /// Used for building mocked <see cref="ClaimsIdentity"/>.
    /// </summary>
    public interface IWithClaimsIdentityBuilder
    {
        /// <summary>
        /// Sets type of the username claim. Default is <see cref="ClaimTypes.Name"/>.
        /// </summary>
        /// <param name="nameType">Type to set on the username claim.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder WithNameType(string nameType);

        /// <summary>
        /// Sets type of the role claim. Default is <see cref="ClaimTypes.Role"/>.
        /// </summary>
        /// <param name="roleType">Type to set on the role claim.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder WithRoleType(string roleType);

        /// <summary>
        /// Sets identifier claim to the built <see cref="ClaimsIdentity"/>. If such is not provided, "TestId" is used by default.
        /// </summary>
        /// <param name="identifier">Value of the identifier claim - <see cref="ClaimTypes.NameIdentifier"/>.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder WithIdentifier(string identifier);

        /// <summary>
        /// Sets username claims to the built <see cref="ClaimsIdentity"/>. If such is not provided, "TestUser" is used by default.
        /// </summary>
        /// <param name="username">Value of the username claim. Default claim type is <see cref="ClaimTypes.Name"/>.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder WithUsername(string username);

        /// <summary>
        /// Adds claim to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="type">Type of the <see cref="Claim"/> to add.</param>
        /// <param name="value">Value of the <see cref="Claim"/> to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder WithClaim(string type, string value);

        /// <summary>
        /// Adds claim to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="claim">The <see cref="Claim"/> to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder WithClaim(Claim claim);

        /// <summary>
        /// Adds claims to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="claims">Collection of <see cref="Claim"/> to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder WithClaims(IEnumerable<Claim> claims);

        /// <summary>
        /// Adds claims to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="claims"><see cref="Claim"/> parameters to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder WithClaims(params Claim[] claims);

        /// <summary>
        /// Adds authentication type to the built <see cref="ClaimsIdentity"/>. If such is not provided, "Passport" is used by default.
        /// </summary>
        /// <param name="authenticationType">Authentication type to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder WithAuthenticationType(string authenticationType);

        /// <summary>
        /// Adds role to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="role">Value of the role claim. Default claim type is <see cref="ClaimTypes.Role"/>.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder InRole(string role);

        /// <summary>
        /// Adds roles to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder InRoles(IEnumerable<string> roles);

        /// <summary>
        /// Adds roles to the built <see cref="ClaimsIdentity"/>.
        /// </summary>
        /// <param name="roles">Role name parameters to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsIdentityBuilder"/>.</returns>
        IAndWithClaimsIdentityBuilder InRoles(params string[] roles);
    }
}
