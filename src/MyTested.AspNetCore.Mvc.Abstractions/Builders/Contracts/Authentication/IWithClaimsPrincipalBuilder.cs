﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Security.Principal;

    /// <summary>
    /// Used for building mocked <see cref="ClaimsPrincipal"/>.
    /// </summary>
    public interface IWithClaimsPrincipalBuilder
    {
        /// <summary>
        /// Sets type of the username claim. Default is <see cref="ClaimTypes.Name"/>.
        /// </summary>
        /// <param name="nameType">Type to set on the username claim.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithNameType(string nameType);

        /// <summary>
        /// Sets type of the role claim. Default is <see cref="ClaimTypes.Role"/>.
        /// </summary>
        /// <param name="roleType">Type to set on the role claim.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithRoleType(string roleType);

        /// <summary>
        /// Sets identifier (Id) claim to the built <see cref="ClaimsPrincipal"/>. If such is not provided, "TestId" is used by default.
        /// </summary>
        /// <param name="identifier">Value of the identifier claim - <see cref="ClaimTypes.NameIdentifier"/>.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithIdentifier(string identifier);

        /// <summary>
        /// Sets username claims to the built <see cref="ClaimsPrincipal"/>. If such is not provided, "TestUser" is used by default.
        /// </summary>
        /// <param name="username">Value of the username claim. Default claim type is <see cref="ClaimTypes.Name"/>.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithUsername(string username);

        /// <summary>
        /// Adds claim to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="type">Type of the <see cref="Claim"/> to add.</param>
        /// <param name="value">Value of the <see cref="Claim"/> to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithClaim(string type, string value);

        /// <summary>
        /// Adds claim to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claim">The <see cref="Claim"/> to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithClaim(Claim claim);

        /// <summary>
        /// Adds claims to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claims">Collection of <see cref="Claim"/> to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithClaims(IEnumerable<Claim> claims);

        /// <summary>
        /// Adds claims to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claims"><see cref="Claim"/> parameters to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithClaims(params Claim[] claims);

        /// <summary>
        /// Adds authentication type to the built <see cref="ClaimsPrincipal"/>. If such is not provided, "Passport" is used by default.
        /// </summary>
        /// <param name="authenticationType">Authentication type to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithAuthenticationType(string authenticationType);

        /// <summary>
        /// Adds role to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="role">Value of the role claim. Default claim type is <see cref="ClaimTypes.Role"/>.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder InRole(string role);

        /// <summary>
        /// Adds roles to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="roles">Collection of role names to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder InRoles(IEnumerable<string> roles);

        /// <summary>
        /// Adds roles to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="roles">Role name parameters to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder InRoles(params string[] roles);

        /// <summary>
        /// Adds <see cref="IIdentity"/> to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="identity"><see cref="IIdentity"/> to add.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithIdentity(IIdentity identity);

        /// <summary>
        /// Adds <see cref="IIdentity"/> to the built <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claimsIdentityBuilder">Builder for creating mocked <see cref="IIdentity"/>.</param>
        /// <returns>The same <see cref="IAndWithClaimsPrincipalBuilder"/>.</returns>
        IAndWithClaimsPrincipalBuilder WithIdentity(Action<IWithClaimsIdentityBuilder> claimsIdentityBuilder);
    }
}
