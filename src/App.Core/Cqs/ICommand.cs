

namespace App.Core.Cqs;

public interface ICommand { }
public interface ICommand<out TResult> { }
