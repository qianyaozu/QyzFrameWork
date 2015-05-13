using Qyz.FrameWork.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Qyz.FrameWork.Core
{
    public class ReflectMethod
    {
        /// <summary>
        /// 根据类型所定义的dllInfoAttribute来获取方法实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GreateInstance<T>()
        {
            object obj = GetAttribute(typeof(T), typeof(DllInfoAttribute));
            if (obj == null)
                throw new Exception("接口没有定义DllInfoAttribute属性信息，不能反射调用");
            DllInfoAttribute attribute = obj as DllInfoAttribute;
            obj = CreateInstance(attribute.DllName, attribute.FullClassName);
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
                System.Type type = assembly.GetType(fullClassName);
                return type;
            }
            else
            {
                return assembly.CreateInstance(fullClassName);
            }
        }

        /// <summary>
        /// 获取类的自定义属性
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        private static object GetAttribute(System.Type type, System.Type attribute)
        {
            object[] objs = type.GetCustomAttributes(attribute, true);
            if (objs.Length > 0)
                return objs[0];
            return null;
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
        public static object RunReflectMethod(string dllName,string fullClassName,string methodName,bool isStatic,params object[] args)
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
    }
}
