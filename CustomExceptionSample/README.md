# 自定义异常处理
## ASP.NET CORE 内置的三个异常处理中间件 
- DeveloperExceptionPageMiddleware
    - 能给出详细的请求/返回/错误信息，因为包含敏感信息，所以仅适合开发环境
- ExceptionHandlerMiddleware
    - 仅处理500错误
- StatusCodePagesMiddleware 
    - 能处理400-599之间的错误，但需要Response中不能包含内容(ContentLength=0 && ContentType=null，经实验不能响应mvc里未捕获异常)

由于ExceptionHandlerMiddleware和StatusCodePagesMiddleware的各自的限制条件，两者需要搭配使用。相比之下自定义中间件更加灵活，既能对各种错误状态进行统一处理，也能按照配置决定处理方式。