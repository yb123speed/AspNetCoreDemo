# [.NET Core 图片操作在 Linux/Docker 下的坑](https://www.cnblogs.com/stulzq/p/10172550.html)

一.前言
.NET Core 目前更新到2.2了，但是直到现在在 .NET Core 本身依然不包括和图片有关的 Image、Bitmap 等类型。对于图片的操作在我们开发中很常见，比如：生成验证码、二维码等等。在 .NET Core 的早期版本中，有 .NET 社区开发者实现了一些 System.Drawing 的 Image等类型实现的组件，比如 CoreCompat.System.Drawing、ZKWeb.System.Drawing等。后来微软官方提供了一个组件 System.Drawing.Common实现了 System.Drawing 的常用类型，以 Nuget 包的方式发布的。今天就围绕它来讲一讲这里面的坑。

在 .NET Core 中可以通过安装 System.Drawing.Common 来使用 Image、Bitmap 等类型。

二.寻坑
本文将以一个 ASP.NET Core 项目使用 QRCoder 组件来生成一个二维码作为示例。

1.新建一个 ASP.NET Core 项目
2.安装 QRCoder
dotnet add package QRCoder
QRCoder是一个非常强大的生成二维码的组件，它使用了 System.Drawing.Common ，所以安装它用来做测试。

3.打开 ValuesController，添加如下代码：

```c#
[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public FileResult Get()
    {
        QRCodeGenerator.ECCLevel eccLevel = QRCodeGenerator.ECCLevel.L;
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode("Hello .NET Core", eccLevel))
            {
                using (QRCode qrCode = new QRCode(qrCodeData))
                {
                    Bitmap bp = qrCode.GetGraphic(20, Color.Black, Color.White,true);
                    return File(Bitmap2Byte(bp), "image/png", "hello-dotnetcore.png");
                }
            }
        }
    }

    public static byte[] Bitmap2Byte(Bitmap bitmap)
    {
        using (MemoryStream stream = new MemoryStream())
        {
            bitmap.Save(stream, ImageFormat.Jpeg);
            byte[] data = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.Read(data, 0, Convert.ToInt32(stream.Length));
            return data;
        }
    }
```

上面的代码生成了一个二维码，通过API返回，文件名为 hello-dotnetcore.png

4.运行
（1）Windows
在 Windows 环境下我们直接运行，打开浏览器访问 http://localhost:5000/api/values

一切正常

（2）Linux 或者 Docker(Linux)
Docker(Linux)指：以Linux系统为基础的镜像
我们将代码原封不动的拷贝到 Linux 上运行
使用curl访问

```curl http://localhost:5000/api/values```

查看日志输出可以见到报错了


fail: Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
      An unhandled exception has occurred while executing the request.
System.TypeInitializationException: The type initializer for 'Gdip' threw an exception. ---> System.DllNotFoundException: Unable to load DLL 'libgdiplus': The specified module could not be found.

该异常的意思是： 找不到DLL libgdiplus，如何解决？请看下一小节。

三.埋坑
System.Drawing.Common 组件提供对GDI+图形功能的访问。它是依赖于GDI+的，那么在Linux上它如何使用GDI+，因为Linux上是没有GDI+的。Mono 团队使用C语言实现了GDI+接口，提供对非Windows系统的GDI+接口访问能力（个人认为是模拟GDI+，与系统图像接口对接），这个就是 libgdiplus。进而可以推测 System.Drawing.Common 这个组件实现时，对于非Windows系统肯定依赖了 ligdiplus 这个组件。如果我们当前系统不存在这个组件，那么自然会报错，找不到它，安装它即可解决。

libgdiplus github: https://github.com/mono/libgdiplus

1.CentOS

### 一键命令

```sudo curl https://raw.githubusercontent.com/stulzq/awesome-dotnetcore-image/master/install/centos7.sh|sh```

或者

```shell
yum update
yum install libgdiplus-devel -y
ln -s /usr/lib64/libgdiplus.so /usr/lib/gdiplus.dll
ln -s /usr/lib64/libgdiplus.so /usr/lib64/gdiplus.dll
```

2.Ubuntu

### 一键命令

```sudo curl https://raw.githubusercontent.com/stulzq/awesome-dotnetcore-image/master/install/ubuntu.sh|sh```

或者

```shell

apt-get update
apt-get install libgdiplus -y
ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll

```

3.Docker
Dockerfile 加入 RUN 命令，以官方 asp.net core runtime 镜像，以 asp.net core 2.2 作为示例：

```yaml

FROM microsoft/dotnet:2.2.0-aspnetcore-runtime
WORKDIR /app
COPY . .
RUN apt-get update -y && apt-get install -y libgdiplus && apt-get clean && ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
EXPOSE 80
ENTRYPOINT ["dotnet", "<你的入口程序集>"]

```

`apt-get update`这一步是必不可少的，不然会报找不到 libgdiplus。但是官方镜像里面使用的软件包源又是国外的地址，所以造成我们使用国内网络非常慢，进而造成整体构建过程非常慢。下面有两个解决方案：

（1）直接使用打包好的Docker镜像
该镜像是基于微软官方镜像打包的，只安装了 libgdiplus，不添加任何添加剂。

将 Dockerfile 中的 FROM microsoft/dotnet:2.2.0-aspnetcore-runtime 换为 FROM stulzq/dotnet:2.2.0-aspnetcore-runtime-with-image

示例：

```yaml

FROM stulzq/dotnet:2.2.0-aspnetcore-runtime-with-image
WORKDIR /app
COPY . .
EXPOSE 80
ENTRYPOINT ["dotnet", "<你的入口程序集>"]

```
（2）更换软件包源为国内源
此方法请看我以前写的文章：Docker实用技巧之更改软件包源提升构建速度

4.其他Linux发行版
首先查询下是否有编译好的 libgdiplus，如果没有可以到官方github查看教程，使用源码编译。

四.其他
这里要说明一下在 .NET Core 下，并非所有与图片操作有关的都需要安装 libgdiplus，只有你使用的组件依赖于 它提供的GDI+能力（依赖于它）才有必要装它。就比如你要是用 Image、Bitmap 类型，你就得安装 System.Drawing.Common ；或者你用的组件依赖了 System.Drawing.Common，比如 QRCoder。

有一些可以用于 .NET Core 的图片处理组件，自身没有依赖于 System.Drawing.Common，也没有依赖于 GDI+，使用它们是无需注意libgdiplus 这个问题的，比如 ImageSharp ，它使用纯C#实现了一些图片底层操作。

SkiaSharp 同样是可以进行图片操作的组件，在Linux上需要安装libSkiaSharp，SkiaSharp是由mono项目组提供的。我没有深入研究这个库，有兴趣的同学可以研究一下。

五.结束
本文所诉问题，其实是个老问题了，网上也都有解决方案，本文是搁置很久（一直处于未编辑完状态）才发布的，这里就算做个总结吧。

本文所用测试代码、shell命令、以及 Dockerfile 都在github: https://github.com/stulzq/dotnetcore-image 如果觉得有用欢迎 Star img


转自博客园——[晓晨Master（李志强）](https://www.cnblogs.com/stulzq)