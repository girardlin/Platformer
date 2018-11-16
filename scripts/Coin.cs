using Godot;
using System;

public class Coin : Area2D
{
    [Signal] public delegate void Hit();

    public CollisionShape2D m_CollisionShape2D;
    public AnimatedSprite m_AnimatedSprite;
    public AudioStreamPlayer2D m_AudioStreamPlayer2D;

    public override void _Ready()
    {
        m_CollisionShape2D = (CollisionShape2D) GetNode("CollisionShape2D");
        m_AnimatedSprite = (AnimatedSprite) GetNode("AnimatedSprite");
        m_AnimatedSprite.Play();

        m_AudioStreamPlayer2D = (AudioStreamPlayer2D) GetNode("Noise");
    }

    public void OnCoinBodyEntered(Godot.Object body)
    {
        m_AudioStreamPlayer2D.Play();
        Hide();
        EmitSignal("Hit");
        m_CollisionShape2D.Disabled = true;
    }
}
