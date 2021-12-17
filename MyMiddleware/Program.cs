using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MyMiddleware
{
    internal class Program
    {
        private static readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();
        static void Main(string[] args)
        {
            Use(next =>
            {
                return context =>
                {
                    Console.WriteLine("1");
                    return next.Invoke(context);
                };
            });

            Use((RequestDelegate next) =>
            {
                RequestDelegate a = (HttpContext context) =>
                {
                    Console.WriteLine("2");
                    return next(context);
                };
                return a;
            });

            _middlewares.Reverse();

            RequestDelegate next = (context) =>
            {
                Console.WriteLine("end...");
                return Task.CompletedTask;
            };

            foreach (var middleware in _middlewares)
            {
                next = middleware(next);
            }

            // 模拟请求进来
            next(new Context());

            Console.ReadKey();
        }

        public static void Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
        }

    }

    public class Context : HttpContext
    {
        public override IFeatureCollection Features => throw new NotImplementedException();

        public override HttpRequest Request => throw new NotImplementedException();

        public override HttpResponse Response => throw new NotImplementedException();

        public override ConnectionInfo Connection => throw new NotImplementedException();

        public override WebSocketManager WebSockets => throw new NotImplementedException();

        public override AuthenticationManager Authentication => throw new NotImplementedException();

        public override ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IDictionary<object, object> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override IServiceProvider RequestServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override string TraceIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override ISession Session { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
