using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly List<string> _input;
    private readonly List<int> _bingoNumbers;
    private readonly List<List<List<int>>> _bingoBoards;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath)
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        _bingoNumbers = _input[0]
            .Split(',')
            .Select(int.Parse)
            .ToList();

        _bingoBoards = _input
            .Skip(1)
            .Batch(5)
            .Select(r => r
                .Select(r => Regex.Split(r.Trim(), @"\s+")
                .Select(r => int.Parse(r))
                    .ToList())
                .ToList())
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        for (int i = 0; i < _bingoNumbers.Count; i++)
        {
            for (int j = 0; j < _bingoBoards.Count; j++)
            {
                for (int k = 0; k < _bingoBoards[j].Count; k++)
                {
                    for (int l = 0; l < _bingoBoards[j][k].Count; l++)
                    {
                        if (_bingoBoards[j][k][l] == _bingoNumbers[i])
                        {
                            _bingoBoards[j][k][l] = -1;
                        }
                    }
                }

            }

            var bingo = Bingo(_bingoBoards);
            if (bingo != -1)
            {
                return new((bingo * _bingoNumbers[i]).ToString());
            }
        }

        return new("No one won!");

        int Bingo(List<List<List<int>>> boards)
        {
            foreach (var board in boards)
            {
                if (board.Any(r => r.Sum() == -5) || board.Transpose().Any(r => r.Sum() == -5))
                {
                    return board.SelectMany(n => n).Select(n => n).Where(n => n >= 0).Sum();
                }
            }

            return -1;
        }
    }

    public override ValueTask<string> Solve_2()
    {
        return new("Part 2");
    }
}
