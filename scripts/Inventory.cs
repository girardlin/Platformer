using Godot;
using System;

public class Inventory : Node2D
{
    [Export] public int m_ColumnCount = 9;
    [Export] public Vector2 m_IconSize = new Vector2(32, 32);
    [Export] public Texture m_Texture;

    public ItemList m_ItemList;
    public override void _Ready()
    {
        m_ItemList = (ItemList) GetNode("Panel/ItemList");
        m_ItemList.SetMaxColumns(m_ColumnCount);
        m_ItemList.SetFixedIconSize(m_IconSize);
        m_ItemList.SetIconMode(ItemList.IconModeEnum.Top);
        m_ItemList.SetSelectMode(ItemList.SelectModeEnum.Single);
        m_ItemList.SetSameColumnWidth(false);

    }
}
