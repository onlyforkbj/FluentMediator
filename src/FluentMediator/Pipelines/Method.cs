using System;

namespace FluentMediator.Pipelines
{
    public sealed class Method<TAction>
    {
        public Type HandlerType { get; }
        public TAction Action { get; }

        public Method(Type handlerType, TAction action)
        {
            HandlerType = handlerType;
            Action = action;
        }
    }
}