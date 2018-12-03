using Godot;
using System;

public class Enemy : Area2D
{
    [Signal] public delegate void HitByWeapon();
    public int m_Health = 10;
    public void OnEnemyBodyEnteredByWeapon(Godot.Object body)
    {
        EmitSignal("HitByWeapon");
    }

    public void HitResponse()
    {
        m_Health -= 4;
    }

    public override void _PhysicsProcess(float delta)
    {
        if(m_Health <= 0)
        {
            Hide();
            var collshape = (CollisionShape2D) GetNode("CollisionShape2D");
            collshape.Disabled = true;
        }
    }
}
