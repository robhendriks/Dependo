# Dependo

C# dependency resolution library.

## Features

* Dependency sorting.
* Dependency graphing.
* Cyclic dependency detection.

## Basic Usage

Create concrete implementations for `DependencyBase<T, TKey>` and `DependencyContainerBase<T, TKey>`. The `TKey` type parameter is used to determine the type that'll be used for each dependency's unique key.

```csharp
class MyDependency : DependencyBase<MyDependency, string>
{
    public MyDependency(string key) : base(key)
    {
    }
}
```

```csharp
class MyContainer : DependencyContainerBase<MyDependency, string>
{
    public MyContainer()
    {
    }
}
```

Create an instance of `MyContainer` and register the dependencies.

```csharp
var container = new ItemContainer()
	.RegisterDependency(new Item("A")
		.DependsOn("B"))
	.RegisterDependency(new Item("B")
		.DependsOn("D"))
	.RegisterDependency(new Item("C")
		.DependsOn("B"))
	.RegisterDependency(new Item("D"))
	.RegisterDependency(new Item("E")
		.DependsOn("C")
		.DependsOn("A"))
	.RegisterDependency(new Item("F"))
	.RegisterDependency(new Item("G")
		.DependsOn("F"));
```

Resolve the dependencies, this will return all roots (nodes without incoming edges).

```csharp
var roots = container.ResolveDependencies();
```

Now you can do whatever and even walk the dependency tree.

```csharp
foreach (var root in roots)
{
    root.Walk((info, plugin) =>
    {
        // Do something
    });
}
```

## Manual Installation

* Clone the repository.
* Build the project using Visual Studio 2015 or later.
* Copy _Dependo.dll_ to your main project and reference them.
