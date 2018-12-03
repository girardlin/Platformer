using Godot;
using System;

public class Player : KinematicBody2D
{
    public enum AnimationState
    {
        Idle,
        Running,
        Jumping,
        Punching,
        Neutral
    };

    [Export] public float m_MovementSpeed = 105f;
    [Export] public float m_JumpSpeed = -410;
    [Export] public float m_Gravity = 1200;
    [Export] public float m_IdleAnimationSpeed = 3f;
    [Export] public float m_RunningAnimationSpeed = 12f;
    [Export] public bool m_FallReset = true;
    [Export] public float m_FallDistanceBeforeReset = 300f;
    [Export] public float m_ResetLocationX = 32;
    [Export] public float m_ResetLocationY = 32;
    [Export] public int m_Damage = 2;
    
    [Signal] public delegate void Fell();
    [Signal] public delegate void Damage();

    private bool m_Jumping = false;
    private Vector2 m_Velocity = new Vector2();
    private AnimationState m_AnimState;
    private AnimationState m_LastAnimState;
    private bool m_LastVelocityXPositive;

    CollisionShape2D m_PunchShape;
    
    public override void _Ready()
    {
        m_LastAnimState = AnimationState.Neutral;
        m_AnimState = AnimationState.Idle;

        m_PunchShape = (CollisionShape2D) GetNode("PunchArea/PunchShape");
        m_PunchShape.Disabled = true;
    }

    public void ProcessInputs()
    {
        //velocity addition based on input
        m_Velocity.x = 0;

        bool right = Input.IsActionPressed("ui_right");
        bool left = Input.IsActionPressed("ui_left");
        bool jump = Input.IsActionPressed("ui_select");
        bool punch = Input.IsActionPressed("Punch");

        if (jump && IsOnFloor())
        {
            m_Jumping = true;
            m_Velocity.y = m_JumpSpeed;
        } 
        if (right)
        {
            m_Velocity.x += m_MovementSpeed;
        }
        if (left)
        {
            m_Velocity.x -= m_MovementSpeed;
        }

        //states for animation player to handle
        //determines velocity and chooses the appropriate animation state
        //checks to see if the state is the same, if so, do not play a new animation
        if(m_Velocity.y == 0 && m_Velocity.x != 0)
        {
            m_AnimState = AnimationState.Running;
            if(m_Velocity.x > 0)
            {
                m_LastVelocityXPositive = true;
            }
            else
            {
                m_LastVelocityXPositive = false;
            }
        } 
        else if (m_Velocity.y != 0)
        {
            m_AnimState = AnimationState.Jumping;
        }
        else
        {
            if(punch)
            {
                m_AnimState = AnimationState.Punching;
            }
            else
            {
                m_PunchShape.Disabled = true;
                m_AnimState = AnimationState.Idle;
            }
        }

        if(m_AnimState == m_LastAnimState)
        {
            m_AnimState = AnimationState.Neutral;
        }
        else
        {
            m_LastAnimState = m_AnimState;
        }

    }

    public void ProcessAnimations()
    {
        //flip sprite based on velocity
        var m_Sprite = (Sprite) GetNode("Sprite");
        if(m_LastVelocityXPositive == false)
        {
            m_Sprite.FlipH = true;
            m_PunchShape.Position = new Vector2(-1 * Math.Abs(m_PunchShape.Position.x), m_PunchShape.Position.y);
        }
        else
        {
            m_Sprite.FlipH = false;
            m_PunchShape.Position = new Vector2(1 * Math.Abs(m_PunchShape.Position.x), m_PunchShape.Position.y);
        }

        //determine animation to play
        var m_AnimPlayer = (AnimationPlayer) GetNode("AnimationPlayer");
        
        if(m_AnimState == AnimationState.Idle)
        {
            m_AnimPlayer.Play("Idle", -1, m_IdleAnimationSpeed, false);
        }
        else if(m_AnimState == AnimationState.Running)
        {
            m_AnimPlayer.Play("Running", -1, m_RunningAnimationSpeed, false);
        }
        else if(m_AnimState == AnimationState.Jumping)
        {
            m_AnimPlayer.Play("Jumping", -1, 1, false);
        } 
        else if(m_AnimState == AnimationState.Punching)
        {
            m_AnimPlayer.Play("Punching", -1, 13, false);
        }
    }
    
    public override void _PhysicsProcess(float delta)
    {
        //process inputs and calculate physics
        ProcessInputs();
        m_Velocity.y += m_Gravity * delta;
        if (m_Jumping && IsOnFloor())
        {
            m_Jumping = false;
        }

        //process animations based on physics
        ProcessAnimations();

        //perform physics
        m_Velocity = MoveAndSlide(m_Velocity, new Vector2(0, -1));

        //
        if(m_FallReset && Position.y > m_FallDistanceBeforeReset)
        {
            EmitSignal("Fell");
            Position = new Vector2(m_ResetLocationX, m_ResetLocationY);
            m_Velocity.y = 0;
        }
    }

    public void DamageEnemy()
    {
        EmitSignal("Damage");
    }
}
