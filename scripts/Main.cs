using Godot;
using System;

public class Main : Node2D
{
    public HUD m_HUD;
    private int m_Score = 0;
    private int m_Lives = 3;
    public AudioStreamPlayer2D m_Hurt;
    
    public override void _Ready()
    {
        m_HUD = (HUD) GetNode("HUD");
        m_HUD.UpdateScore(0);
        m_Hurt = (AudioStreamPlayer2D) GetNode("Hurt");
    }
    
    public void ScoreUp()
    {
        m_Score++;
        m_HUD.UpdateScore(m_Score);
    }

    public void LivesDown()
    {
        m_Hurt.Play();
        m_Lives -= 1;
        m_HUD.UpdateLives(m_Lives);

        if(m_Lives <= 0)
        {
            GetTree().Quit();
        }
    }
}
