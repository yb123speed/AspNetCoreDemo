using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CustomExceptionSample
{
    /// <summary>
    /// 异常中间件配置对象
    /// </summary>
    public class CustomExceptionMiddleWareOption
    {
        public CustomExceptionMiddleWareOption(
            CustomExceptionHandleType handleType = CustomExceptionHandleType.JsonHandle,
            IList<PathString> jsonHandleUrlKeys = null,
            string errorHandingPath = "")
        {
            HandleType = handleType;
            JsonHandleUrlKeys = jsonHandleUrlKeys;
            ErrorHandingPath = errorHandingPath;
        }

        /// <summary>
        /// 异常处理方式
        /// </summary>
        public CustomExceptionHandleType HandleType { get; set; }

        /// <summary>
        /// Json处理方式的Url关键字
        /// <para>仅HandleType=Both时生效</para>
        /// </summary>
        public IList<PathString> JsonHandleUrlKeys { get; set; }

        /// <summary>
        /// 错误跳转页面
        /// </summary>
        public PathString ErrorHandingPath { get; set; }
    }

    /// <summary>
    /// 错误处理方式
    /// </summary>
    public enum CustomExceptionHandleType
    {
        JsonHandle = 0,   //Json形式处理
        PageHandle = 1,   //跳转网页处理
        Both = 2          //根据Url关键字自动处理
    }
}