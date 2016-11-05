using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Fade : FadeVariables {
    private float fScaleChangeVolume;// 大きさ変化量

    private Image image;
    private Vector2 vecSize;

    public Canvas canvas;
	FadeVariables Variables;
	RectTransform rt;

    // Use this for initialization
    void Start () {
		Variables = canvas.GetComponent<FadeVariables>();
        rt = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        fSizeLimit = Variables.fSizeLimit;
        FadeMode = Variables.FadeMode;          // フェードモード
        FadeVolume = Variables.FadeVolume;        // アルファ値変化量
		fScaleChangeVolume = PublicScaleChangeVolume = Variables.PublicScaleChangeVolume * 100.0f;// 大きさ変化量

        Variables.bFading = true;
        if(FadeMode == eFADEMODE.FadeIn) {
            Variables.fAlpha = 255.0f;
            vecSize = new Vector2(0.0f,0.0f);
        } else {
            //Variables.fAlpha = 0.0f;
            vecSize = new Vector2(fSizeLimit, fSizeLimit * HEIGHT_CORRECTION);
        }
        var color = image.color;
        color.a = Variables.fAlpha;
        image.color = color;
    }

    // 使い始めるとき
    public void CreateFade(eFADEMODE fadeMode = eFADEMODE.FadeIn) {
        FadeMode = fadeMode;
        Variables.bFading = true;

        if(FadeMode == eFADEMODE.FadeIn) {
            Variables.fAlpha = 255.0f;
            vecSize = new Vector2(0.0f, 0.0f);
        } else {
            Variables.fAlpha = 0.0f;
            vecSize = new Vector2(fSizeLimit, fSizeLimit * HEIGHT_CORRECTION);
        }
        var color = image.color;
        color.a = Variables.fAlpha;
        image.color = color;
    }
	
	// Update is called once per frame
	void Update () {
        if(Variables.bFading) {
            switch(FadeMode) {
                case eFADEMODE.FadeIn:
                    vecSize.x += fScaleChangeVolume * Time.deltaTime;
                    vecSize.y += fScaleChangeVolume * HEIGHT_CORRECTION * Time.deltaTime;
                    if(vecSize.x > fSizeLimit || vecSize.y > fSizeLimit * HEIGHT_CORRECTION) { 
                        vecSize = new Vector2(fSizeLimit, fSizeLimit * HEIGHT_CORRECTION);
                        Variables.bFading = false;
                    }
                    if(vecSize.x > ScreenWidth) {
                        Variables.fAlpha -= FadeVolume * 3 * Time.deltaTime;
                    } else {
                        Variables.fAlpha -= FadeVolume * Time.deltaTime;
                    }
                    if(Variables.fAlpha <0.0f) {
                        Variables.fAlpha = 0.0f;
                    }

                    break;
                case eFADEMODE.FadeOut:
                    vecSize.x -= fScaleChangeVolume * Time.deltaTime;
                    vecSize.y -= fScaleChangeVolume * HEIGHT_CORRECTION * Time.deltaTime;
                    if(vecSize.x < 0.0f || vecSize.y < 0.0f) {
                        vecSize = new Vector2(0.0f,0.0f);
                        Variables.bFading = false;
                    }
                    Variables.fAlpha += FadeVolume * Time.deltaTime;
                    if(Variables.fAlpha > 255.0f) {
                        Variables.fAlpha = 255.0f;
                    }

                    break;
                default:
                    break;
            }
            rt.sizeDelta = vecSize;
            var color = image.color;
            color.a = Variables.fAlpha / 255.0f;
            image.color = color;
        }
	}

    public bool GetFading() {
        return Variables.bFading;
    }
}
