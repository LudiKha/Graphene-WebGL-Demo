using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graphene.Demo
{
  public class TouchPad : MonoBehaviour
  {
    public Transform cube;
    public DemoCubeViewModel viewModel;

    public float speed;

    private void OnMouseDrag()
    {
      float x = Input.GetAxis("Mouse X");
      float y = Input.GetAxis("Mouse Y");
      cube.Rotate(y * speed * Time.deltaTime, -x * speed * Time.deltaTime, 0, Space.World);

      viewModel.UpdateViewModel();
    }
  }
}