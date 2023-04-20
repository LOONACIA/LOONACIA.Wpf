using System;
using System.Windows.Input;

namespace LOONACIA.Wpf.Example.Input;
internal class SimpleRelayCommand : ICommand
{
	private readonly Action _action;

	private readonly Func<object?, bool>? _canExecute;

	public SimpleRelayCommand(Action action)
	{
		_action = action;
	}

	public SimpleRelayCommand(Action action, Func<object?, bool> canExecute)
	{
		_action = action;
		_canExecute = canExecute;
	}

	public event EventHandler? CanExecuteChanged
	{
		add { CommandManager.RequerySuggested += value; }
		remove { CommandManager.RequerySuggested -= value; }
	}

	public bool CanExecute(object? parameter)
	{
		if (_canExecute != null)
		{
			return _canExecute(parameter);
		}

		return true;
	}

	public void Execute(object? parameter)
	{
		_action?.Invoke();
	}
}
