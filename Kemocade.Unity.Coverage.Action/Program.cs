using CommandLine;
using Kemocade.Unity.Coverage.Action;
using static System.Console;

// Configure Cancellation
using CancellationTokenSource tokenSource = new();
CancelKeyPress += delegate { tokenSource.Cancel(); };

// Configure Inputs
ParserResult<ActionInputs> parser = Parser.Default.ParseArguments<ActionInputs>(args);
if (parser.Errors.ToArray() is { Length: > 0 } errors)
{
    foreach (Error error in errors)
    { WriteLine($"{nameof(error)}: {error.Tag}"); }
    Environment.Exit(2);
    return;
}
ActionInputs inputs = parser.Value;

FileInfo coverageFilePath = new(inputs.CoverageFilePath);
TestCoverageChecker coverage = new(coverageFilePath);

float requiredCoverage = float.Parse(inputs.RequiredCoverage);
float actualCoverage = coverage.GetCodeCoverage();

if (actualCoverage >= requiredCoverage)
{ WriteLine($"Code coverage checks pass with {actualCoverage}/{requiredCoverage}%."); }
else
{ throw new InsufficientCodeCoverageException(actualCoverage, requiredCoverage); }