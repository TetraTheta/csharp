using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Configuration.Libraries {
  public class NaturalStringComparer : IComparer<string> {
    private static readonly Regex Chunker = new Regex(@"\d+|\D+", RegexOptions.Compiled);

    public int Compare(string x, string y) {
      if (ReferenceEquals(x, y)) return 0;
      if (x == null) return -1;
      if (y == null) return 1;

      MatchCollection xChunks = Chunker.Matches(x);
      MatchCollection yChunks = Chunker.Matches(y);

      int chunkCount = Math.Min(xChunks.Count, yChunks.Count);

      for (int i = 0; i < chunkCount; i++) {
        string xChunk = xChunks[i].Value;
        string yChunk = yChunks[i].Value;

        bool isXNumber = char.IsDigit(xChunk[0]);
        bool isYNumber = char.IsDigit(yChunk[0]);

        int cmp;

        if (isXNumber && isYNumber) {
          var xNum = BigInteger.Parse(xChunk);
          var yNum = BigInteger.Parse(yChunk);
          cmp = xNum.CompareTo(yNum);
        } else {
          cmp = string.Compare(xChunk, yChunk, StringComparison.OrdinalIgnoreCase);
        }
        if (cmp != 0) return cmp;
      }

      return xChunks.Count.CompareTo(yChunks.Count);
    }
  }
}
