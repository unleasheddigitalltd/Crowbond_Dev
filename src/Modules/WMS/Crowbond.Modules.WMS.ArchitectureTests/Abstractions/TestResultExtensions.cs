﻿using FluentAssertions;
using NetArchTest.Rules;

namespace Crowbond.Modules.WMS.ArchitectureTests.Abstractions;
internal static class TestResultExtensions
{
    internal static void ShouldBeSuccessful(this TestResult testResult)
    {
        testResult.FailingTypes?.Should().BeEmpty();
    }
}
