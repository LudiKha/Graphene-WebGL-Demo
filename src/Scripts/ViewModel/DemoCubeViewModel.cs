using Graphene;
using Graphene.Demo;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class DemoCubeViewModel : MonoBehaviour, IModel
{
  [SerializeField] Transform cube;
  [SerializeField] Material material;

  [Draw(ControlType.SubTitle), Bind(BindingMode.OneTime)]
  public string rotation = "Rotation";

  [Draw, BindFloat("", 0.5f, 0, 1, "X Rotation")]
  public BindableFloat RotationX;
  [Draw, BindFloat("", 0.5f, 0, 1, "Y Rotation")]
  public BindableFloat RotationY;
  [Draw, BindFloat("", 0.5f, 0, 1, "Z Rotation")]
  public BindableFloat RotationZ;

  [Draw(ControlType.SubTitle), Bind(BindingMode.OneTime)]
  public string color = "Color";
  [Draw]
  public BindableFloat ColorR;
  [Draw]
  public BindableFloat ColorG;
  [Draw]
  public BindableFloat ColorB;

  private string hex = "#FFAE00";
  [Draw(ControlType.TextField), BindBaseField("", "Hex Color")]
  public string HexCode { get => hex; set => SetColor(value); }

  [field: SerializeField]
  public bool Render { get; set; } = true;

  public Action onModelChange { get; set; }

  public void Initialize(VisualElement container, Plate plate)
  {
    plate.onShow.AddListener(Plate_onShow);
    plate.onHide.AddListener(Plate_onHide);

    RotationX.OnValueChange += RotationX_OnValueChange;
    RotationY.OnValueChange += RotationY_OnValueChange;
    RotationZ.OnValueChange += RotationZ_OnValueChange;

    ColorR.OnValueChange += ColorR_OnValueChange;
    ColorG.OnValueChange += ColorG_OnValueChange;
    ColorB.OnValueChange += ColorB_OnValueChange;

    var renderer = cube.GetComponent<UnityEngine.Renderer>();
    material = Instantiate(renderer.material);
    renderer.material = material;
  }

  private void RotationX_OnValueChange(object sender, float e) => SetRotation(360 * e);

  private void RotationY_OnValueChange(object sender, float e) => SetRotation(null, 360 * e);

  private void RotationZ_OnValueChange(object sender, float e) => SetRotation(null, null, 360 * e);

  private void SetRotation(float? x = null, float? y = null, float? z = null)
  {
    Vector3 oldRot = cube.rotation.eulerAngles;
    Vector3 newRot = new Vector3(x.HasValue ? x.Value : oldRot.x, y.HasValue ? y.Value : oldRot.y, z.HasValue ? z.Value : oldRot.z);
    cube.rotation = Quaternion.Euler(newRot);
  }

  private void ColorR_OnValueChange(object sender, float e) => SetColor(e);
  private void ColorG_OnValueChange(object sender, float e) => SetColor(null, e);
  private void ColorB_OnValueChange(object sender, float e) => SetColor(null, null, e);

  void SetColor(float? r = null, float? g = null, float? b = null)
  {
    Color oldCol = material.color;
    Color newCol = new Color(r ?? oldCol.r, g ?? oldCol.g, b ?? oldCol.b);
    material.color = newCol;
    hex = $"#{ColorUtility.ToHtmlStringRGB(newCol)}";

    onModelChange?.Invoke();
  }

  void SetColor (string hex)
  {
    if (hex.IndexOf('#') != 0)
      hex.Insert(0, "#");

    this.hex = hex;
    if(ColorUtility.TryParseHtmlString(hex, out Color col))
    {      
      material.color = col;
      ColorR.SetValueWithoutNotify(col.r);
      ColorG.SetValueWithoutNotify(col.g);
      ColorB.SetValueWithoutNotify(col.b);
    }

    onModelChange?.Invoke();
  }

  void Plate_onShow() => cube.gameObject.SetActive(true);

  void Plate_onHide() => cube.gameObject.SetActive(false);
}
