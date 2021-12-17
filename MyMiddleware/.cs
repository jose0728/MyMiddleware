using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMiddleware
{
    /*
     * Middleware
     * 中间件在ASP.NET Core 中被表示成一个 Func<RequestDelegate,RequestDelegate>对象，即它的输入和输出都是一个RequestDelegate。
     * 为什么采用一个Func<RequestDelegate,RequestDelegate>对象来表示中间件。是因为这样的考虑：
     * 对于管道中的某一个中间件来说，由后续中间件组成的管道体现为一个RequestDelegate对象，
     * 由于当前中间件在完成了自身的请求处理任务之后，往往需要将请求分发给后续中间件进行处理，所以它需要将由后续中间件构成的RequestDelegate作为输入。
     * 即：上一个中间件的输出需要可以作为下一个中间件的输入，所以设计为Func<RequestDelegate,RequestDelegate>对象
     */

    /*
     * ApplicationBuilder
     * ApplicationBuilder 是用来构建 Application的。
     * 既然 Pipeline = Server + HttpHandler , 可以看出HttpHandler承载了当前应用的所有职责，那么 HttpHandler就等于 Application。
     * 由于 HttpHandler通过RequestDelegate表示，那么由ApplicationBuilder构建的Application就是一个RequestDelegate对象。（职责1）
     * 由于表示HttpHandler的RequestDelegate是由注册的中间件来构建的，所以ApplicationBuilder还具有注册中间件的功能。(职责2)
     * 基于ApplicationBuilder具有的这两个基本职责，我们可以将对应的接口定义为如下形式。
     * Use 方法用来注册提供的中间件，Builder方法则将注册的中间件构建成一个RequestDelegate对象。
     */
    public class MyApplicationBuilder : IMyApplicationBuilder
    {


        /// <summary>
        /// 将注册的中间件构建成一个RequestDelegate对象。
        /// </summary>
        /// <returns></returns>
        public RequestDelegate Build()
        {

        }

        /// <summary>
        /// 注册提供的中间件
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public IMyApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}
