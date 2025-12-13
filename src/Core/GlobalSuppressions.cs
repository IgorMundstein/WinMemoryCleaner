// This file is used by Code Analysis to maintain SuppressMessage attributes that are applied to this project.

using System.Diagnostics.CodeAnalysis;

// General suppressions
[assembly: SuppressMessage("Design", "CA1030:Use events where appropriate", Justification = "Ignored", Scope = "type", Target = "~T:WinMemoryCleaner.ObservableObject")]
[assembly: SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Ignored")]
[assembly: SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "Ignored")]
[assembly: SuppressMessage("Design", "CA1040:Avoid empty interfaces", Justification = "Ignored")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Ignored", Scope = "type", Target = "~T:WinMemoryCleaner.TrayIconContextMenuControl.ToolStripRenderer")]
[assembly: SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Ignored", Scope = "type", Target = "~T:WinMemoryCleaner.ExtensionMethods")]
[assembly: SuppressMessage("Documentation", "CA1200:Avoid using cref tags with a prefix", Justification = "Ignored")]
[assembly: SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "Ignored", Scope = "type", Target = "~T:WinMemoryCleaner.Localizer")]
[assembly: SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "Ignored", Scope = "type", Target = "~T:WinMemoryCleaner.Constants")]
[assembly: SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "Ignored", Scope = "type", Target = "~T:WinMemoryCleaner.Localizer")]
[assembly: SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "Ignored", Scope = "type", Target = "~T:WinMemoryCleaner.Structs")]
[assembly: SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "Ignored", Scope = "type", Target = "~T:WinMemoryCleaner.Structs")]
[assembly: SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Ignored")]
[assembly: SuppressMessage("Reliability", "CA9998:Migrate from FxCop analyzers to .NET analyzers", Justification = "Legacy .NET Framework version")]

// Test suppressions
[assembly: SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "Test classes implement IDisposable through IClassFixture/ICollectionFixture when needed", Scope = "type", Target = "~T:WinMemoryCleaner.ServiceTests+ComputerServiceTests")]
[assembly: SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "Test classes implement IDisposable through IClassFixture/ICollectionFixture when needed", Scope = "type", Target = "~T:WinMemoryCleaner.ServiceTests+HotkeyServiceTests")]
[assembly: SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "Test classes implement IDisposable through IClassFixture/ICollectionFixture when needed", Scope = "type", Target = "~T:WinMemoryCleaner.ServiceTests+NotificationServiceTests")]
[assembly: SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "Test classes implement IDisposable through IClassFixture/ICollectionFixture when needed", Scope = "type", Target = "~T:WinMemoryCleaner.ViewModelTests+MainViewModelTests")]
[assembly: SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "Test classes implement IDisposable through IClassFixture/ICollectionFixture when needed", Scope = "type", Target = "~T:WinMemoryCleaner.ViewModelTests+MessageViewModelTests")]
[assembly: SuppressMessage("Design", "CA1001:Types that own disposable fields should be disposable", Justification = "Test classes implement IDisposable through IClassFixture/ICollectionFixture when needed", Scope = "type", Target = "~T:WinMemoryCleaner.ViewModelTests+DonationViewModelTests")]
[assembly: SuppressMessage("Design", "CA1052:Static holder types should be static or NotInheritable", Justification = "Test container classes for organizing nested test classes", Scope = "type", Target = "~T:WinMemoryCleaner.ServiceTests")]
[assembly: SuppressMessage("Design", "CA1052:Static holder types should be static or NotInheritable", Justification = "Test container classes for organizing nested test classes", Scope = "type", Target = "~T:WinMemoryCleaner.ViewModelTests")]
[assembly: SuppressMessage("Design", "CA1812:Avoid uninstantiated internal classes", Justification = "Test helper classes", Scope = "namespaceanddescendants", Target = "~N:WinMemoryCleaner")]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Test method names use underscores for readability following best naming conventions", Scope = "namespaceanddescendants", Target = "~N:WinMemoryCleaner")]