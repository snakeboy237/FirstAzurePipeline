﻿using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Xunit;

[assembly: ApplicationPart("MyTested.AspNetCore.Mvc.Test.Setups")]

[assembly: AssemblyProduct("MyTested.AspNetCore.Mvc.Versioning.Test")]
[assembly: ComVisible(false)]

[assembly: CollectionBehavior(DisableTestParallelization = true)]
