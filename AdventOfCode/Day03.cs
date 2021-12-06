using System.Text;

namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override async ValueTask<string> Solve_1() => new(await Part1());

    public override async ValueTask<string> Solve_2() => new(await Part2());

    private async Task<string> Part1()
    {
        var gammaRateBinary = new string(_input.Split(Environment.NewLine)
            .Transpose()
            .Select(s => s.GroupBy(c => c)
            .OrderByDescending(x => x.Count()).First().Key)
            .ToArray()
            );

        var gammaRate = Convert.ToInt32(gammaRateBinary, 2);

        var epsilonRateBinary = new string(gammaRateBinary.Select(c => c == '1' ? '0' : '1').ToArray());

        var epsilonRate = Convert.ToInt32(epsilonRateBinary, 2);

        return (gammaRate * epsilonRate).ToString();
    }

    private async Task<string> Part2()
    {
        return "part2";
    }
}
