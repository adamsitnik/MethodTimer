﻿using System;
using System.IO;
using NUnit.Framework;

[TestFixture]
public class AssemblyWithAttributeOnModuleTests
{
    AssemblyWeaver assemblyWeaver;

    public AssemblyWithAttributeOnModuleTests()
    {
		var assemblyPath = Path.GetFullPath(@"..\..\..\AssemblyToProcess\bin\DebugWithAttributeOnModule\AssemblyToProcess.dll");
        assemblyWeaver = new AssemblyWeaver(assemblyPath);
    }

    [Test]
    public void ClassWithNoAttribute()
    {
        var message = DebugRunner.CaptureDebug(() =>
            {
                var type = assemblyWeaver.Assembly.GetType("ClassWithNoAttribute");
                var instance = (dynamic) Activator.CreateInstance(type);
                instance.Method();
            });
        Assert.IsTrue(message.StartsWith("ClassWithNoAttribute.Method "));
    }


#if(DEBUG)
    [Test]
    public void PeVerify()
    {
        Verifier.Verify(assemblyWeaver.Assembly.CodeBase.Remove(0, 8));
    }
#endif

}