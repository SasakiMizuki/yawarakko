//=========================================
/*
  取り扱い説明書
☆ボタンを取得するときはMultiInput.Instance.GetPressButton()に
  MultiInput.CONTROLLER_BUTTON列挙型を引数に入れて呼んでください。
☆スティックは-1.0～1.0のfloatで、その他ボタンはboolで返ってきます。

☆パブリックの変数にはキーを入れてください。
☆パッドによってボタン配置が違うかもしれないです。
  (エレコムのJC-U3312Sを基準としています)
  その時はお手数ですがStartでpadDictionalyに入れているキーコードを
  変えてください(ボタン1がKeyCode.Joystick1Button0です)。

☆十字キーと右スティックが反応しない時はProjectSettingsのInputのSizeを22にして以下の4つを追加してください。
Name:HorizontalRight
DescriptiveNameからAltPositiveButtonまで空白
Gravity:0
Dead:0.19
Sensitivity:1
Type:JoystickAxis
Axis:3rd axis

Name:VerticalRight
DescriptiveNameからAltPositiveButtonまで空白
Gravity:0
Dead:0.19
Sensitivity:1
Type:JoystickAxis
Axis:4th axis

Name:GamePadHorizontal
DescriptiveNameからAltPositiveButtonまで空白
Gravity:3
Dead:0.001
Sensitivity:3
Type:JoystickAxis
Axis:5th axis

Name:GamePadVertical
DescriptiveNameからAltPositiveButtonまで空白
Gravity:3
Dead:0.001
Sensitivity:3
Type:JoystickAxis
Axis:6th axis

PS.頭悪いプログラムでごめーんね☆
*/
//=========================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiInput : SingletonMonoBehaviour<MultiInput> {
    public enum CONTROLLER_BUTTON {
        CLOSS_UP,        // 十字キー上
        CLOSS_DOWN,      // 十字キー下
        CLOSS_LEFT,      // 十字キー左
        CLOSS_RIGHT,     // 十字キー右
        CIRCLE,          // ○ボタン(Aボタン)
        CANCEL,          // ×ボタン(Bボタン)
        TRIANGLE,        // △ボタン(Xボタン)
        SQUARE,          // □ボタン(Yボタン)
        RIGHT_1,         // R1ボタン
        RIGHT_2,         // R2ボタン
        RIGHT_3,         // R3ボタン
        LEFT_1,          // L1ボタン
        LEFT_2,          // L2ボタン
        LEFT_3,          // L3ボタン  
        RIGHTSTICK_UP,   // 右スティック上
        RIGHTSTICK_DOWN, // 右スティック下
        RIGHTSTICK_LEFT, // 右スティック左
        RIGHTSTICK_RIGHT,// 右スティック右 
        LEFTSTICK_UP,    // 左スティック上
        LEFTSTICK_DOWN,  // 左スティック下
        LEFTSTICK_LEFT,  // 左スティック左
        LEFTSTICK_RIGHT, // 左スティック右
    };
    [Header("空白にするとたぶんバグるので何かしら入れておいてください")]
    [Header("ボタン名はDUALSHOCK基準です")]
    [Header("各ボタンに合わせたいキーを入れてください。")]
    public string ButtonClossUp;    // 十字キー上
	public string ButtonClossDown;	// 十字キー下
    public string ButtonClossLeft;	// 十字キー左
    public string ButtonClossRight;	// 十字キー右
    public string ButtonCircle;		// ○ボタン(Aボタン)
    public string ButtonCancel;		// ×ボタン(Bボタン)
    public string ButtonTriangle;	// △ボタン(Xボタン)
    public string ButtonSquare;		// □ボタン(Yボタン)
    public string ButtonR1;			// R1ボタン
    public string ButtonR2;			// R2ボタン
    public string ButtonR3;			// R3ボタン
    public string ButtonL1;			// L1ボタン
    public string ButtonL2;			// L2ボタン
    public string ButtonL3;			// L3ボタン  
    public string StickR_Up;		// 右スティック上
    public string StickR_Down;		// 右スティック下
    public string StickR_Left;		// 右スティック左
    public string StickR_Right;		// 右スティック右 
    public string StickL_Up;		// 左スティック上
    public string StickL_Down;		// 左スティック下
    public string StickL_Left;		// 左スティック左
    public string StickL_Right;		// 左スティック右

    private Dictionary<CONTROLLER_BUTTON, string> keyDictionary;
    private Dictionary<CONTROLLER_BUTTON, KeyCode> padDictionary;

    private bool[] BeforeClossButton;
    // Use this for initialization
    void Start () {
        // きーぼーど
        keyDictionary = new Dictionary<CONTROLLER_BUTTON, string>()
        {
            {CONTROLLER_BUTTON.CLOSS_UP,   ButtonClossUp},
            {CONTROLLER_BUTTON.CLOSS_DOWN, ButtonClossDown},
            {CONTROLLER_BUTTON.CLOSS_LEFT, ButtonClossLeft},
            {CONTROLLER_BUTTON.CLOSS_RIGHT,ButtonClossRight},
            {CONTROLLER_BUTTON.CIRCLE,     ButtonCircle},
            {CONTROLLER_BUTTON.CANCEL,     ButtonSquare},
            {CONTROLLER_BUTTON.TRIANGLE,   ButtonTriangle},
            {CONTROLLER_BUTTON.SQUARE,     ButtonCancel},
            {CONTROLLER_BUTTON.RIGHT_1,    ButtonR1},
            {CONTROLLER_BUTTON.RIGHT_2,    ButtonR2},
            {CONTROLLER_BUTTON.RIGHT_3,    ButtonR3},
            {CONTROLLER_BUTTON.LEFT_1,     ButtonL1},
            {CONTROLLER_BUTTON.LEFT_2,     ButtonL2},
            {CONTROLLER_BUTTON.LEFT_3,     ButtonL3},
            {CONTROLLER_BUTTON.RIGHTSTICK_UP,      StickR_Up},
            {CONTROLLER_BUTTON.RIGHTSTICK_DOWN,    StickR_Down},
            {CONTROLLER_BUTTON.RIGHTSTICK_LEFT,    StickR_Left},
            {CONTROLLER_BUTTON.RIGHTSTICK_RIGHT,   StickR_Right},
            {CONTROLLER_BUTTON.LEFTSTICK_UP,       StickL_Up},
            {CONTROLLER_BUTTON.LEFTSTICK_DOWN,     StickL_Down},
            {CONTROLLER_BUTTON.LEFTSTICK_LEFT,     StickL_Left},
            {CONTROLLER_BUTTON.LEFTSTICK_RIGHT,    StickL_Right},
        };

		// げーむぱっど(Joystick1Button19は対応するボタンがなかったので仮で入れてあるものです)
		padDictionary = new Dictionary<CONTROLLER_BUTTON, KeyCode>()
        {
            {CONTROLLER_BUTTON.CLOSS_UP,   KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.CLOSS_DOWN, KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.CLOSS_LEFT, KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.CLOSS_RIGHT,KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.CIRCLE,     KeyCode.Joystick1Button3},
            {CONTROLLER_BUTTON.CANCEL,     KeyCode.Joystick1Button2},
            {CONTROLLER_BUTTON.TRIANGLE,   KeyCode.Joystick1Button1},
            {CONTROLLER_BUTTON.SQUARE,     KeyCode.Joystick1Button0},
            {CONTROLLER_BUTTON.RIGHT_1,    KeyCode.Joystick1Button5},
            {CONTROLLER_BUTTON.RIGHT_2,    KeyCode.Joystick1Button7},
            {CONTROLLER_BUTTON.RIGHT_3,    KeyCode.Joystick1Button11},
            {CONTROLLER_BUTTON.LEFT_1,     KeyCode.Joystick1Button4},
            {CONTROLLER_BUTTON.LEFT_2,     KeyCode.Joystick1Button6},
            {CONTROLLER_BUTTON.LEFT_3,     KeyCode.Joystick1Button10},
            {CONTROLLER_BUTTON.RIGHTSTICK_UP,      KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.RIGHTSTICK_DOWN,    KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.RIGHTSTICK_LEFT,    KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.RIGHTSTICK_RIGHT,   KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.LEFTSTICK_UP,       KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.LEFTSTICK_DOWN,     KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.LEFTSTICK_LEFT,     KeyCode.Joystick1Button19},
            {CONTROLLER_BUTTON.LEFTSTICK_RIGHT,    KeyCode.Joystick1Button19},
        };

        BeforeClossButton = new bool[4];
    }                                              
	                                               
	// Update is called once per frame   
    void Update() {
    }        
                  
	void LateUpdate () {
		// 現在フレームで入力されているかの確認(TriggerとRelease用)
        for(int i = 0; i < BeforeClossButton.Length; i++) {
            BeforeClossButton[i] = GetPressButton((CONTROLLER_BUTTON)i);
        }
    }

	// 以下コメントなし。質問のある方はこちらまで→0120828828
			
    public bool GetPressButton(CONTROLLER_BUTTON button) {
        if(button > CONTROLLER_BUTTON.CLOSS_RIGHT) {
            if(Input.GetKey(keyDictionary[button]) || Input.GetKey(padDictionary[button])) {
                return true;
            }
        } else {
            if(button == CONTROLLER_BUTTON.CLOSS_UP) {
                if(Input.GetKey(keyDictionary[button]) || Input.GetAxisRaw("GamePadVertical") == 1.0f) {
                    return true;
                }
            } else if(button == CONTROLLER_BUTTON.CLOSS_DOWN) {
                if(Input.GetKey(keyDictionary[button]) || Input.GetAxisRaw("GamePadVertical") == -1.0f) {
                    return true;
                }
            }else if(button == CONTROLLER_BUTTON.CLOSS_RIGHT) {
                if(Input.GetKey(keyDictionary[button]) || Input.GetAxisRaw("GamePadHorizontal") == 1.0f) {
                    return true;
                }
            } else {
                if(Input.GetKey(keyDictionary[button]) || Input.GetAxisRaw("GamePadHorizontal") == -1.0f) {
                    return true;
                }
            }
        }


        return false;
    }
    public bool GetTriggerButton(CONTROLLER_BUTTON button) {
        if(button > CONTROLLER_BUTTON.CLOSS_RIGHT) {
            if(Input.GetKeyDown(keyDictionary[button]) || Input.GetKeyDown(padDictionary[button])) {
                return true;
            }
        } else {
            if(button == CONTROLLER_BUTTON.CLOSS_UP) {
                if(Input.GetKey(keyDictionary[button]) || Input.GetAxisRaw("GamePadVertical") == 1.0f) {
                    if(!BeforeClossButton[0])
                        return true;
                }
            } else if(button == CONTROLLER_BUTTON.CLOSS_DOWN) {
                if(Input.GetKey(keyDictionary[button]) || Input.GetAxisRaw("GamePadVertical") == -1.0f) {
                    if(!BeforeClossButton[1])
                        return true;
                }
            } else if(button == CONTROLLER_BUTTON.CLOSS_RIGHT) {
                if(Input.GetKey(keyDictionary[button]) || Input.GetAxisRaw("GamePadHorizontal") == 1.0f) {
                    if(!BeforeClossButton[3])
                        return true;
                }
            } else {
                if(Input.GetKey(keyDictionary[button]) || Input.GetAxisRaw("GamePadHorizontal") == -1.0f) {
                    if(!BeforeClossButton[2])
                        return true;
                }
            }
        }

        return false;
    }
    public bool GetReleaseButton(CONTROLLER_BUTTON button) {
        if(Input.GetKeyUp(keyDictionary[button]) || Input.GetKeyUp(padDictionary[button])) {
            return true;
        }
        if(button > CONTROLLER_BUTTON.CLOSS_RIGHT) {
            if(Input.GetKey(keyDictionary[button]) || Input.GetKey(padDictionary[button])) {
                return true;
            }
        } else {
            if(button == CONTROLLER_BUTTON.CLOSS_UP) {
                if(!Input.GetKey(keyDictionary[button]) && !(Input.GetAxisRaw("GamePadVertical") == 1.0f)) {
                    if(BeforeClossButton[0])
                        return true;
                }
            } else if(button == CONTROLLER_BUTTON.CLOSS_DOWN) {
                if(!Input.GetKey(keyDictionary[button]) && !(Input.GetAxisRaw("GamePadVertical") == -1.0f)) {
                    if(BeforeClossButton[1])
                        return true;
                }
            } else if(button == CONTROLLER_BUTTON.CLOSS_RIGHT) {
                if(!Input.GetKey(keyDictionary[button]) && !(Input.GetAxisRaw("GamePadHorizontal") == 1.0f)) {
                    if(BeforeClossButton[3])
                        return true;
                }
            } else {
                if(!Input.GetKey(keyDictionary[button]) && !(Input.GetAxisRaw("GamePadHorizontal") == -1.0f)) {
                    if(BeforeClossButton[2])
                        return true;
                }
            }
        }

        return false;
    }
    public Vector2 GetLeftStickAxis() {
        Vector2 vec;
        if(GetPressButton(CONTROLLER_BUTTON.LEFTSTICK_RIGHT)) {
            vec.x = 1.0f;
        } else if(GetPressButton(CONTROLLER_BUTTON.LEFTSTICK_LEFT)) {
            vec.x = -1.0f;
        } else {
            vec.x = Input.GetAxisRaw("Horizontal");
        }
        if(GetPressButton(CONTROLLER_BUTTON.LEFTSTICK_UP)) {
            vec.y = 1.0f;
        } else if(GetPressButton(CONTROLLER_BUTTON.LEFTSTICK_DOWN)) {
            vec.y = -1.0f;
        } else {
            vec.y = Input.GetAxisRaw("Vertical");
        }
        return vec;
    }
    public Vector2 GetRightStickAxis() {
        Vector2 vec;
        if(GetPressButton(CONTROLLER_BUTTON.RIGHTSTICK_RIGHT)) {
            vec.x = 1.0f;
        } else if(GetPressButton(CONTROLLER_BUTTON.RIGHTSTICK_LEFT)) {
            vec.x = -1.0f;
        } else {
            vec.x = Input.GetAxisRaw("HorizontalRight");
        }
        if(GetPressButton(CONTROLLER_BUTTON.RIGHTSTICK_UP)) {
            vec.y = 1.0f;
        } else if(GetPressButton(CONTROLLER_BUTTON.RIGHTSTICK_DOWN)) {
            vec.y = -1.0f;
        } else {
            vec.y = Input.GetAxisRaw("VerticalRight");
        }

        return vec;
    }
}
