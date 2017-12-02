#!/bin/bash

cd Tests
dotnet test --verbosity=m --filter=FullyQualifiedName!=Tests.Main.SampleTests.ShouldFail
exit $? 