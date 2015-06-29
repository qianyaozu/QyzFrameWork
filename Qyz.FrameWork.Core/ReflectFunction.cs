using Qyz.FrameWork.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Qyz.FrameWork.Core
{
    public class ReflectFunction
    {
        /// <summary>
        /// 根据接口所定义的dllInfoAttribute来获取方法实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GreateInstance<T>()
        {
            //查询该类型的自定义特性
            object[] objs = typeof(T).GetCustomAttributes(typeof(DllInfoAttribute), true);
            if (objs.Length <= 0)
                throw new Exception("接口没有定义DllInfoAttribute属性信息，不能反射调用");
            DllInfoAttribute attribute = objs[0] as DllInfoAttribute;
            object obj = CreateInstance(attribute.DllName, attribute.FullClassName);
            if (obj != null)
                return (T)obj;
            return default(T);
        }

        /// <summary>
        /// 根据dll名字，方法名来获取方法实例
        /// </summary>
        /// <param name="dllName"></param>
        /// <param name="fullClassName"></param>
        /// <param name="isStaticClass"></param>
        /// <returns></returns>
        public static object CreateInstance(string dllName, string fullClassName, bool isStaticClass = false)
        {
            if (!dllName.Contains(':'))
                dllName = AppDomain.CurrentDomain.BaseDirectory + dllName;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(dllName);
            if (assembly == null)
                return null;
            if (isStaticClass)
            { 
                return assembly.GetType(fullClassName);
            }
            else
            {
                return assembly.CreateInstance(fullClassName);
            }
        }

        /// <summary>
        /// 根据dll名，类名，方法名调用方法
        /// </summary>
        /// <param name="dllName"></param>
        /// <param name="fullClassName"></param>
        /// <param name="methodName"></param>
        /// <param name="isStatic"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static object RunReflectMethod(string dllName, string fullClassName, string methodName, bool isStatic, params object[] args)
        {
            object obj = CreateInstance(dllName, fullClassName, isStatic);
            if (obj == null) return false;
            MethodInfo method;
            if (isStatic)
                method = ((System.Type)obj).GetMethods().First(p => p.Name == methodName);
            else
                method = obj.GetType().GetMethod(methodName);

            if (method == null)
                return false;

            return method.Invoke(obj, args);
        }

        /// <summary>
        /// 根据当前实例，方法名调用方法
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName"></param>
        /// <param name="isStatic"></param>
        /// <returns></returns>
        public static object RunReflectMethod(object obj, string methodName, bool isStatic)
        {
            MethodInfo method;
            if (isStatic)
                method = ((System.Type)obj).GetMethods().First(p => p.Name == methodName);
            else
                method = obj.GetType().GetMethod(methodName);

            if (method == null)
                return false;

            return method.Invoke(obj, null);
        }


        /// <summary>
        /// 获取属性名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static string GetPropertyName<T>(Expression<Func<T, object>> expr)
        {
            string result = string.Empty;
            if (expr.Body is UnaryExpression)
            {
                result = ((MemberExpression)((UnaryExpression)expr.Body).Operand).Member.Name;
            }
            else if (expr.Body is MemberExpression)
            {
                result = ((MemberExpression)expr.Body).Member.Name;
            }
            else if (expr.Body is ParameterExpression)
            {
                result = ((ParameterExpression)expr.Body).Type.Name;
            }
            return result;
        }

        /// <summary>
        /// 返回枚举的详细信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<int, string> GetEnumDictionary<T>()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            System.Type type = typeof(T);
            if (type.BaseType.FullName != "System.Enum")
            {
                return dic;
            }
            List<FieldInfo> list = type.GetFields().ToList();
            for (int i = 0; i < list.Count; i++)
            {
                FieldInfo fi = list[i];
                dic.Add((int)fi.GetValue(null), fi.GetValue(null).ToString());
            }
            return dic;
        }

        /// <summary>
        /// 根据程序集名称获得该程序集中的所有类型
        /// </summary>
        /// <param name="dllName"></param>
        /// <returns></returns>
        public static List<System.Type> GetClassName(string dllName)
        {
            List<System.Type> list = new List<System.Type>();
            if (!dllName.Contains(':'))
                dllName = AppDomain.CurrentDomain.BaseDirectory + dllName;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.LoadFrom(dllName);
            foreach (System.Type type in assembly.GetExportedTypes())
                list.Add(type);
            return list;
        }
    }
}
