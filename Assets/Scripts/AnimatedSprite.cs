using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
   public SpriteRenderer SpriteRenderer { get; private set;}
   public Sprite[] sprites;
   public float animationTime = 0.25f;
   public int AnimationFrame {get; private set;}
   public bool loop = true;

   private void Awake(){
        this.SpriteRenderer = GetComponent<SpriteRenderer>();
   }

   private void Start(){
    InvokeRepeating(nameof(Advance), this.animationTime, this.animationTime );

   }

   private void Advance(){
    // Stop animation when sprite renderer is disabled
    // Test commment
    if(!this.SpriteRenderer.enabled){
        return;
    }   
    
        this.AnimationFrame++;
// Loop sprites back to 0 when last sprite renderd 
        if(this.AnimationFrame >= this.sprites.Length && this.loop){
            this.AnimationFrame = 0;
        }
// Update the sprite
        if(this.AnimationFrame >= 0 && this.AnimationFrame < this.sprites.Length){
            this.SpriteRenderer.sprite = this.sprites[this.AnimationFrame];
        }


   }

   public void Restart(){
    this.AnimationFrame = -1;
    Advance();
   }

}
