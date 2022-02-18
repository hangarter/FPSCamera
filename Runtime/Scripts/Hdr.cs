using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hdr : MonoBehaviour
{
    void Start()
    {
        HDROutputSettings.main.automaticHDRTonemapping = true;
    }
}
