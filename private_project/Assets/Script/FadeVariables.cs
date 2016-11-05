using UnityEngine;
using System.Collections;

public class FadeVariables : MonoBehaviour {
    public const float ScreenWidth = 1024.0f;
    public const float ScreenHeight = 576.0f;
    public const float HEIGHT_CORRECTION = (1.0f / ScreenWidth) * ScreenHeight;

    public enum eFADEMODE {
        FadeIn,
        FadeOut
    };

    public float fSizeLimit = 70.0f;

    public eFADEMODE FadeMode = 0;          // フェードモード
    public float FadeVolume = 10.0f;        // アルファ値変化量
    public float PublicScaleChangeVolume = 0.01f;// 大きさ変化量
    public bool bFading;
    public float fAlpha;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
