namespace Kemocade.Unity.Coverage.Action;

internal class InsufficientCodeCoverageException : Exception
{
    private readonly float _actualCoverage;
    private readonly float _requiredCoverage;

    public InsufficientCodeCoverageException
    (float actualCoverage, float requiredCoverage) =>
        (_actualCoverage, _requiredCoverage) = (actualCoverage, requiredCoverage);

    public override string Message =>
        $"Code checks failed with {_actualCoverage}/{_requiredCoverage}%.";
}
