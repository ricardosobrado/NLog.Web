﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NLog.Extensions.Logging;

namespace NLog.Web.AspNetCore
{
    /// <summary>
    /// Options for ASP.NET Core and NLog
    /// </summary>
    public sealed class NLogAspNetCoreOptions : NLogProviderOptions
    {
        /// <summary>
        /// Register the HttpContextAccessor when not yet registed. Default <c>true</c>
        /// </summary>
        /// <remarks>needed for various layout renderers</remarks>
        [DefaultValue(true)]
        public bool RegisterHttpContextAccessor { get; set; } = true;

        /// <summary>
        /// The default options
        /// </summary>
        public static NLogAspNetCoreOptions Default { get; } = new NLogAspNetCoreOptions();
    }
}
