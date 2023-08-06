// Copyright (c) Brian Rogers. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Digits.Tests;

public sealed class SolverTest
{
    [Fact]
    public void NoSolution2()
    {
        IList<string> solutions = Solve(1000, new[] { 2, 3 });

        solutions.Should().BeEmpty();
    }

    [Fact]
    public void NoSolution3()
    {
        IList<string> solutions = Solve(1000, new[] { 3, 4, 5 });

        solutions.Should().BeEmpty();
    }

    [Fact]
    public void NoSolution4()
    {
        IList<string> solutions = Solve(1000, new[] { 1, 2, 3, 4 });

        solutions.Should().BeEmpty();
    }

    [Fact]
    public void DirectSolution2()
    {
        IList<string> solutions = Solve(3, new[] { 2, 3 });

        solutions.Should().HaveCount(1).And.ContainInOrder(
            string.Empty);
    }

    [Fact]
    public void OneSolution2()
    {
        IList<string> solutions = Solve(6, new[] { 2, 3 });

        solutions.Should().HaveCount(1).And.ContainInOrder(
            "[1]*[0]");
    }

    [Fact]
    public void FourSolutions3()
    {
        IList<string> solutions = Solve(7, new[] { 1, 2, 4 });

        solutions.Should().HaveCount(4).And.ContainInOrder(
            "[1]+[0];[1]+[0]",
            "[2]*[1];[1]-[0]",
            "[2]+[0];[1]+[0]",
            "[2]+[1];[1]+[0]");
    }

    [Fact]
    public void OneSolution3()
    {
        IList<string> solutions = Solve(575, new[] { 2, 23, 25 });

        solutions.Should().HaveCount(1).And.ContainInOrder(
            "[2]*[1]");
    }

    private static IList<string> Solve(int target, int[] numbers)
    {
        var solver = new Solver(target, new Board(numbers));
        var results = new List<IList<Move>>();

        solver.Solve(results.Add);

        var solutions = results.Select(ms => string.Join(';', ms)).ToList();
        solutions.Sort();
        return solutions;
    }
}
