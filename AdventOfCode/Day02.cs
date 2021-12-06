namespace AdventOfCode;

public class Day02 : BaseDay
{
    private readonly string _input;

    public Day02()
    {
        _input = File.ReadAllText(InputFilePath);
    }

    public override async ValueTask<string> Solve_1() => new(await Part1());

    public override async ValueTask<string> Solve_2() => new(await Part2());

    private async Task<string> Part1()
    {
        return _input.Split(Environment.NewLine)
            .Select(i => i.Split(" "))
            .Scan(new { horizontalPosition = 0, depth = 0 }, (state, i) =>
            i[0] == "forward" ? new { horizontalPosition = state.horizontalPosition + int.Parse(i[1]), depth = state.depth } :
            i[0] == "down" ? new { horizontalPosition = state.horizontalPosition, depth = state.depth + int.Parse(i[1]) } :
                                new { horizontalPosition = state.horizontalPosition, depth = state.depth - int.Parse(i[1]) }) // "up"
            .Select(i => i.horizontalPosition * i.depth)
            .Last()
            .ToString();
    }

    private async Task<string> Part2()
    {
        return _input.Split(Environment.NewLine)
            .Select(i => i.Split(" "))
            .Scan(new { aim = 0, horizontalPosition = 0, depth = 0 }, (state, i) =>
            i[0] == "forward" ? new { aim = state.aim, horizontalPosition = state.horizontalPosition + int.Parse(i[1]), depth = state.depth + (state.aim * int.Parse(i[1])) } :
            i[0] == "down" ? new { aim = state.aim + int.Parse(i[1]), horizontalPosition = state.horizontalPosition, depth = state.depth } :
                                new { aim = state.aim - int.Parse(i[1]), horizontalPosition = state.horizontalPosition, depth = state.depth }) // "up"
            .Select(i => i.horizontalPosition * i.depth)
            .Last()
            .ToString();
    }
}