using System.Text;

namespace RomanCaclulatorLib
{
    // На ошибки и всякие юз кейс, когда передается не та строка, строка содержит одну скобку и т.д. тут проверок нет
    public sealed class Calculator : ICalculator
    {
        // "(MMMDCCXXIV - MMCCXXIX) * II"
        public string Evaluate(string input)
        {
            input = input?.Replace(" ", string.Empty);

            // тут ошибку возможно было бы славно, но этож тестовое..
            if (string.IsNullOrEmpty(input))
            {
                return "String is null";
            }

            List<string> tokens = GetTokens(input);

            int result = GetExpressionResult(tokens);

            return IntToRoman(result);
        }

        // ( 90 - 20 ) * 3
        private int GetExpressionResult(List<string> tokens)
        {
            Stack<int> operandStack = new Stack<int>();
            Stack<char> operatorStack = new Stack<char>();

            foreach (var token in tokens)
            {
                if (token == "(")
                {
                    operatorStack.Push(token[0]);
                }
                else if (token == ")")
                {
                    while (operatorStack.Peek() != '(')
                    {
                        DoProcessOp(operandStack, operatorStack.Peek());
                        operatorStack.Pop();
                    }

                    operatorStack.Pop();
                }
                else if (ConvertRomanToInt(token, out int number))
                {
                    operandStack.Push(number);
                }
                else // if operator
                {
                    char curOp = token[0];
                    while (operatorStack.Count != 0 && GetPriority(operatorStack.Peek()) >= GetPriority(curOp))
                    {
                        DoProcessOp(operandStack, operatorStack.Peek());
                        operatorStack.Pop();
                    }
                    operatorStack.Push(curOp);
                }
            }

            while (operatorStack.Count != 0)
            {
                DoProcessOp(operandStack, operatorStack.Peek());
                operatorStack.Pop();
            }

            return operandStack.Peek();
        }

        private void DoProcessOp(Stack<int> operandStack, char v)
        {
            int r = operandStack.Pop();
            int l = operandStack.Pop();

            switch (v)
            {
                case '+':
                    operandStack.Push(l + r);
                    break;
                case '-':
                    operandStack.Push(l - r);
                    break;
                case '*':
                    operandStack.Push(l * r);
                    break;
                case '/':
                    operandStack.Push(l / r);
                    break;
            }
        }

        private int GetPriority(char op)
        {
            if (op == '+' || op == '-')
            {
                return 1;
            }

            if (op == '*' || op == '/')
            {
                return 2;
            }

            return -1;
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        private List<string> GetTokens(string input)
        {
            List<string> tokens = new List<string>();
            string currentToken = string.Empty;

            foreach (char c in input)
            {
                if (IsOperator(c) || c == '(' || c == ')')
                {
                    if (currentToken.Length > 0)
                    {
                        tokens.Add(currentToken);
                        currentToken = string.Empty;
                    }

                    tokens.Add(c.ToString());
                }
                else
                {
                    currentToken += c;
                }
            }

            if (currentToken.Length > 0)
            {
                tokens.Add(currentToken);
            }

            return tokens;
        }

        // number

        private bool ConvertRomanToInt(string s, out int result)
        {
            int sum = 0;
            int last = 0;
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (RomanNumbers.RomanToIntDictionary.ContainsKey(s[i]) == false)
                {
                    result = 0;
                    return false;
                }

                int current = RomanNumbers.RomanToIntDictionary[s[i]];
                if (current < last)
                {
                    sum -= current;
                }
                else
                {
                    sum += current;
                }

                last = current;
            }

            result = sum;
            return true;
        }

        private string IntToRoman(int num)
        {
            var result = new StringBuilder();

            foreach (var kv in RomanNumbers.IntToRomanDictionary)
            {
                while (num >= kv.Key)
                {
                    num -= kv.Key;
                    result.Append(kv.Value);
                }
            }

            return result.ToString();
        }
    }
}