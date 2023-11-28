using System;
using System.Windows.Input;

namespace SurApp.Commands;

/// <summary>
/// 这样写，这个Command还是一个通用的Command， 与逻辑业务没有多大关系
/// 如果这样写，是否可以直接用 RoutedCommand 呢？
/// </summary>
public class MyCommand : ICommand
{

	private readonly Action<object?> _execute;

	//private readonly Func<object?, bool> _canExecute;
	private readonly Predicate<object?> _canExecute;


	public MyCommand(Action<object?> execute, Predicate<object?> canExecute)
	{
		_execute = execute;
		_canExecute = canExecute;
	}

	// 这个为什么没有用到？
	// 这是在网上找到的写法
	public event EventHandler? CanExecuteChanged
	{
		add
		{
			CommandManager.RequerySuggested += value;
		}
		remove
		{
			CommandManager.RequerySuggested -= value;
		}
	}

	public bool CanExecute(object? parameter)
	{
		return _canExecute(parameter);
	}

	public void Execute(object? parameter)
	{
		_execute(parameter);
	}
}
