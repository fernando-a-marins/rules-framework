namespace Rules.Framework.Builder
{
    using System;
    using Rules.Framework.Core;
    using Rules.Framework.Core.ConditionNodes;

    internal class ValueConditionNodeBuilder<TConditionType> : IValueConditionNodeBuilder<TConditionType>
    {
        private readonly TConditionType conditionType;

        public ValueConditionNodeBuilder(TConditionType conditionType)
        {
            this.conditionType = conditionType;
        }

        public IValueConditionNodeBuilder<TConditionType, T> OfDataType<T>()
            => new ValueConditionNodeBuilder<TConditionType, T>(this.conditionType);
    }

    internal class ValueConditionNodeBuilder<TConditionType, TDataType> : IValueConditionNodeBuilder<TConditionType, TDataType>
    {
        private readonly TConditionType conditionType;
        private Operators comparisonOperator;
        private TDataType operand;

        public ValueConditionNodeBuilder(TConditionType conditionType)
        {
            this.conditionType = conditionType;
        }

        public IValueConditionNodeBuilder<TConditionType, TDataType> WithComparisonOperator(Operators comparisonOperator)
        {
            this.comparisonOperator = comparisonOperator;

            return this;
        }

        public IValueConditionNodeBuilder<TConditionType, TDataType> SetOperand(TDataType value)
        {
            this.operand = value;

            return this;
        }

        public IValueConditionNode<TConditionType> Build()
        {
            switch (this.operand)
            {
                case decimal decimalOperand:
                    return new DecimalConditionNode<TConditionType>(this.conditionType, this.comparisonOperator, decimalOperand);

                case int integerOperand:
                    return new IntegerConditionNode<TConditionType>(this.conditionType, this.comparisonOperator, integerOperand);

                case bool booleanOperand:
                    return new BooleanConditionNode<TConditionType>(this.conditionType, this.comparisonOperator, booleanOperand);

                case string stringOperand:
                    return new StringConditionNode<TConditionType>(this.conditionType, this.comparisonOperator, stringOperand);

                default:
                    throw new NotSupportedException($"The data type is not supported: {typeof(TDataType).FullName}.");
            }
        }
    }
}