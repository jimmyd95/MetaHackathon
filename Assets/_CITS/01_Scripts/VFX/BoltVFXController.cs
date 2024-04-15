using UnityEngine;
using UnityEngine.VFX;

public class BoltVFXController : MonoBehaviour
{
    [SerializeField] VisualEffect _vfx;
    [SerializeField] Color _lightningColor; // the colour of the lightning line
    [SerializeField] Color _orbColor; // the orb shinning from the top and the bottom
    [SerializeField] Vector3 _particleVelocity; // speed of the orb moving as the lighting grows
    [SerializeField] Vector2 _time; // the time it takes for the entire VFX operation to complete

    // Update is called once per frame
    void Update()
    {
        
        // // give vfx the colour of the lightning line
        setVFXColour();
    }

    private void setVFXColour(){
        // give vfx the colour of the lightning line
        _vfx.SetVector4("LightningColour", _lightningColor);
        _vfx.SetVector4("OrbColour", _orbColor);
    }

    private void setVFXTime(){
        _vfx.SetVector2("Time", _time);
    }

    private void setVFXVelocity(){
        _vfx.SetVector3("ParticleVelocity", _particleVelocity);
    }
}
