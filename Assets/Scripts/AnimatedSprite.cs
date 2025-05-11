using UnityEngine;

/// <summary>
/// Deze klasse zorgt voor het afspelen van sprite animaties. De animaties worden periodiek gewisseld op basis van de animationTime variablele.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer { get; private set; }
    public Sprite[] Sprites;
    public float animationTime = 0.25f;
    public int AnimationFrame { get; private set; }
    public bool loop = true;

    private void Awake()
    {
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime);
    }

    private void Advance()
    {
        if (!this.SpriteRenderer.enabled)
        {
            return;
        }

        this.AnimationFrame++;
        // Loop sprites back to 0 when last sprite renderd 
        if (this.AnimationFrame >= this.Sprites.Length && this.loop)
        {
            this.AnimationFrame = 0;
        }
        // Update the sprite
        if (this.AnimationFrame >= 0 && this.AnimationFrame < this.Sprites.Length)
        {
            this.SpriteRenderer.sprite = this.Sprites[this.AnimationFrame];
        }
    }

    /// <summary>
    /// Herstart de animatie naar de eerste frame in de Sprite[]
    /// </summary>
    public void Restart()
    {
        this.AnimationFrame = -1;
        Advance();
    }
}
