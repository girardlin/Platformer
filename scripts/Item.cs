using Godot;
using System;

public class Item : Node2D
{
    public int m_ID;
    public string m_SpritePath;
    public string m_Name;
    public string m_Description;

    public Item(int ID, string SpritePath, string Name, string Description)
    {
        m_ID = ID;
        m_SpritePath = SpritePath;
        m_Name = Name;
        m_Description = Description;
    }
}
