# [AspectCore-Framework入门](https://www.cnblogs.com/CoderAyu/p/9906349.html)

## 什么是AOP？

在软件业，AOP为Aspect Oriented Programming的缩写，意为：面向切面编程，通过预编译方式和运行期动态代理实现程序功能的统一维护的一种技术。

有点深奥, 举个栗子

如果说之前做的一个系统专门给内部的服务提供接口的,因为是在内网中访问,所以就没有加上认证服务,现在这个系统要公开出来,同样的那套接口要给外部系统服务了,那么此时,就要进行认证,认证通过才能获取接口的数据.

传统的做法是,修改每一个接口.这样就会造成代码改动过大,很恐怖.

这个时候AOP就可以登场了,我们可以在这一类服务的前面,加上一个一系列上一刀,在切出来的裂缝里面插入认证方法.

![示例图](https://raw.githubusercontent.com/yb123speed/AspNetCoreDemo/master/Images/AOP.png)

然而,怎么插入这个切面是关键.AOP 实现会采用一些常见方法：

- 使用预处理器（如 C++ 中的预处理器）添加源代码。
- 使用后处理器在编译后的二进制代码上添加指令。
- 使用特殊编译器在编译时添加代码。
- 在运行时使用代码拦截器拦截执行并添加所需的代码。

但是常见还是后处理和代码拦截两种方式

- 后处理，或者叫 静态织入

  指使用 AOP 框架提供的命令进行编译，从而在编译阶段就可生成 AOP 代理类，因此也称为编译时增强或静态织入。dotnet 中一般在编译时通过在MSBiuld执行自定义的Build Task来拦截编译过程，在生成的程序集里插入自己的IL。

  dotnet 框架代表： [PostSharp](https://www.postsharp.net/aop.net)

- 代码拦截，或者叫 动态代理

  在运行时在内存中“临时”生成 AOP 动态代理类，因此也被称为运行时增强或动态代理。在dotnet 中一般在运行时通过Emit技术生成动态程序集和动态代理类型从而对目标方法进行拦截。

  dotnet 框架代表： [Castle DynamicProxy](https://github.com/castleproject/Core/blob/master/docs/dynamicproxy-introduction.md) 和 [AspectCore](https://github.com/dotnetcore/AspectCore-Framework)

  引用[https://github.com/dotnetcore/AspectCore-Framework/blob/master/docs/0.AOP%E7%AE%80%E5%8D%95%E4%BB%8B%E7%BB%8D.md](https://github.com/dotnetcore/AspectCore-Framework/blob/master/docs/0.AOP%E7%AE%80%E5%8D%95%E4%BB%8B%E7%BB%8D.md)