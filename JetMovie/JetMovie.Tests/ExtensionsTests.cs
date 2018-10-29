using System;
using JetMovie.Helpers;
using FluentAssertions;
using Xunit;

namespace JetMovie.Tests
{
    public class ExtensionsTests
    {
        [Fact]
        public void ToUnixDate()
        {
            var result = new DateTime(1970, 1, 1).AddSeconds(100).ToUnixDate();
            result.Should().Be(100);
        }
    }
}
