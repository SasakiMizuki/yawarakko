using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeSide : FadeVariables {
    public enum ThisPosition {
        UP,
        DOWN,
        LEFT,
        RIGHT
    };
    public ThisPosition thisPosition;
    public Canvas canvas;
    private float MoveVolume;
    private Vector2 MoveDirection;
    private Vector2 vecPosition;
    private float fChangeVol;
    private Image image;

    // Use this for initialization
    void Start () {
        switch(thisPosition) {
            case ThisPosition.UP:
                MoveDirection = new Vector2(0.0f, 1.0f);
                break;
            case ThisPosition.DOWN:
                MoveDirection = new Vector2(0.0f, -1.0f);
                break;
            case ThisPosition.LEFT:
                MoveDirection = new Vector2(-1.0f, 0.0f);
                break;
            case ThisPosition.RIGHT:
                MoveDirection = new Vector2(1.0f, 0.0f);
                break;
        }
        MoveVolume = canvas.GetComponent<FadeVariables>().PublicScaleChangeVolume / 2;

        if(canvas.GetComponent<FadeVariables>().FadeMode == eFADEMODE.FadeOut)
            vecPosition = new Vector2(0.5f * canvas.GetComponent<FadeVariables>().fSizeLimit * MoveDirection.x + ScreenWidth * 0.25f * MoveDirection.x,
                                      0.5f * canvas.GetComponent<FadeVariables>().fSizeLimit * HEIGHT_CORRECTION * MoveDirection.y + ScreenHeight * 0.25f * MoveDirection.y);
        else
            vecPosition = new Vector2(ScreenWidth * 0.25f * MoveDirection.x, ScreenHeight * 0.25f * MoveDirection.y);

        fChangeVol = canvas.GetComponent<FadeVariables>().PublicScaleChangeVolume * 100.0f * 0.5f;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        //if(canvas.GetComponent<FadeVariables>().bFading) {
            if(canvas.GetComponent<FadeVariables>().FadeMode == eFADEMODE.FadeOut) {
                vecPosition.x -= fChangeVol * MoveDirection.x * Time.deltaTime;
                vecPosition.y -= fChangeVol * HEIGHT_CORRECTION * MoveDirection.y * Time.deltaTime;
                if(vecPosition.x * MoveDirection.x <= ScreenWidth * 0.25f && vecPosition.y * MoveDirection.y <= ScreenHeight * 0.25f) {
                    vecPosition = new Vector2(ScreenWidth * 0.25f * MoveDirection.x, ScreenHeight * 0.25f * MoveDirection.y);
                }
            } else {
                vecPosition.x += fChangeVol * MoveDirection.x * Time.deltaTime;
                vecPosition.y += fChangeVol * HEIGHT_CORRECTION * MoveDirection.y * Time.deltaTime;
            if(vecPosition.x * MoveDirection.x >= ScreenWidth * 0.75f || vecPosition.y * MoveDirection.y >= ScreenHeight * 0.75f) {
                vecPosition = new Vector2(ScreenWidth * 0.75f * MoveDirection.x, ScreenHeight * 0.75f * MoveDirection.y);
            }
        }

            transform.localPosition = new Vector3(vecPosition.x, vecPosition.y, 0.0f);
       // }
        var color = image.color;
        color.a = canvas.GetComponent<FadeVariables>().fAlpha / 255.0f;
        image.color = color;
    }
}
