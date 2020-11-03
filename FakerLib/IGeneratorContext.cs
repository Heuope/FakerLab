using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public interface IGeneratorContext
    {
        object Generate(Type t);
    }
}
