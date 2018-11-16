using Godot;
using System;

public class HUD : CanvasLayer
{
    public Label m_ScoreLabel;
    public Sprite m_FirstHeart;
    public Sprite m_SecondHeart;
    public Sprite m_ThirdHeart;


    public override void _Ready()
    {
        m_ScoreLabel = (Label) GetNode("ScoreLabel");
        m_FirstHeart = (Sprite) GetNode("FirstHeart");
        m_SecondHeart = (Sprite) GetNode("SecondHeart");
        m_ThirdHeart = (Sprite) GetNode("ThirdHeart");
    }
    public void UpdateScore(int score)
    {
        m_ScoreLabel.Text = score.ToString();
    }

    public void UpdateLives(int lives)
    {
        if(lives == 3)
        {
            m_FirstHeart.Show();
            m_SecondHeart.Show();
            m_ThirdHeart.Show();
        }
        else if(lives == 2)
        {
            m_FirstHeart.Hide();
            m_SecondHeart.Show();
            m_ThirdHeart.Show();
            
        }
        else if(lives == 1)
        {
            m_FirstHeart.Hide();
            m_SecondHeart.Hide();
            m_ThirdHeart.Show();
            
        }
        else if(lives <= 0)
        {
            m_FirstHeart.Hide();
            m_SecondHeart.Hide();
            m_ThirdHeart.Hide();
            
        }
    }
}
