// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Ignored")]
[assembly: SuppressMessage("Documentation", "CA1200:Avoid using cref tags with a prefix", Justification = "Ignored")]
[assembly: SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection", Scope = "type", Target = "~T:WinMemoryCleaner.ComputerService")]
[assembly: SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection", Scope = "type", Target = "~T:WinMemoryCleaner.NotificationService")]
[assembly: SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Dependency Injection", Scope = "type", Target = "~T:WinMemoryCleaner.ViewModelLocator")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Ignored")]
[assembly: SuppressMessage("Reliability", "CA9998:Migrate from FxCop analyzers to .NET analyzers", Justification = "Legacy .NET Framework version")]
