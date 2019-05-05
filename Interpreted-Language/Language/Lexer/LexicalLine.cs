using System;

namespace InterpretedLanguage.Language.Lexer
{
    internal struct LexicalLine
    {
        private readonly string _line;
        private readonly int _number;
        private int _position;

        public int Length => _line.Length - _position;

        public LexicalLine(string line, int number)
        {
            _line = line ?? throw new ArgumentNullException(nameof(line));
            _number = number;
            _position = 0;
        }

        public static implicit operator string(LexicalLine line)
        {
            return line._line.Substring(line._position);
        }

        public static implicit operator int(LexicalLine line)
        {
            return line._number;
        }

        public void Seek(int offset)
        {
            _position += offset;
        }
    }
}