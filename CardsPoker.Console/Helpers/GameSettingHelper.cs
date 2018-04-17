using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CardsPocker.Common;

namespace CardsPoker.Console.Helpers
{
    public static class GameSettingHelper
    {
        public static string ValidateInputAndAssignValueFor<TObj, TVal>(this TObj obj, Expression<Func<TObj, TVal>> propExpression, string input, int min, int max)
        {
            var propertyInfo = ((propExpression?.Body as MemberExpression)?.Member as PropertyInfo);
            if (propertyInfo != null)
            {
                var displayAttr = propertyInfo.GetCustomAttributes().FirstOrDefault(x => x is DisplayNameAttribute) as DisplayNameAttribute;
                var propertyDisplayName = displayAttr?.DisplayName ?? propertyInfo.Name;

                uint val;
                if (uint.TryParse(input, out val))
                {
                    if (val > max)
                    {
                        return $"{propertyDisplayName} can't be higher than {max}";
                    }
                    else if (val < min)
                    {
                        return $"{propertyDisplayName} can't be lower than {min}";
                    }
                    else
                    {
                        propertyInfo.SetValue(obj, val);
                        return null;
                    }
                }
                else
                {
                    return $"Can't parse {propertyDisplayName}. Please try again";
                }
            }

            return Constants.UnhandledError;
        }
    }
}
