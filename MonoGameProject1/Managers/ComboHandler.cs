using System;
using Microsoft.Xna.Framework;
using MonoGameProject1.Core;
using MonoGameProject1.UI;
using MonoGameProject1.Utilities.Configs;
using MonoGameProject1.Visuals;

namespace MonoGameProject1;

public class ComboHandler : GameObject
{
    private ComboText comboText;
    // private ScoreText scoreText;
    private DepthMeter depthMeter;
    // private Text currentDepthText;

    public const int MAXCOMBO = 10;
    private const int SCOREFISH = 100;
    private const float MAXCOMBOTIME = 3.0f;
    private const float DEPTHUNIT = 10f;

    private int score = 0;
    private int combo = 0;
    private int lastCombo = 0;
    private float comboTimer = 0;
    private float currentDepth = 0f;
    private float totalDistance = 0f;

    public static int TotalFishCaught { get; private set; } = 0;
    public static float MaxDepthReached { get; private set; } = 0f;
    public static int HighestCombo { get; private set; } = 0;
    public static int BestScore { get; private set; } = 0;

    public int CurrentScore => score;
    public float CurrentDepth => currentDepth;
    public int CurrentCombo => combo;

    public static void UpdateFishCaught() => TotalFishCaught++;

    public static void UpdateMaxDepth(float depth)
    {
        if (depth > MaxDepthReached)
            MaxDepthReached = depth;
    }

    public static void UpdateHighestCombo(int combo)
    {
        if (combo > HighestCombo)
            HighestCombo = combo;
    }

    public static void UpdateBestScore(int score)
    {
        if (score > BestScore)
            BestScore = score;
    }

    public static void Reset()
    {
        TotalFishCaught = 0;
        MaxDepthReached = 0f;
        HighestCombo = 0;
    }

    public ComboHandler(string name, ComboText _comboText, ScoreText _scoreText, DepthMeter _depthMeter) : base(name)
    {
        Scale = new Vector2(0.5f, 0.5f);

        comboText = _comboText;
        // scoreText = _scoreText;
        depthMeter = _depthMeter;
        
       
        CreateCurrentDepthDisplay();
    }
    
    private void CreateCurrentDepthDisplay()
    {
        var fontInfo = TextManager.GetFont("Oswald");
        var currentDepthConfig = new TextConfig(fontInfo);

        currentDepthConfig.Color = Color.LightBlue;
        currentDepthConfig.EffectSettings.EnableColorCycle = true;
        currentDepthConfig.EffectSettings.ColorCycleSpeed = 0.5f;
        currentDepthConfig.EffectSettings.ColorPalette = new Color[]
        {
            Color.LightBlue,
            Color.CornflowerBlue,
            Color.DeepSkyBlue
        };
        
  }

    public override void Enable()
    {
        Fish.OnFishCaught += IncreaseCombo;
        Position = ScreenPosition.TopLeft() + new Vector2(120, 40);
        base.Enable();
    }

    public override void Disable()
    {
        Fish.OnFishCaught -= IncreaseCombo;
        base.Disable();
    }

    private void IncreaseCombo(Fish fish = null, bool isAboomnpha = false)
    {
        if (isAboomnpha) return;
        if (combo >= MAXCOMBO) return;

        lastCombo = combo;
        combo++;
        comboTimer = MAXCOMBOTIME;

        UpdateHighestCombo(combo);
        UpdateFishCaught();

        int fishScore = SCOREFISH * combo;
        score = (TotalFishCaught * fishScore) + (int)currentDepth;
        UpdateBestScore(score);

        comboText.Text.TriggerShake();
        comboText.Text.TriggerBounce();

        if (combo >= MAXCOMBO / 2)
        {
            comboText.TextConfig.EffectSettings.WaveAmplitude = 8f;
            comboText.TextConfig.EffectSettings.ColorCycleSpeed = 3f;
        }
    }

    public void UpdateDepth(float speed, float deltaTime)
    {
        float depthIncrement = (speed / 100f) * deltaTime;
        totalDistance += depthIncrement;
        currentDepth = totalDistance / DEPTHUNIT;

        UpdateMaxDepth(currentDepth);

        int fishScore = SCOREFISH * (combo > 0 ? combo : 1);
        score = (TotalFishCaught * fishScore) + (int)currentDepth;
    }

    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

        comboTimer -= deltaTime;
        if (comboTimer <= 0 && combo > 0)
        {
            combo = 0;
            comboTimer = 0;

            comboText.TextConfig.EffectSettings.WaveAmplitude = 5f;
            comboText.TextConfig.EffectSettings.ColorCycleSpeed = 2f;
        }

        UpdateUITexts();

        float scaleMultiplier = 1f + ((float)combo / MAXCOMBO) * (comboText.TextConfig.EffectSettings.MaxScale - 1f);
        comboText.TextConfig.EffectSettings.BaseScale = scaleMultiplier;

        base.Update(gameTime);
    }

    private void UpdateUITexts()
    {
        // Update combo text
        comboText.TextConfig.Text = $"Combo: {combo}x";

        // Update score text  
        // if (scoreText != null)
        // {
        //     scoreText.TextConfig.Text = $"Score: {score:N0}";
        // }

        // Update depth meter using the new method
        if (depthMeter != null)
        {
            depthMeter.UpdateDepthDisplay(currentDepth);
        }

        // Update current depth display
        // if (currentDepthText != null)
        // {
        //     currentDepthText.textConfig.Text = $"Depth: {currentDepth:F1}m";
        // }
    }
}