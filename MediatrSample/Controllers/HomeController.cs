using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatrSample.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MediatrSample.Controllers
{
    #region MediatR
    public class SomeEvent : INotification
    {
        public SomeEvent(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public class Handler1 : INotificationHandler<SomeEvent>
    {
        private readonly ILogger<Handler1> _logger;

        public Handler1(ILogger<Handler1> logger)
        {
            _logger = logger;
        }
        public Task Handle(SomeEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Handled: {notification.Message}");
            return Task.CompletedTask;
        }
    }
    public class Handler2 : INotificationHandler<SomeEvent>
    {
        private readonly ILogger<Handler2> _logger;

        public Handler2(ILogger<Handler2> logger)
        {
            _logger = logger;
        }
        public Task Handle(SomeEvent notification,CancellationToken cancellationToken)
        {
            _logger.LogWarning($"Handled: {notification.Message}");
            return Task.CompletedTask;
        }
    }

    #endregion

    public class HomeController : Controller
    {
            private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            await _mediator.Publish(new SomeEvent("Hello World"),CancellationToken.None);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
