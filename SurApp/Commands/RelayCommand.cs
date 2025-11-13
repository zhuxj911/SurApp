using System;
using System.Windows.Input;

namespace SurApp.Commands;

/// <summary>
/// 取名为RelayCommand，是因为与CommunityToolkit.Mvvm.Input中的RelayCommand相似。
/// </summary>
public class RelayCommand : ICommand
{
    //利用 CommandManager.RequerySuggested 全局事件，实现“懒刷新”(每 200ms左右轮询一次所有命令)
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }


    private readonly Action<object?> _execute;
	private readonly Func<object?, bool>? _canExecute;
	//private readonly Predicate<object?> _canExecute;

	public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
	{
		_execute = execute;
		_canExecute = canExecute;
	}

    public bool CanExecute(object? parameter)
	{
        return _canExecute?.Invoke(parameter) ?? true;
    }

	public void Execute(object? parameter)
	{
		_execute(parameter);
	}
}
