using System;
using System.Collections.Generic;

namespace callculator
{
    public class Evaluator
    {
        readonly Queue<Token> postfixExpression;

        public Evaluator(Queue<Token> postfixExpression)
        {
            this.postfixExpression = postfixExpression;
        }

        public double Evaluate()
        {
            Stack<Token> evaluationStack = new Stack<Token>();
            foreach (var token in postfixExpression)
            {
                if (token.Type == TokenType.Number)
                {
                    evaluationStack.Push(token);
                }
                else if (token.Type == TokenType.Operator)
                {
                    double result;
                    Token temp = evaluationStack.Pop();
                    Operator op = new Operator(token);
                    if (op.Op.Value == "#" || op.Op.Value == "@")
                    {
                        result = op.Operation(double.Parse(temp.Value));
                    }
                    else
                    {
                        string val1 = evaluationStack.Peek().Value;
                        result = op.Operation(Double.Parse(evaluationStack.Pop().Value), Double.Parse(temp.Value));
                    }
                    evaluationStack.Push(new Token(result.ToString()));
                }
                else if (token.Type == TokenType.Function)
                {
                    double result = Function.Call(token.Value, evaluationStack);
                    evaluationStack.Push(new Token(result.ToString()));
                }
            }

            return Double.Parse(evaluationStack.Pop().Value);
        }
    }
}