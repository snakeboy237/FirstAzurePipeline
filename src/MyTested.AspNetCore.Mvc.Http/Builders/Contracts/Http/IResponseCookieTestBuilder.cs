﻿namespace MyTested.AspNetCore.Mvc.Builders.Contracts.Http
{
    using System;
    using Microsoft.Net.Http.Headers;

    /// <summary>
    /// Used for testing <see cref="Microsoft.AspNetCore.Http.HttpResponse"/> cookie.
    /// </summary>
    public interface IResponseCookieTestBuilder
    {
        /// <summary>
        /// Sets the expected <see cref="SetCookieHeaderValue.Name"/> of the tested cookie.
        /// </summary>
        /// <param name="name">Name to set on the cookie.</param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithName(string name);

        /// <summary>
        /// Tests whether the <see cref="SetCookieHeaderValue.Value"/> property is the same as the provided one.
        /// </summary>
        /// <param name="value">Expected value of the cookie.</param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithValue(string value);

        /// <summary>
        /// Tests whether the <see cref="SetCookieHeaderValue.Value"/> passes the provided assertions.
        /// </summary>
        /// <param name="assertions">Action containing assertions for the cookie value.</param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithValue(Action<string> assertions);

        /// <summary>
        /// Tests whether the <see cref="SetCookieHeaderValue.Value"/> passes the provided predicate.
        /// </summary>
        /// <param name="predicate">Predicate testing the cookie value.</param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithValue(Func<string, bool> predicate);

        /// <summary>
        /// Tests whether the <see cref="SetCookieHeaderValue.Domain"/> property is the same as provided one.
        /// </summary>
        /// <param name="domain">Expected domain of the cookie.</param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithDomain(string domain);

        /// <summary>
        /// Tests whether the <see cref="SetCookieHeaderValue.Expires"/> property is the same as provided one.
        /// </summary>
        /// <param name="expiration">Expected expiration date time offset of the cookie.</param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithExpiration(DateTimeOffset? expiration);

        /// <summary>
        /// Tests whether the <see cref="SetCookieHeaderValue.HttpOnly"/> property is the same as provided one.
        /// </summary>
        /// <param name="httpOnly">
        /// Expected <see cref="SetCookieHeaderValue.HttpOnly"/> property of the cookie.
        /// </param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithHttpOnly(bool httpOnly);

        /// <summary>
        /// Tests whether the <see cref="SetCookieHeaderValue.MaxAge"/> property is the same as provided one.
        /// </summary>
        /// <param name="maxAge">Expected <see cref="SetCookieHeaderValue.MaxAge"/> property of the cookie.</param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithMaxAge(TimeSpan? maxAge);

        /// <summary>
        /// Tests whether the <see cref="SetCookieHeaderValue.Path"/> property is the same as provided one.
        /// </summary>
        /// <param name="path">Expected <see cref="SetCookieHeaderValue.Path"/> property of the cookie.</param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithPath(string path);

        /// <summary>
        /// Tests whether the <see cref="SetCookieHeaderValue.SameSite"/> property is the same as provided one.
        /// </summary>
        /// <param name="sameSite">
        /// Expected <see cref="SetCookieHeaderValue.SameSite"/> property of the cookie.
        /// </param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithSameSite(SameSiteMode sameSite);

        /// <summary>
        /// Tests whether the <see cref="SetCookieHeaderValue.Secure"/> property is the same as provided one.
        /// </summary>
        /// <param name="security">Expected <see cref="SetCookieHeaderValue.Secure"/> property of the cookie.</param>
        /// <returns>The same <see cref="IAndResponseCookieTestBuilder"/>.</returns>
        IAndResponseCookieTestBuilder WithSecurity(bool security);
    }
}
