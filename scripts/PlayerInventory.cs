using Godot;
using System;

public class PlayerInventory : Node2D
{

    [Signal] public delegate void addItemCallback();
    [Signal] public delegate void removeItemCallback();
    public Dictionary<object, object> m_Inventory;

    public void addItem(Item input)
    {
        m_Inventory.Add(input.m_ID, input);
        EmitSignal("addItemCallback");
    }

    public void removeItem(Item input)
    {
        m_Inventory.Remove(input.m_ID);
        EmitSignal("removeItemCallback");
    }
}