/* InfixExpression.cs
 * Author:  Hunter Bennett, Connor Black, James Dunton
 * Desc:    This class represents an infix expression, containing an expression and an identifier number.
 */

namespace Project2_Group_17
{
    public class InfixExpression
    {
        public int SNO { get; set; } // Serial/Expression number
        public string Expression { get; set; }

        public InfixExpression(int sno, string expression)
        {
            SNO = sno;
            Expression = expression;
        }

        public override string ToString()
        {
            return Expression;
        }
    }
}