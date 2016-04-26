namespace Elements.Enumerations
{
    /// <summary>
    /// Represents the types of regular expression elements
    /// </summary>
    public enum ElementType
    {
        Expression,
        Character,
        SpecialCharacter,
        Group,
        Conditional,
        Alternative,
        CharacterClass,
        Comment,
        WhiteSpace
    }
}