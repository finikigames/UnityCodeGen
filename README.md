# Unity CodeGen
Code Generation Library for Unity Editor

<img src="https://github.com/AnnulusGames/UnityCodeGen/blob/main/Assets/UnityCodeGen/Documentation~/Header.png" width="800">

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE)

[日本語版READMEはこちら](README_JP.md)

## Overview
Unity CodeGen is a library that streamlines code generation on the Unity editor.
By defining your own Generator that inherits from ICodeGenerator, you can generate codeautomatically.

### Features
* Code generation can be implemented smoothly
* Code can be generated automatically when compiling

## Setup

### Requirement
* Unity 2020.1 or higher

### Install
1. Open the Package Manager from Window > Package Manager
2. "+" button > Add package from git URL
3. Enter the following to install
   * https://github.com/AnnulusGames/UnityCodeGen.git?path=/Assets/UnityCodeGen


or open Packages/manifest.json and add the following to the dependencies block.

```json
{
    "dependencies": {
        "com.annulusgames.unity-codegen": "https://github.com/AnnulusGames/UnityCodeGen.git?path=/Assets/UnityCodeGen"
    }
}
```

## Usage
Create a .cs file under any Editor folder and implement a class that inherits ICodeGenerator.
Below is a Generator that generates an empty Sample class.

```cs
using UnityCodeGen;

[Generator] // Add GeneratorAttribute
public class SampleGenerator : ICodeGenerator // Inherits ICodeGenerator
{
    public void Execute(GeneratorContext context) // Implement Execute method
    {
        context.AddCode("Sample.Generated.cs", // File name
@"// <auto-generated/>
namespace SampleNamespace.Generated
{
    public class Sample
    {

    }
}"
        );
    }
}
```

Back in the editor, select Tools/UnityCodeGen/Generate to generate code.

<img src="https://github.com/AnnulusGames/UnityCodeGen/blob/main/Assets/UnityCodeGen/Documentation~/img1.png" width="400">

Generated code is placed in Assets/UnityCodeGen.Generated.

```cs
// <auto-generated/>
namespace SampleNamespace.Generated
{
    public class Sample
    {

    }
}
```

## Specify Output Path
By using GeneratorContext.OverrideFolderPath, you can specify the output folder path.

```cs
public void Execute(GeneratorContext context)
{
    context.OverrideFolderPath("Assets/YourFolder/Generated");
    ...
}
```

## Auto-generate on Compile
It is possible to automate code generation by checking Tools/UnityCodeGen/Auto-generate on Compile.

<img src="https://github.com/AnnulusGames/UnityCodeGen/blob/main/Assets/UnityCodeGen/Documentation~/img2.png" width="400">

When Auto-generate on Compile is on, it will be automatically generated at the end of compilation and will be recompiled only if there is any change in the generated code.

## Unity Code Gen Utility
By using the UnityCodeGenUtility class, it is also possible to operate from your script.

``` cs
// get default output folder path
var path = UnityCodeGenUtility.defaultFolderPath;

// run generation
UnityCodeGenUtility.Generate();
```

## Advanced

### Perform processing on classes with specific attributes added
By using Unity's TypeCache, classes with specific attributes can be retrieved all at once.
This allows you to generate code for classes with specific attributes.

As an example, let's generate code that overrides ToString to display all public fields for a class with the AddToStringAttribute.

First, define the attributes used to identify the Generator.

```cs
using System;

public class AddToStringAttribute : Attribute { }
```

Next, create a Generator. Note that this file should be placed under any Editor folder.

```cs
using System.Linq;
using UnityEditor;
using UnityCodeGen;

[Generator]
public class AddToStringGenerator : ICodeGenerator
{
    public void Execute(GeneratorContext context)
    {
        var types = TypeCache.GetTypesWithAttribute<AddToStringAttribute>();
        foreach (var t in types)
        {
            var publicFields = t.GetFields()
                .Where(x => x.IsPublic && !x.IsStatic)
                .Select(x => $"{x.Name}:{{{x.Name}}}");

            var toString = string.Join(", ", publicFields);
            var code = 
$@"// <auto-generated/>
partial class {t.Name}
{{
    public override string ToString()
    {{
        return $""{toString}"";
    }}
}}";
            context.AddCode($"{t.FullName}.AddToString.Generated.cs", code);
        }
    }
}
```

You are now ready to generate.
Create a class and add AddToStringAttribute.

```cs
using UnityEngine;

[AddToString]
public partial class FooClass
{
    public int foo;
    public string bar;
    public Vector3 baz;
}
```

Executing Generate will generate FooClass.AddToString.Generated.cs under the UnityCodeGen.Generated folder.

```cs
// <auto-generated/>
partial class FooClass
{
    public override string ToString()
    {
        return $"foo:{foo}, bar:{bar}, baz:{baz}";
    }
}
```

## Samples

<img src="https://github.com/AnnulusGames/UnityCodeGen/blob/main/Assets/UnityCodeGen/Documentation~/img3.png" width="400">

Unity CodeGen samples are available from the Package Manager.
Below is a list of samples included in the package.

|  Sample Name  |  Description  |
| ---------- | ------ |
|  Tags & Layers |  A sample that generates a class that manages Tags, Layers and Sorting Layers with constants. |


## License

[MIT License](LICENSE)