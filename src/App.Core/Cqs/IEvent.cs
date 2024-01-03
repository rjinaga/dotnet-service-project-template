namespace App.Core.Cqs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IEvent<out T> where T : class
{
    T Arg { get; }
}
