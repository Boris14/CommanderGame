using Godot;
using System;

public partial class Controller : Node
{
    protected Character PossessedCharacter;
    public virtual void SetCharacter(Character InCharacter)
    {
        PossessedCharacter = InCharacter;
    }
}
