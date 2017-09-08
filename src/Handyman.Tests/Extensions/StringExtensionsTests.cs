﻿using System;
using Handyman.Extensions;
using Shouldly;
using Xunit;

namespace Handyman.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void ShouldJoinStrings()
        {
            new[] { "1", "2", "3" }.Join().ShouldBe("123");
            new[] { "join", "multiple", "strings" }.Join(" ").ShouldBe("join multiple strings");
        }

        [Fact]
        public void ShouldCheckIfStringContainsValue()
        {
            "Hello World".Contains("lo wo", StringComparison.InvariantCulture).ShouldBe(false);
            "Hello World".Contains("lo wo", StringComparison.InvariantCultureIgnoreCase).ShouldBe(true);
        }

        [Fact]
        public void ShouldCheckIfStringIsNull()
        {
            ((string)null).IsNull().ShouldBe(true);
            "".IsNull().ShouldBe(false);
        }

        [Fact]
        public void ShouldCheckIfStringIsEmpty()
        {
            ((string)null).IsEmpty().ShouldBe(false);
            "".IsEmpty().ShouldBe(true);
            " ".IsEmpty().ShouldBe(false);
            "x".IsEmpty().ShouldBe(false);
        }

        [Fact]
        public void ShouldCheckIfStringIsWhiteSpace()
        {
            ((string)null).IsWhiteSpace().ShouldBe(false);
            "".IsWhiteSpace().ShouldBe(true);
            " ".IsWhiteSpace().ShouldBe(true);
            "x".IsWhiteSpace().ShouldBe(false);
        }

        [Fact]
        public void ShouldCheckIfStringIsNullOrEmpty()
        {
            ((string)null).IsNullOrEmpty().ShouldBe(true);
            "".IsNullOrEmpty().ShouldBe(true);
            " ".IsNullOrEmpty().ShouldBe(false);
            "x".IsNullOrEmpty().ShouldBe(false);
        }

        [Fact]
        public void ShouldCheckIfStringIsNullOrWhiteSpace()
        {
            ((string)null).IsNullOrWhiteSpace().ShouldBe(true);
            "".IsNullOrWhiteSpace().ShouldBe(true);
            " ".IsNullOrWhiteSpace().ShouldBe(true);
            "x".IsNullOrWhiteSpace().ShouldBe(false);
        }

        [Fact]
        public void ShouldCheckIfStringIsNotNull()
        {
            ((string)null).IsNotNull().ShouldBe(false);
            "".IsNotNull().ShouldBe(true);
        }

        [Fact]
        public void ShouldCheckIfStringIsNotEmpty()
        {
            ((string)null).IsNotEmpty().ShouldBe(true);
            "".IsNotEmpty().ShouldBe(false);
            " ".IsNotEmpty().ShouldBe(true);
            "x".IsNotEmpty().ShouldBe(true);
        }

        [Fact]
        public void ShouldCheckIfStringIsNotWhiteSpace()
        {
            ((string)null).IsNotWhiteSpace().ShouldBe(true);
            "".IsNotWhiteSpace().ShouldBe(false);
            " ".IsNotWhiteSpace().ShouldBe(false);
            "x".IsNotWhiteSpace().ShouldBe(true);
        }

        [Fact]
        public void ShouldCheckIfStringIsNotNullOrEmpty()
        {
            ((string)null).IsNotNullOrEmpty().ShouldBe(false);
            "".IsNotNullOrEmpty().ShouldBe(false);
            " ".IsNotNullOrEmpty().ShouldBe(true);
            "x".IsNotNullOrEmpty().ShouldBe(true);
        }

        [Fact]
        public void ShouldCheckIfStringIsNotNullOrWhiteSpace()
        {
            ((string)null).IsNotNullOrWhiteSpace().ShouldBe(false);
            "".IsNotNullOrWhiteSpace().ShouldBe(false);
            " ".IsNotNullOrWhiteSpace().ShouldBe(false);
            "x".IsNotNullOrWhiteSpace().ShouldBe(true);
        }

        [Fact]
        public void ShouldGetSubstring()
        {
            "".SubstringSafe(1).ShouldBe(string.Empty);
            "".SubstringSafe(1, 1).ShouldBe(string.Empty);
            "Hello".SubstringSafe(1).ShouldBe("ello");
            "Hello".SubstringSafe(1, 1).ShouldBe("e");
        }

        [Fact]
        public void ShouldReverseTheString()
        {
            "Hello".Reverse().ShouldBe("olleH");
        }

        [Fact]
        public void ShouldConvertStringToEnumOrThrow()
        {
            Should.Throw<ArgumentException>(() => "yes".ToEnum<IgnoreCase>());
            "yes".ToEnum<IgnoreCase>(IgnoreCase.Yes).ShouldBe(IgnoreCase.Yes);
        }

        [Fact]
        public void ShouldConvertStringToEnumOrNull()
        {
            "yes".ToEnumOrNull<IgnoreCase>().ShouldBe(null);
            "yes".ToEnumOrNull<IgnoreCase>(IgnoreCase.Yes).ShouldBe(IgnoreCase.Yes);
        }

        [Fact]
        public void ShouldConvertStringToEnumOrDefault()
        {
            "yes".ToEnumOrDefault(IgnoreCase.No).ShouldBe(IgnoreCase.No);
            "yes".ToEnumOrDefault(() => IgnoreCase.No).ShouldBe(IgnoreCase.No);
        }

        [Fact]
        public void ShouldGetValueIfNull()
        {
            ((string)null).IfNull("value").ShouldBe("value");
            ((string)null).IfNull(() => "value").ShouldBe("value");
            "".IfNull("value").ShouldBe("");
            "".IfNull(() => "value").ShouldBe("");
            " ".IfNull("value").ShouldBe(" ");
            " ".IfNull(() => "value").ShouldBe(" ");
            "foobar".IfNull("value").ShouldBe("foobar");
            "foobar".IfNull(() => "value").ShouldBe("foobar");
        }

        [Fact]
        public void ShouldGetValueIfEmpty()
        {
            ((string)null).IfEmpty("value").ShouldBe(null);
            ((string)null).IfEmpty(() => "value").ShouldBe(null);
            "".IfEmpty("value").ShouldBe("value");
            "".IfEmpty(() => "value").ShouldBe("value");
            " ".IfEmpty("value").ShouldBe(" ");
            " ".IfEmpty(() => "value").ShouldBe(" ");
            "foobar".IfEmpty("value").ShouldBe("foobar");
            "foobar".IfEmpty(() => "value").ShouldBe("foobar");
        }

        [Fact]
        public void ShouldGetValueIfWhiteSpace()
        {
            ((string)null).IfWhiteSpace("value").ShouldBe(null);
            ((string)null).IfWhiteSpace(() => "value").ShouldBe(null);
            "".IfWhiteSpace("value").ShouldBe("value");
            "".IfWhiteSpace(() => "value").ShouldBe("value");
            " ".IfWhiteSpace("value").ShouldBe("value");
            " ".IfWhiteSpace(() => "value").ShouldBe("value");
            "foobar".IfWhiteSpace("value").ShouldBe("foobar");
            "foobar".IfWhiteSpace(() => "value").ShouldBe("foobar");
        }

        [Fact]
        public void ShouldGetValueIfNullOrEmpty()
        {
            ((string)null).IfNullOrEmpty("value").ShouldBe("value");
            ((string)null).IfNullOrEmpty(() => "value").ShouldBe("value");
            "".IfNullOrEmpty("value").ShouldBe("value");
            "".IfNullOrEmpty(() => "value").ShouldBe("value");
            " ".IfNullOrEmpty("value").ShouldBe(" ");
            " ".IfNullOrEmpty(() => "value").ShouldBe(" ");
            "foobar".IfNullOrEmpty("value").ShouldBe("foobar");
            "foobar".IfNullOrEmpty(() => "value").ShouldBe("foobar");
        }

        [Fact]
        public void ShouldGetValueIfNullOrWhiteSpace()
        {
            ((string)null).IfNullOrWhiteSpace("value").ShouldBe("value");
            ((string)null).IfNullOrWhiteSpace(() => "value").ShouldBe("value");
            "".IfNullOrWhiteSpace("value").ShouldBe("value");
            "".IfNullOrWhiteSpace(() => "value").ShouldBe("value");
            " ".IfNullOrWhiteSpace("value").ShouldBe("value");
            " ".IfNullOrWhiteSpace(() => "value").ShouldBe("value");
            "foobar".IfNullOrWhiteSpace("value").ShouldBe("foobar");
            "foobar".IfNullOrWhiteSpace(() => "value").ShouldBe("foobar");
        }

        [Fact]
        public void ShouldTryConvertStringToEnum()
        {
            int @int;
            Should.Throw<ArgumentException>(() => "one".TryToEnum(out @int));
            Should.Throw<ArgumentException>(() => "one".TryToEnum(IgnoreCase.Yes, out @int));

            // ReSharper disable once RedundantAssignment
            var number = Number.One;

            "one".TryToEnum(out number).ShouldBe(false);
            number.ShouldBe(Number.Zero);

            "one".TryToEnum(IgnoreCase.Yes, out number).ShouldBe(true);
            number.ShouldBe(Number.One);

            // ReSharper disable once RedundantAssignment
            number = Number.Zero;
            "1".TryToEnum(out number).ShouldBe(true);
            number.ShouldBe(Number.One);
        }

        private enum Number
        {
            Zero = 0,
            One = 1
        }
    }
}