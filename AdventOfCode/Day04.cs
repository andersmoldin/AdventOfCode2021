using System.Text.RegularExpressions;

namespace AdventOfCode;

public class Day04 : BaseDay
{
    private readonly List<string> _input;
    private readonly List<int> _bingoNumbers;

    public Day04()
    {
        _input = File.ReadAllLines(InputFilePath)
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        _bingoNumbers = _input[0]
            .Split(',')
            .Select(int.Parse)
            .ToList();
    }

    public override ValueTask<string> Solve_1()
    {
        var bingoBoards = GetBingoBoards(_input);
        return PlayBingo(_bingoNumbers, bingoBoards, Find.First);
    }

    public override ValueTask<string> Solve_2()
    {
        var bingoBoards = GetBingoBoards(_input);
        return PlayBingo(_bingoNumbers, bingoBoards, Find.Last);
    }

    List<List<List<int>>> GetBingoBoards(List<string> input)
    {
        return input
            .Skip(1)
            .Batch(5)
            .Select(r => r
                .Select(r => Regex.Split(r.Trim(), @"\s+")
                .Select(r => int.Parse(r))
                    .ToList())
                .ToList())
            .ToList();
    }

    ValueTask<string> PlayBingo(List<int> bingoNumbers, List<List<List<int>>> bingoBoards, Find find)
    {
        var register = new Dictionary<int, bool>();

        if (find == Find.Last)
        {
            for (int i = 0; i < bingoBoards.Count; i++)
            {
                register.Add(i, false);
            }
        }

        for (int i = 0; i < bingoNumbers.Count; i++)
        {
            for (int j = 0; j < bingoBoards.Count; j++)
            {
                for (int k = 0; k < bingoBoards[j].Count; k++)
                {
                    for (int l = 0; l < bingoBoards[j][k].Count; l++)
                    {
                        if (bingoBoards[j][k][l] == bingoNumbers[i])
                        {
                            bingoBoards[j][k][l] = -1;
                        }
                    }
                }

            }

            int bingo = -1;
            switch (find)
            {
                case Find.First:
                    bingo = Bingo(bingoBoards, find);
                    break;
                case Find.Last:
                    bingo = Bingo(bingoBoards, find, register);
                    break;
            }
            if (bingo != -1)
            {
                return new((bingo * bingoNumbers[i]).ToString());
            }
        }

        return new("No one won!");
    }

    int Bingo(List<List<List<int>>> boards, Find find, Dictionary<int, bool> register = null)
    {
        switch (find)
        {
            case Find.First:
                foreach (var board in boards)
                {
                    if (board.Any(r => r.Sum() == -5) || board.Transpose().Any(r => r.Sum() == -5))
                    {
                        return board.SelectMany(n => n).Select(n => n).Where(n => n >= 0).Sum();
                    }
                }
                break;

            case Find.Last:

                if (boards.All(b => b.Any(r => r.Sum() == -5) || b.Transpose().Any(r => r.Sum() == -5)))
                {
                    return boards[register.Single(b => !b.Value).Key].SelectMany(n => n).Select(n => n).Where(n => n >= 0).Sum();
                }

                for (int i = 0; i < boards.Count; i++)
                {
                    if (boards[i].Any(r => r.Sum() == -5) || boards[i].Transpose().Any(r => r.Sum() == -5))
                    {
                        register[i] = true;
                        //if (register.Values.Count(b => !b) == 1)
                        //{
                        //    return boards[register.Single(b => !b.Value).Key].SelectMany(n => n).Select(n => n).Where(n => n >= 0).Sum();
                        //}
                        //else
                        //{
                        //}
                    }
                }

                break;
        }

        return -1;
    }

    enum Find
    {
        First,
        Last
    }
}
