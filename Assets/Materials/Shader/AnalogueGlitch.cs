using UnityEngine;
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Glitch/Analogue Glitch")]
public class AnalogueGlitch : MonoBehaviour
{
    #region Public Properties
    [SerializeField, Range(0, 1)]
    float _scanLineJitter = 0;
    public float ScanLineJitter
    {
        get { return _scanLineJitter; }
        set { _scanLineJitter = value; }
    }
    [SerializeField, Range(0, 1)]
    float _verticalJump = 0;
    public float VerticalJump
    {
        get { return _verticalJump; }
        set { _verticalJump = value; }
    }
    [SerializeField, Range(0, 1)]
    float _horizontalShake = 0;
    public float HorizontalShake
    {
        get { return _horizontalShake; }
        set { _horizontalShake = value; }
    }
    [SerializeField, Range(0, 1)]
    float _colourDrift = 0;
    public float ColourDrift
    {
        get { return _colourDrift; }
        set { _colourDrift = value; }
    }
    #endregion
    #region Private Properties
    //ATTACH THIS TO THE SCRIPT BEFORE ADDING IT TO THE CAMERA
    [SerializeField] Shader _shader;
    Material _material;
    float _verticalJumpTime;
    #endregion
    #region MonoBehaviour Functions
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(_material == null)
        {
            _material = new Material(_shader);
            _material.hideFlags = HideFlags.DontSave;
        }

        _verticalJumpTime += Time.deltaTime * _verticalJump * 11.3f;
        var sl_thresh = Mathf.Clamp01(1.0f - _scanLineJitter*1.2f);
        var sl_disp = 0.002f + Mathf.Pow(_scanLineJitter, 3) * 0.05f;
        //ANALOGUE GLITCH SHADER SCRIPT
        _material.SetVector("_ScanLineJitter", new Vector2(sl_disp, sl_thresh));
        var vj = new Vector2(_verticalJump, _verticalJumpTime);
        //ANALOGUE GLITCH SHADER SCRIPT
        _material.SetVector("_VerticalJump", vj);
        //ANALOGUE GLITCH SHADER SCRIPT
        _material.SetFloat("_HorizontalShake",_horizontalShake*0.2f);
        var cd = new Vector2(_colourDrift * 0.04f, Time.time * 606.11f);
        //ANALOGUE GLITCH SHADER SCRIPT
        _material.SetVector("_ColourDrift", cd);
        Graphics.Blit(source, destination, _material);

    }

    #endregion
}
