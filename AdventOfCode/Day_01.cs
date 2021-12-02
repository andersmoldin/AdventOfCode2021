namespace AdventOfCode;

public class Day_01 : BaseDay
{
    private readonly string _input;

    public Day_01()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override async ValueTask<string> Solve_1() => new(await Part1());

    public override async ValueTask<string> Solve_2() => new(await Part2());

    private async Task<string> Part1()
    {
        return _input.Split(Environment.NewLine)
            .Select(int.Parse)
            .Pairwise((a, b) => a < b ? 1 : 0)
            .Sum()
            .ToString();
    }

    private async Task<string> Part2()
    {
        return _input.Split(Environment.NewLine)
            .Select(int.Parse)
            .Window(3)
            .Select(w => w.Sum())
            .Pairwise((a, b) => a < b ? 1 : 0)
            .Sum()
            .ToString();
    }
}
