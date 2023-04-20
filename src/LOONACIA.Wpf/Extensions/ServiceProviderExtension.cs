using System;
using System.Linq;

namespace LOONACIA.Wpf;
internal static class ServiceProviderExtension
{
	public static T CreateInstanceForUnregisteredType<T>(this IServiceProvider serviceProvider, bool isScoped = false)
	{
		var constructor = typeof(T).GetConstructors().SingleOrDefault() ?? throw new InvalidOperationException($"{typeof(T)} has more than one constructor.");
		var parameters = constructor.GetParameters();
		if (parameters is not { Length: > 0 })
		{
			return Activator.CreateInstance<T>();
		}

		var resolvedParameters = isScoped
			? parameters.Select(parameter => serviceProvider.GetService(parameter.ParameterType)).ToArray()
			: parameters.Select(parameter => parameter.ParameterType == typeof(IServiceProvider) ? serviceProvider : serviceProvider.GetService(parameter.ParameterType)).ToArray();

		return Activator.CreateInstance(typeof(T), resolvedParameters) is T instance
			? instance
			: throw new InvalidOperationException($"Failed to create instance of {typeof(T)}.");
	}
}
