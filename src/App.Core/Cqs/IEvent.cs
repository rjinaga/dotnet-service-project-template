namespace App.Core.Cqs;

public interface IEvent<out T> where T : class
{
    T Arg { get; }
}
