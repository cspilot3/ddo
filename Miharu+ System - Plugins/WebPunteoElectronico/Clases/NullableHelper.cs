using Slyg.Tools;
using System;

public class NullableHelper
{
    private NullableHelper() { }

    public static NullableHelper Instance()
    {
        return new NullableHelper();
    }

    public object ConvertToNullableValue(string val, Type nType)
    {
        if (val == null || val.ToString().Trim() == "")
        {
            if (nType == typeof(int))
                return new SlygNullable<int>(DBNull.Value);
            else if (nType == typeof(decimal))
                return new SlygNullable<decimal>(DBNull.Value);
            else if (nType == typeof(string))
                return new SlygNullable<string>(DBNull.Value);
        }
        else
        {
            if (nType == typeof(int))
            {
                return NewSlygNullableInt(int.Parse(val));
            }
            else if (nType == typeof(decimal))
            {
                object Value;

                try
                {
                    Value = NewSlygNullableDec(decimal.Parse(val));
                }
                catch
                {
                    val = val.Replace("$","").Replace(" ","").Replace(",","");

                    var Valor = Slyg.Tools.DataConvert.ToNumericD(val, ".");

                    //string IntPart = val.Substring(0, val.Length - 3);
                    //string DecimalPart = val.Substring(val.Length - 3, 3);
                    //IntPart = IntPart.Replace(',', '.');
                    //DecimalPart = DecimalPart.Replace('.', ',');
                    //val = IntPart + DecimalPart;
                    Value = NewSlygNullableDec((decimal)Valor);
                }

                return Value;
            }

            if (nType == typeof(string))
                return NewSlygNullable(val);
        }

        return null;
    }

    private SlygNullable<string> NewSlygNullable(string val)
    {
        return new SlygNullable<string>(val);        
    }

    private SlygNullable<int> NewSlygNullableInt(int val)
    {
        return new SlygNullable<int>(val);        
    }

    private SlygNullable<decimal> NewSlygNullableDec(decimal val)
    {
        return new SlygNullable<decimal>(val);        
    }
}