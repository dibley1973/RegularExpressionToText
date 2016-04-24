
namespace Elements
{
    public class Comment : Element
    {
        private string contents;

        public string Contents
        {
            get
            {
                return this.contents;
            }
        }

        public Comment(CharacterBuffer buffer)
        {
            //this.Image = ImageType.Comment;
            this.Start = buffer.IndexInOriginalBuffer;
            do
            {
                if (buffer.IsAtEnd)
                {
                    break;
                }
                buffer.MoveNext();
            }
            while (buffer.CurrentCharacter != '\n');
            this.End = buffer.IndexInOriginalBuffer;
            this.Literal = buffer.Substring(this.Start - buffer.Offset, this.End - this.Start);
            this.contents = this.Literal.Remove(0, 1).Trim();
            this.Description = string.Concat("Comment: ", this.contents);
            buffer.MoveNext();
        }
    }
}