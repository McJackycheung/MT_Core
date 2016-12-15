using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MT.Core
{
   public class BaseValidate
    {
        /// <summary>
        /// 判断对象是否为Int32类型的数字
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        public static bool IsNumeric(object Expression)
        {
            if (Expression != null)
            {
                string str = Expression.ToString();
                if (str.Length > 0 && str.Length <= 11 && Regex.IsMatch(str, @"^[-]?[0-9]*[.]?[0-9]*$"))
                {
                    if ((str.Length < 10) || (str.Length == 10 && str[0] == '1') || (str.Length == 11 && str[0] == '-' && str[1] == '1'))
                    {
                        return true;
                    }
                }
            }
            return false;

        }


        public static bool IsDouble(object Expression)
        {
            if (Expression != null)
            {
                return Regex.IsMatch(Expression.ToString(), @"^([0-9])[0-9]*(\.\w*)?$");
            }
            return false;
        }

        /// <summary>
        /// 验证是否为正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt(string str)
        {

            return Regex.IsMatch(str, @"^[0-9]*$");
        }

        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        /// <summary>
        /// 检查是否为空（null 或是""）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNull(object obj)
        {
            if (obj == null)
            {
                return true;
            }

            string typeName = obj.GetType().Name;
            switch (typeName)
            {
                case "String[]":
                    string[] list = (string[])obj;
                    if (list.Length == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    string str = obj.ToString();
                    if (str == null || str == "")
                        return true;
                    else
                        return false;
            }
        }
        /// <summary>
        /// 检查是否为空（null 或是""）
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(object obj)
        {
            return (!IsNull(obj));
        }


        /// <summary>
        /// 判断传入的数字是否为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullZero(object obj)
        {
            return !IsNotNullZero(obj);
        }
        /// <summary>
        /// 判断传入的数字是否不为0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNullZero(object obj)
        {
            if (IsNull(obj))
            {
                return false;
            }
            else
            {
                try
                {
                    double d = Convert.ToDouble(obj);
                    if (d == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 判断是否为日期型变量
        /// </summary>
        /// <param name="str">日期变量</param>
        /// <returns>是否为日期</returns>
        public static bool IsDateTime(string str)
        {
            try
            {
                Convert.ToDateTime(str);

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 判断是否为日期型变量
        /// </summary>
        /// <param name="obj">日期变量</param>
        /// <returns>是否为日期</returns>
        public static bool IsDateTime(object obj)
        {
            try
            {
                Convert.ToDateTime(obj.ToString());

                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 判断是否为base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsBase64String(string str)
        {
            //A-Z, a-z, 0-9, +, /, =
            return Regex.IsMatch(str, @"[A-Za-z0-9\+\/\=]");
        }
    }
}
