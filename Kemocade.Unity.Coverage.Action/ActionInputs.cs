using CommandLine;

namespace Kemocade.Unity.Coverage.Action;

internal record ActionInputs
{
    [Option('p', "coverage-file-path", Required = true)]
    public string CoverageFilePath { get; init; } = null!;

    [Option('r', "required-coverage", Required = true)]
    public string RequiredCoverage { get; init; } = null!;
}