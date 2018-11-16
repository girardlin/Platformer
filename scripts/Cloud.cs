using Godot;
using System;

public class Cloud : KinematicBody2D
{
    [Export] public float m_FloatSpeed = 2;
    [Export] public float m_XLimit = 700;


    private Vector2 m_Velocity = new Vector2();
    
    public override void _Ready()
    {

    }

    public override void _PhysicsProcess(float delta)
    {
        m_Velocity = new Vector2();
        m_Velocity.x += m_FloatSpeed;
        MoveAndSlide(m_Velocity);

        if(Position.x > m_XLimit)
        {
            Position = new Vector2(-50, Position.y);
        }
    }
}
