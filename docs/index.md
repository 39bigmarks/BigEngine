# hello world

Test code block:
```csharp
// test code block

using System;

void Main() {
	Console.WriteLine("What is your name?");
	string name = Console.ReadLine();
	
	if(!String.IsNullOrWhiteSpace(name)) {
		Console.WriteLine($"Hello, ${name}!");
		Console.ReadKey();
	} else {
		Console.WriteLine($"I highly doubt you don't have a name, son.");
		Main();
	}
}
```
