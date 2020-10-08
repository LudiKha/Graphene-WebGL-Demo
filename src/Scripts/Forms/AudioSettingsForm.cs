
using UnityEngine;
using UnityEngine.UIElements;

namespace Graphene.Demo
{
  [CreateAssetMenu(menuName = "UI/Forms/AudioSettingsForm")]
  public class AudioSettingsForm : Form
  {
    [Draw, BindFloat("", 0.5f, 0,1, "Master Volume")]
    public float MasterVolume = 0.5f;

    [Draw, BindFloat("", 0.5f, 0, 1, "SFX Volume")]
    public float SFXVolume;
    [Draw, BindFloat("", 0.5f, 0, 1, "Music Volume")]
    public float MusicVolume;



    public override void Initialize(VisualElement container, Plate plate)
    {
    }

  }
}
