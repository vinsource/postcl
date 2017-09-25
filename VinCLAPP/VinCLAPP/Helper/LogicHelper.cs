using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using VinCLAPP.DatabaseModel;

namespace VinCLAPP.Helper
{
    public sealed class LogicHelper
    {
        public static Expression<Func<TElement, bool>> BuildContainsExpression<TElement, TValue>(
            Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            if (null == valueSelector)
            {
                throw new ArgumentNullException("valueSelector");
            }
            if (null == values)
            {
                throw new ArgumentNullException("values");
            }
            ParameterExpression p = valueSelector.Parameters.Single();
            // p => valueSelector(p) == values[0] || valueSelector(p) == ...
            if (!values.Any())
            {
                return e => false;
            }
            IEnumerable<Expression> equals =
                values.Select(
                    value =>
                    (Expression) Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof (TValue))));
            Expression body = equals.Aggregate((accumulate, equal) => Expression.Or(accumulate, equal));
            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }


        public static bool VerifyEmail(string emailVerify)
        {
            return Regex.IsMatch(emailVerify,
                                 "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$");
        }


       

        public static int GetDailyUse()
        {
            var context = new CLDMSEntities();

            return context.Trackings.Count(x => x.AccountId == GlobalVar.CurrentAccount.AccountId &&
                                                                  x.AddedDate.Value.Year == DateTime.Now.Year &&
                                                                  x.AddedDate.Value.Month == DateTime.Now.Month &&
                                                                  x.AddedDate.Value.Day == DateTime.Now.Day);
        }



    }
}