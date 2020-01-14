using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFight_With_ASPNetCore
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton, IOperationSingletonInstance
    {
        readonly Guid _guid;
        public Operation() : this(Guid.NewGuid())
        {

        }
        public Operation(Guid guid)
        {
            _guid = guid;
        }

        public Guid OperationId => _guid;
    }
}
