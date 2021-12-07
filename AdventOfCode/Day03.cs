namespace AdventOfCode;

public class Day03 : BaseDay
{
    private readonly string[] _input;

    public Day03()
    {
        _input = File.ReadAllText(InputFilePath).Split(Environment.NewLine);
    }

    public override ValueTask<string> Solve_1()
    {
        var gammaRateBinary = new string(_input
            .Transpose()
            .Select(s => s.GroupBy(c => c)
            .OrderByDescending(x => x.Count()).First().Key)
            .ToArray());

        var gammaRate = Convert.ToInt32(gammaRateBinary, 2);

        var epsilonRateBinary = new string(gammaRateBinary.Select(c => c == '1' ? '0' : '1').ToArray());

        var epsilonRate = Convert.ToInt32(epsilonRateBinary, 2);

        return new((gammaRate * epsilonRate).ToString());
    }

    public override ValueTask<string> Solve_2()
    {
        var oxygenGeneratorRating = Convert.ToInt32(GetRating(_input, 0, '1'), 2);
        var co2ScrubberRating = Convert.ToInt32(GetRating(_input, 0, '0'), 2);

        return new((oxygenGeneratorRating * co2ScrubberRating).ToString());

        string GetRating(IEnumerable<string> input, int position, char masterBit)
        {
            if (input.Count() == 1)
            {
                return input.Single();
            }

            var mostCommon = CountOnesAndZeros(input, masterBit);

            return GetRating(input.Where(s => s[position] == mostCommon), position + 1, masterBit);

            char CountOnesAndZeros(IEnumerable<string> input, char masterBit)
            {
                var count = input
                    .Transpose()
                    .Select(s => new string(s.ToArray())).ToList()
                    .Select(s => s.GroupBy(c => c).ToDictionary(s => s.Key, s => s.Count()))
                    .Skip(position)
                    .First();

                return count['0'] != count['1'] ?
                    count.Aggregate((x, y) => 
                        masterBit == '0' ?
                            x.Value < y.Value ? x : y :
                            x.Value > y.Value ? x : y).Key :
                    masterBit;
            }
        }
    }
}
