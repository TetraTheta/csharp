using System.Collections.Generic;

namespace OutputColorizer.Format;

public class Lexer(string text) {
  private List<Token> _tokens = [];

  private readonly string Text = text;

  public List<Token> Tokenize() {
    if (_tokens != null) return _tokens;

    List<Token> tokens = [];
    int currentIndex = 0, previousTokenEnd = 0;

    while (currentIndex < Text.Length) {
      // skip over the next character
      if (Text[currentIndex] == '\\') {
        currentIndex++;
      } else {
        char token = Text[currentIndex];

        if (token == ']' || token == '[' || token == '!') {
          // put whatever was before this token into a string token
          if (previousTokenEnd != currentIndex) tokens.Add(new Token(TokenKind.String, previousTokenEnd, currentIndex - 1));

          // this will throw for invalid tokens.
          Token previousToken = new Token(token, currentIndex, currentIndex);
          tokens.Add(previousToken);
          previousTokenEnd = currentIndex + 1;
        }
      }

      // continue if we need to.
      currentIndex++;
    }

    // add a last segment if current index is different than previous token (i.e. some text after the last token)
    if (currentIndex != previousTokenEnd) tokens.Add(new Token(TokenKind.String, previousTokenEnd, Text.Length - 1));

    _tokens = [.. tokens];
    return _tokens;
  }

  public string GetValue(Token token) => Text.Substring(token.Start, token.End - token.Start + 1);

#if DEBUG
  public string WriteTokens() {
    StringBuilder sb = new();
    foreach (var item in _tokens) {
      sb.AppendLine($"{item.Kind} ({item.Start}-{item.End}): {GetValue(item)}");
    }
    return sb.ToString();
  }
#endif
}
