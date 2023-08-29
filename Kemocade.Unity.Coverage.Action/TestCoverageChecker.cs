using System.Xml.Linq;

namespace Kemocade.Unity.Coverage.Action;

internal class TestCoverageChecker
{
    private readonly FileInfo _coverageSummaryFile;
    private readonly XDocument _coverageSummaryReport;

    public TestCoverageChecker(FileInfo coverageSummaryFile)
    {

        if (!coverageSummaryFile.Exists)
        { throw new FileNotFoundException(coverageSummaryFile.FullName); }

        Console.WriteLine("Found coverage file at " + coverageSummaryFile.FullName);
        string coverageFileText = File.ReadAllText(coverageSummaryFile.FullName);
        _coverageSummaryReport = XDocument.Parse(coverageFileText);

        _coverageSummaryFile = coverageSummaryFile;
    }

    public float GetCodeCoverage()
    {
        if (!_coverageSummaryFile.Exists)
        { throw new FileNotFoundException(_coverageSummaryFile.FullName); }

        IEnumerable<XElement> coverage = _coverageSummaryReport.Descendants("Linecoverage");
        if (!coverage.Any()) { return 0; }

        string coverageStr = coverage.First().Value;
        return float.Parse(coverageStr);
    }
}
