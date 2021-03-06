using System;
using System.Collections;
using NSubstitute.Core.Arguments;
using NSubstitute.Specs.Infrastructure;
using NUnit.Framework;

namespace NSubstitute.Specs.Arguments
{
    public class ArgumentIsAnythingSpecificationSpecs : StaticConcern
    {
        [Test]
        public void Match_when_supplied_argument_type_matches_specified_type()
        {
            var anyStringSpec = new ArgumentIsAnythingSpecification(typeof(string));
            Assert.True(anyStringSpec.IsSatisfiedBy("asdf"));
            Assert.False(anyStringSpec.IsSatisfiedBy(123));
        }

        [Test]
        public void Match_when_supplied_argument_type_is_assignable_to_specified_type()
        {
            var anyEnumerableSpec = new ArgumentIsAnythingSpecification(typeof(IEnumerable));
            Assert.True(anyEnumerableSpec.IsSatisfiedBy(new [] {"string array is assignable to IEnumerable"}));
            Assert.False(anyEnumerableSpec.IsSatisfiedBy(new object()));
        }

        [Test]
        public void Match_when_supplied_argument_is_null()
        {
            var anySpec = new ArgumentIsAnythingSpecification(typeof(string));
            Assert.True(anySpec.IsSatisfiedBy(null));
        }

        [Test]
        public void Match_specified_int_type_passed_by_reference_when_compatible_type_passed_in()
        {
            var anyRefToIntSpec = new ArgumentIsAnythingSpecification(typeof(int).MakeByRefType());

            Assert.True(anyRefToIntSpec.IsSatisfiedBy(4));
            Assert.True(anyRefToIntSpec.IsSatisfiedBy(null));
            Assert.False(anyRefToIntSpec.IsSatisfiedBy(new object()));
        }

        [Test]
        public void Match_specified_object_type_passed_by_reference_when_compatible_type_passed_in()
        {
            var anyRefToIntSpec = new ArgumentIsAnythingSpecification(typeof(object).MakeByRefType());

            Assert.True(anyRefToIntSpec.IsSatisfiedBy(4));
            Assert.True(anyRefToIntSpec.IsSatisfiedBy(null));
            Assert.True(anyRefToIntSpec.IsSatisfiedBy(new object()));
        }
    }
}