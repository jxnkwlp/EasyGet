using System.Collections.Generic;
using System.Linq;

namespace Passingwind.EasyGet;

public static class Extensions
{
    public static Dictionary<int, IEnumerable<T>> SplitByPeer<T>(this IEnumerable<T> source, int peerCount)
    {
        var count = source.Count() % peerCount == 0 ? (source.Count() / peerCount) : (source.Count() / peerCount) + 1;

        var result = new Dictionary<int, IEnumerable<T>>();

        for (int i = 0; i < count; i++)
        {
            result.Add(i, source.Skip(i * peerCount).Take(peerCount));
        }

        return result;
    }
}
