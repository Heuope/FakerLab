using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib
{
    public interface IGenerator
    {
        object Generate(Type targetType, IGeneratorContext generatorContext);

        bool CanGenerate(Type type);
    }
}
