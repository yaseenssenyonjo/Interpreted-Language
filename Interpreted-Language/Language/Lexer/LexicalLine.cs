using System;

namespace InterpretedLanguage.Language.Lexer
{
    /// <summary>
    /// Represents a line being scanned by the lexer.
    /// </summary>
    internal struct LexicalLine
    {
        /// <summary>
        /// The line.
        /// </summary>
        private readonly string _line;
        /// <summary>
        /// The line number.
        /// </summary>
        private readonly int _number;
        /// <summary>
        /// The position within the line.
        /// </summary>
        private int _position;
        
        /// <summary>
        /// The remaining length of the line.
        /// </summary>
        public int Length => _line.Length - _position;
        
        /// <summary>
        /// Initialises a new instance of the <see cref="LexicalLine"/> class.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="number">The line number.</param>
        /// <exception cref="ArgumentNullException">The line is null.</exception>
        public LexicalLine(string line, int number)
        {
            _line = line ?? throw new ArgumentNullException(nameof(line));
            _number = number;
            _position = 0;
        }
        
        /// <summary>
        /// Seeks forward by the specified the offset.
        /// </summary>
        /// <param name="offset">The offset.</param>
        public void Seek(int offset)
        {
            _position += offset;
        }
        
        public static implicit operator string(LexicalLine line)
        {
            return line._line.Substring(line._position);
        }

        public static implicit operator int(LexicalLine line)
        {
            return line._number;
        }
    }
}