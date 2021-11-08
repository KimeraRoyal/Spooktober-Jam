using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spooktober.Screen
{
    [ExecuteInEditMode]
    public class SimpleBlit : MonoBehaviour
    {
        [SerializeField] protected Material m_material;
        
        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, m_material);
        }
    }
}
