using System.Diagnostics;
using AccountApi.Utilities;

namespace AccountApi.Domain;

public class TimedAccountLineValidation : IAccountLineValidation
{
    private readonly IAccountLineValidation _wrapped;
    private readonly ILogger<TimedAccountLineValidation> _logger;

    public TimedAccountLineValidation(IAccountLineValidation wrapped, ILogger<TimedAccountLineValidation> logger)
    {
        _wrapped = wrapped;
        _logger = logger;
    }

    public List<string> ErrorsIn(TextLine line)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        List<string> errors = _wrapped.ErrorsIn(line);
        stopwatch.Stop();

        _logger.LogTrace("Line number {0} validation duration was {1}", line.Number, stopwatch.Elapsed);

        return errors;
    }
}
