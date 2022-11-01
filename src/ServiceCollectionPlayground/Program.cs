// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;

public static class Program {
    public static void Main (string [] args)
    {
        var services = new ServiceCollection();
        services.AddSingleton<IMyLog,MyConsoleLogger>();
        //services.AddSingleton<IFoo,FooForDemo>(provider => new FooForDemo(provider));
        services.AddSingleton<IFoo,FooForDemo>();

        var provider = services.BuildServiceProvider();

        IFoo demoFoo = provider.GetService<IFoo>()!;

        demoFoo.Bar();
    }
}

interface IMyLog {
    void Information(string messageToLog);
}

class MyConsoleLogger : IMyLog
{
    public void Information(string messageToLog) => System.Console.WriteLine(messageToLog);
}

interface IFoo
{
    void Bar();
}

class FooForDemo : IFoo
{
    private IServiceProvider _provider;

    public FooForDemo(IServiceProvider provider) => _provider = provider;

    public void Bar()
    {
        _provider.GetService<IMyLog>()?.Information(nameof(Bar));
    }
}