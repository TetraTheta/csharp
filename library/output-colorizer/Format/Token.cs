using System;

namespace OutputColorizer.Format;

public struct Token {
  public Token(TokenKind kind, int start, int end) {
    Kind = kind;
    Start = start;
    End = end;
  }
  public Token(char charToken, int start, int end) {
    Kind = charToken switch {
      ']' => TokenKind.CloseBracket,
      '[' => TokenKind.OpenBracket,
      '!' => TokenKind.ColorDelimiter,
      _ => throw new InvalidOperationException(),
    };
    Start = start;
    End = end;
  }

  public TokenKind Kind { get; set; }
  public int Start { get; set; }
  public int End { get; set; }
}
