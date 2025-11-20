using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace TheRevelator.Services;

public record SentEmail(string To, string Subject, string Body, DateTimeOffset SentAt);

public class InMemoryEmailStore
{
    private readonly ConcurrentQueue<SentEmail> _emails = new();

    public void Add(SentEmail email) => _emails.Enqueue(email);
    public IEnumerable<SentEmail> GetAll() => _emails.ToArray();
}

public class InMemoryEmailSender : IEmailSender
{
    private readonly InMemoryEmailStore _store;

    public InMemoryEmailSender(InMemoryEmailStore store) => _store = store;

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        _store.Add(new SentEmail(email, subject, htmlMessage, DateTimeOffset.UtcNow));
        // For development, also write to console
        Console.WriteLine($"[DEV EMAIL] To={email}, Subject={subject}\n{htmlMessage}");
        return Task.CompletedTask;
    }
}
