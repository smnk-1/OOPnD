using Hwdtech;
namespace StarWars.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class RegisterIoCDependencyAdaptersGenerate : Hwdtech.ICommand
{
    public void Execute()
    {
        IoC.Resolve<object>("IoC.Register", "Adapters.Generate",
        (Func<object[], object>)(args =>
        {
            var adaptee = args[0];
            var targetType = (Type)args[1];

            if (targetType.IsInstanceOfType(adaptee))
            {
                return adaptee;
            }

            var adapterKey = $"{adaptee.GetType().FullName}->{targetType.FullName}";
            var adapters = IoC.Resolve<IDictionary<string, IAdapter>>("Adapters.Dictionary");

            if (adapters.TryGetValue(adapterKey, out var existingAdapter))
            {
                return existingAdapter.Adapt(adaptee);
            }

            var template = IoC.Resolve<string>("Adapters.GetTemplate", targetType);
            var sourceCode = IoC.Resolve<string>("Adapters.GenerateAdapterCode", adaptee.GetType(), targetType, template);
            var assembly = IoC.Resolve<Assembly>("Adapters.Compile", adaptee.GetType().Assembly, sourceCode);

            var adapterType = assembly.GetTypes().First(t => targetType.IsAssignableFrom(t));
            var adapter = (IAdapter)Activator.CreateInstance(adapterType)!;

            adapters[adapterKey] = adapter;

            return adapter.Adapt(adaptee);

        }));
    }
}
