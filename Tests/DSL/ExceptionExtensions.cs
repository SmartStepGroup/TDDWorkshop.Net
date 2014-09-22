using System;
using NUnit.Framework;

namespace Tests.DSL
{

    public static class ExceptionExtensions
    {
        public static void withMessage(this InvalidOperationException ex, string message)
        {
            Assert.AreEqual(message, ex.Message);
        }
    }

}
