#!/bin/sh -e

TEST_PROJECT=./src/IxMilia.LinearAlgebra.Test/IxMilia.LinearAlgebra.Test.csproj
dotnet restore $TEST_PROJECT
dotnet test $TEST_PROJECT
