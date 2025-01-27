namespace Rules.Framework.Evaluation.ValueEvaluation
{
    using System;
    using System.Collections.Generic;
    using Rules.Framework.Core;

    internal class OperatorEvalStrategyFactory : IOperatorEvalStrategyFactory
    {
        private readonly IDictionary<Operators, IOperatorEvalStrategy> strategies;

        public OperatorEvalStrategyFactory()
        {
            this.strategies = new Dictionary<Operators, IOperatorEvalStrategy>()
            {
                { Operators.Equal, new EqualOperatorEvalStrategy() },
                { Operators.NotEqual, new NotEqualOperatorEvalStrategy() },
                { Operators.GreaterThan, new GreaterThanOperatorEvalStrategy() },
                { Operators.GreaterThanOrEqual, new GreaterThanOrEqualOperatorEvalStrategy() },
                { Operators.LesserThan, new LesserThanOperatorEvalStrategy() },
                { Operators.LesserThanOrEqual, new LesserThanOrEqualOperatorEvalStrategy() }
            };
        }

        public IOperatorEvalStrategy GetOperatorEvalStrategy(Operators @operator)
        {
            if (this.strategies.TryGetValue(@operator, out IOperatorEvalStrategy operatorEvalStrategy))
            {
                return operatorEvalStrategy;
            }

            throw new NotSupportedException($"Operator evaluation is not supported for operator '{@operator}'.");
        }
    }
}