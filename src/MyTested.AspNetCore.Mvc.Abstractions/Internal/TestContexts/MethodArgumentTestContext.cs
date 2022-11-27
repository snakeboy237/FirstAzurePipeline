﻿namespace MyTested.AspNetCore.Mvc.Internal.TestContexts
{
    using System;

    /// <summary>
    /// Method argument information containing name, type and value for a method parameter.
    /// </summary>
    public class MethodArgumentTestContext
    {
        /// <summary>
        /// Gets or sets the name of the argument.
        /// </summary>
        /// <value>Argument's name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the argument.
        /// </summary>
        /// <value>Argument's type.</value>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the argument.
        /// </summary>
        /// <value>Argument's value.</value>
        public object Value { get; set; }
    }
}
