using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
using UnityEngine.Events;

namespace UISystem
{
    public abstract partial class UIBase : MonoBehaviour
    {
        //UI显示的GameObject
        public GameObject mUIShowObj = null;
        public eUIType mUIType = eUIType.None;
        public Canvas _mCanvas = null;
        public int iaddoder = 0;
        protected CanvasScaler _mCanvasScaler = null;
        public List<UIBase> CommonUIList = new List<UIBase>(); // 通用Com小UI
        public object refreshFuncData = null;
        public bool refreshFuncCallBack = false;
        /// <summary>
        /// 当前UI脚本类型名
        /// </summary>
        public string uiTypeName;
        /// <summary>
        /// 是否开启UI
        /// </summary>
        public bool isActive = false;
        /// <summary>
        /// 是否切换场景删除
        /// </summary>
        public bool isSwichSceneDestroy = false;
        /// <summary>
        /// 物体层级
        /// </summary>
        public int _miUIOder = 0;
        /// <summary>
        /// 打开时的层级
        /// </summary>
        protected int _perUIOder = -1;

        protected bool _mbShowUILoaded = false;


        //存储gameobject零时数据
        protected Hashtable _mTemporaryData = new Hashtable();
 

        //protected Dictionary<int, EventSoundComp> _mOnClickFunc = new Dictionary<int, EventSoundComp>();

        /// <summary>
        /// 屏蔽板
        /// </summary>
        private Image ShieldPlateImage = null;

        protected Dictionary<int, EventSoundComp> _mOnClickFunc = new Dictionary<int, EventSoundComp>();

        internal void _OnLoadedShowUI()
        {
            _mbShowUILoaded = true;
            _mCanvas = mUIShowObj.GetComponent<Canvas>();
            _mCanvasScaler = mUIShowObj.GetComponent<CanvasScaler>();
            MainManager.UI.SetUICompent(_mCanvas, _mCanvasScaler);
            _InitAllDicCom();
        }

        /// <summary>
        /// 引导功能逻辑接口
        /// </summary>
        public virtual GameObject getGuideObject(string str)
        {
            return null;
        }

        //显示UIprefab加载完毕后调用,所有控件对象的初始化赋值在这里进行
        public abstract void OnAutoLoadedUIObj();
        public abstract void OnLoadedUIObj();
        public abstract void OnAutoRelease();
        public abstract void OnRelease();

        public bool bShowUILoaded
        {
            get { return _mbShowUILoaded; }
        }
        /// <summary>
        /// 清理UI物体层级
        /// </summary>
        public void recoverOder()
        {
            _miUIOder = 0;
            _perUIOder = 0;
            _mCanvas.sortingOrder = 0;
        }

        /// <summary>
        /// 设置UI物体层级
        /// </summary>
        public void setCanvasOder()
        {
            _miUIOder = MainManager.UI.GetCanvasOder(mUIType);
        }

        /// <summary>
        /// 刷新层级让UI预设提层级生效
        /// </summary>
        public void freshCanvasOder()
        {
            _miUIOder = MainManager.UI.GetCanvasOder(mUIType);

            if (_mCanvas != null)
            {
                _mCanvas.sortingOrder = _miUIOder;
            }
            else
            {
                Debug.LogError(gameObject.name + "不存在_mCanvas");
            }

            if (_perUIOder != _miUIOder)
            {
                FreshCanvas();
                _perUIOder = _miUIOder;
            }
        }


        /// <summary>
        /// 得到当前列表层级
        /// </summary>
        public int GetCurrentCanvasOder()
        {
            return _miUIOder;
        }

        public virtual void FreshCanvas()
        {

        }

    //一些功能函数
    public string getTextColorString(string text, string color = "ffffff")
        {
            return "<color=#" + color + ">" + text + "</color>";
        }

        public string getTextImageString(string name, string size)
        {
            return "<quad emoji=" + name + " size=" + size + "/>";
        }

        /// <summary>
        /// 获取字符串宽度
        /// </summary>
        /// <param name="TargetText"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public float getTextWidth(Text TargetText, int size = 0)
        {
            CharacterInfo characterInfo;
            float width = 0f;
            if (size != 0)
                TargetText.font.RequestCharactersInTexture(TargetText.text, size, FontStyle.Normal);
            for (int i = 0; i < TargetText.text.Length; i++)
            {
                if (size != 0)
                    TargetText.font.GetCharacterInfo(TargetText.text[i], out characterInfo, size);
                else
                    TargetText.font.GetCharacterInfo(TargetText.text[i], out characterInfo);

                width += characterInfo.advance;
            }
            return width;
        }

        public void setRectTransformByPosSize(RectTransform setrect, Vector2 pos, Vector2 size)
        {
            int screenw = (int)MainManager.UI.mReferenceResolution.x;
            int screenh = (int)MainManager.UI.mReferenceResolution.y;

            int posx = (int)pos.x;
            int posy = (int)pos.y;

            int sizex = (int)size.x;
            int sizey = (int)size.y;

            float pivx = setrect.pivot.x;
            float pivy = setrect.pivot.y;

            Vector2 offsetmin = new Vector2(screenw / 2.0f + (posx - sizex * pivx), screenh / 2.0f + (posy - sizey * pivy));
            Vector2 offsetmax = new Vector2(-screenw / 2.0f + (posx + sizex * (1 - pivx)), -screenh / 2.0f + (posy + sizey * (1 - pivy)));


            setRectTransformByOffset(setrect, offsetmin, offsetmax);
        }

        public void setTemporaryData(GameObject ob, int index, object data)
        {
            if (_mTemporaryData.ContainsKey(ob.GetInstanceID()))
            {
                List<object> datas = (List<object>)_mTemporaryData[ob.GetInstanceID()];
                InspectCurrency(ref datas, index);
                datas[index] = data;
            }
            else
            {
                List<object> datas = new List<object>();
                InspectCurrency(ref datas, index);
                datas[index] = data;
                _mTemporaryData.Add(ob.GetInstanceID(), datas);
            }
        }

        private void InspectCurrency(ref List<object> datas, int id)
        {
            if (datas.Count <= id)
            {
                int size = id - datas.Count + 1;
                for (int i = 0; i < size; ++i)
                {
                    datas.Add(null);
                }
            }
        }

        /// <summary>
        /// 计算字符串的字符数  英文1个    汉子2个
        /// </summary>在 ASCII码表中，英文的范围是0-127，而汉字则是大于127
        /// <param name="str"></param>
        /// <returns></returns>
        public int CalculateStringChar(string str)
        {
            int charNum = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if ((int)str[i] > 127)
                    charNum += 2;
                else if ((int)str[i] != 0)
                    charNum += 1;
                else
                    charNum += 0;
            }
            return charNum;
        }

        public object getTemporaryData(GameObject ob, int index)
        {
            if (_mTemporaryData.ContainsKey(ob.GetInstanceID()))
            {
                List<object> datas = (List<object>)_mTemporaryData[ob.GetInstanceID()];
                if (index < 0 || index >= datas.Count)
                    return null;

                return datas[index];
            }
            else
            {
                return null;
            }
        }
        public void setRectTransformByOffset(RectTransform setrect, Vector2 offsetmin, Vector2 offsetmax)
        {

            int screenw = (int)MainManager.UI.mReferenceResolution.x;
            int screenh = (int)MainManager.UI.mReferenceResolution.y;

            int minx = (int)offsetmin.x;
            int miny = (int)offsetmin.y;

            int maxx = (int)offsetmax.x;
            int maxy = (int)offsetmax.y;

            Vector2 offsetMin = new Vector2((minx - setrect.anchorMin.x * screenw), (miny - setrect.anchorMin.y * screenh));
            Vector2 offsetMax = new Vector2((maxx + (1 - setrect.anchorMax.x) * screenw), (maxy + (1 - setrect.anchorMax.y) * screenh));

            setrect.offsetMin = offsetMin;
            setrect.offsetMax = offsetMax;
        }

        public void getRectTransformByOffset(RectTransform setrect, out Vector2 offsetmin, out Vector2 offsetmax)
        {

            int screenw = (int)MainManager.UI.mReferenceResolution.x;
            int screenh = (int)MainManager.UI.mReferenceResolution.y;

            offsetmin.x = setrect.offsetMin.x + setrect.anchorMin.x * screenw;
            offsetmin.y = setrect.offsetMin.y + setrect.anchorMin.y * screenh;

            offsetmax.x = setrect.offsetMax.x - (1 - setrect.anchorMax.x) * screenw;
            offsetmax.y = setrect.offsetMax.y - (1 - setrect.anchorMax.y) * screenh;
        }

        //public void OpenShieldPlate()
        //{
        //    if (ShieldPlateImage == null)
        //    {
        //        GameObject imageObj = new GameObject("ShieldPlate");
        //        RectTransform rectImage = imageObj.AddComponent<RectTransform>();
        //        ShieldPlateImage = imageObj.AddComponent<Image>();
        //        imageObj.transform.SetParent(gate.PanelWindow);
        //        rectImage.anchorMax = Vector2.one;
        //        rectImage.anchorMin = Vector2.zero;
        //        rectImage.pivot = Vector2.one * 0.5f;
        //        rectImage.anchoredPosition3D = Vector3.zero;
        //        rectImage.sizeDelta = Vector2.zero;
        //        Color color = ShieldPlateImage.color;
        //        color.a = 0;
        //        ShieldPlateImage.color = color;
        //    }
        //    ShieldPlateImage.gameObject.SetActive(true);
        //}

        public void CloseShieldPlate()
        {
            if (ShieldPlateImage != null)
            {
                ShieldPlateImage.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 显示titel
        /// </summary>
        /// <param name="parent">父节点</param>
        /// <param name="backFunc">返回按钮回调 必须</param>
        /// <param name="helpFunc">帮助按钮回调 null=隐藏 </param>
        /// <param name="closeFunc">关闭按钮回调 null=默认关闭当前所有，回到主界面</param>
        /// <param name="titelName">titel 名字</param>

        public void ShowTitle(Transform parent, string titelName, OnVoidCallBack backFunc, OnVoidCallBack helpFunc = null, OnVoidCallBack closeFunc = null  )
        {
            Com_UiTitle uiTitle = MainManager.UI.createUI<Com_UiTitle>("Com_UiTitle", this);
            uiTitle.transform.SetParent(parent);
            AdaptationByCom(uiTitle.gameObject, parent);
            uiTitle.SetData(titelName, backFunc, helpFunc, closeFunc);

            uiTitle.transform.Reset();
        }
        //添加通用背景
        public void ShowBG(Transform parent)
        {
            Com_commonBG uiBG = MainManager.UI.createUI<Com_commonBG>("Com_commonBG", this);
            uiBG.transform.SetParent(parent);
            //AdaptationByCom(uiTitle.gameObject, parent);

            uiBG.transform.Reset();
        }

        /// <summary>
        /// 公共组件适配
        /// </summary>
        /// <param name="_obj"></param>
        /// <param name="_parent"></param>
        public void AdaptationByCom(GameObject _obj, Transform _parent)
        {
            if (null == _parent)
            {
                UTLog.Error("_parent == null");
                return;
            }

            RectTransform rectTran = _obj.GetComponent<RectTransform>();
            rectTran.sizeDelta = _parent.GetComponent<RectTransform>().sizeDelta;

            RectTransform TagetRectTran = _parent.GetComponent<RectTransform>();
            rectTran.sizeDelta = TagetRectTran.sizeDelta;
            rectTran.anchorMin = TagetRectTran.anchorMin;
            rectTran.anchorMax = TagetRectTran.anchorMax;
            rectTran.offsetMax = TagetRectTran.offsetMax;
            rectTran.offsetMin = TagetRectTran.offsetMin;
            rectTran.pivot = TagetRectTran.pivot;
            rectTran.anchoredPosition3D = TagetRectTran.anchoredPosition3D;
        }
    }

    public abstract partial class UIBase : MonoBehaviour
    {
        public Dictionary<string, GameObject> mDicBtns = new Dictionary<string, GameObject>();
        public Dictionary<string, GameObject> mDicTexts = new Dictionary<string, GameObject>();
        public Dictionary<string, GameObject> mDicImages = new Dictionary<string, GameObject>();



        private void _InitAllDicCom()
        {
            mDicBtns = Util.GetAllCom<Button>(gameObject);
            mDicTexts = Util.GetAllCom<Text>(gameObject);
            mDicImages = Util.GetAllCom<Image>(gameObject);
        }
        /// <summary>
        /// 修改控件为灰色
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="gray"></param>
        public static void ChangeSpriteToGray(Graphic sprite, bool gray)
        {
            if (null == sprite) return;

            // 判断是否非ETC1
            if (null == sprite.material
                || sprite.material.shader.name.Contains("Custom/UI/Gray")
                || sprite.material.shader.name.Contains("UI/DefaultETC1")
                || sprite.material.shader.name.Contains("UI/ETC1SplitAlphaEditorGray"))
            {
                Sprite tmp = sprite.GetComponent<Image>().sprite;
                string grayShaderName = tmp.associatedAlphaSplitTexture != null ? "UI/ETC1SplitAlphaEditorGray" : "Custom/UI/Gray";
                if (gray)
                {
                    Shader tmpSH = Shader.Find(grayShaderName);
                    if (null == tmpSH) UTLog.Error(grayShaderName + "   不存在");
                    sprite.material = new Material(tmpSH);
                }
                else
                    sprite.material = null;
                return;
            }
            // ETC1版本
            string shaderName = sprite.material.shader.name;
            if (shaderName.Contains("UI/ETC1SplitAlpha")
                || shaderName.Contains("SpriteRender/ETC1SplitAlpha"))
            {
                bool isReturn = (shaderName.EndsWith("Gray") == gray);
                if (isReturn) return;
                string normalName = shaderName.Replace("Gray", "");
                if (gray)
                {
                    // 保存现在的材质
                    // 针对静态的Image
                    if (!MainManager.LoadSprite._mdicmat.ContainsKey((Texture2D)sprite.material.mainTexture))
                        MainManager.LoadSprite._mdicmat.Add((Texture2D)sprite.material.mainTexture, sprite.material);
                    Shader tmpSH = Shader.Find(normalName + "Gray");
                    if (null == tmpSH) UTLog.Error(normalName + "Gray" + "   不存在");
                    var tmpMaterial = new Material(tmpSH);
                    tmpMaterial.hideFlags = HideFlags.HideAndDontSave;
                    tmpMaterial.SetTexture("_MyAlphaTex", sprite.material.GetTexture("_MyAlphaTex"));
                    sprite.material = tmpMaterial;
                }
                else
                {
                    Material preMat = null;
                    Image isImg = sprite.GetComponent<Image>();
                    if (null == isImg) return;
                    if (MainManager.LoadSprite._mdicmat.ContainsKey(isImg.sprite.texture))
                    {
                        preMat = MainManager.LoadSprite._mdicmat[isImg.sprite.texture];
                        sprite.material = preMat;
                    }
                    else
                    {
                        UTLog.Error("缓存表没有材质: " + isImg.sprite.texture.name);
                    }
                }
            }

        }

        /// <summary>
        /// 修改控件为高亮
        /// </summary>
        public static void ChangeSpriteToBright(Graphic sprite, bool isBright, float bright = 1.5f)
        {
            if (null == sprite) return;

            // 判断是否非ETC1
            if (null == sprite.material
                || sprite.material.shader.name.Contains("Custom/UI/Bright")
                || sprite.material.shader.name.Contains("UI/DefaultETC1")
                || sprite.material.shader.name.Contains("UI/ETC1SplitAlphaEditorBright"))
            {
                Sprite tmp = sprite.GetComponent<Image>().sprite;
                string grayShaderName = tmp.associatedAlphaSplitTexture != null ? "UI/ETC1SplitAlphaEditorBright" : "Custom/UI/Bright";
                if (isBright)
                {
                    Shader tmpSH = Shader.Find(grayShaderName);
                    if (null == tmpSH) UTLog.Error(grayShaderName + "   不存在");
                    sprite.material = new Material(tmpSH);
                    sprite.material.SetFloat("_Bright", bright);
                }
                else
                    sprite.material = null;
                return;
            }
            // ETC1版本
            string shaderName = sprite.material.shader.name;
            if (shaderName.Contains("UI/ETC1SplitAlpha")
                || shaderName.Contains("SpriteRender/ETC1SplitAlpha"))
            {
                bool isReturn = (shaderName.EndsWith("Bright") == isBright);
                if (isReturn) return;
                string normalName = shaderName.Replace("Bright", "");
                if (isBright)
                {
                    // 保存现在的材质
                    // 针对静态的Image
                    if (!MainManager.LoadSprite._mdicmat.ContainsKey((Texture2D)sprite.material.mainTexture))
                        MainManager.LoadSprite._mdicmat.Add((Texture2D)sprite.material.mainTexture, sprite.material);
                    Shader tmpSH = Shader.Find(normalName + "Bright");
                    if (null == tmpSH) UTLog.Error(normalName + "Bright" + "   不存在");
                    var tmpMaterial = new Material(tmpSH);
                    tmpMaterial.hideFlags = HideFlags.HideAndDontSave;
                    tmpMaterial.SetFloat("Brightness", bright);
                    tmpMaterial.SetTexture("_MyAlphaTex", sprite.material.GetTexture("_MyAlphaTex"));
                    sprite.material = tmpMaterial;
                }
                else
                {
                    Material preMat = null;
                    Image isImg = sprite.GetComponent<Image>();
                    if (null == isImg) return;
                    if (MainManager.LoadSprite._mdicmat.ContainsKey(isImg.sprite.texture))
                    {
                        preMat = MainManager.LoadSprite._mdicmat[isImg.sprite.texture];
                        sprite.material = preMat;
                    }
                    else
                    {
                        UTLog.Error("缓存表没有材质: " + isImg.sprite.texture.name);
                    }
                }
            }

        }
        public static void ChangeSpriteToGray(bool gray, params Graphic[] sprites)
        {
            foreach (var v in sprites)
            {
                ChangeSpriteToGray(v, gray);
            }
        }
        
        protected bool CheckCurUIActive()
        {
            return MainManager.UI.checkTargetUIActive(this.uiTypeName);
        }
        protected void SetComListState<T>(List<T> clearlist, bool state = false) where T : UIBase
        {
            if (clearlist == null)
                return;
            for (int i = 0; i < clearlist.Count; ++i)
            {
                if (clearlist[i] == null)
                    continue;
                clearlist[i].gameObject.SetActive(state);
            }
        }

        public void setOnClick(EventTriggerListener.PointerEventDelegateObj onclick, GameObject obj, string DefaultSound = "")
        {
            if ("" == DefaultSound)
            {
                DefaultSound = MainManager.Music.GetPlayVoiceName("notice_ui_button");
            }
            int key = obj.GetInstanceID();
            if (!_mOnClickFunc.ContainsKey(key))
            {
                _mOnClickFunc.Add(obj.GetInstanceID(), new EventSoundComp(DefaultSound, onclick));
                EventTriggerListener.Get(obj).onObjClick += OnClick;
            }
        }

        public void setOnClick(EventTriggerListener.PointerEventDelegate onclick, GameObject obj, string DefaultSound = "")
        {
            if ("" == DefaultSound)
            {
                DefaultSound = MainManager.Music.GetPlayVoiceName("notice_ui_button");
            }
            int key = obj.GetInstanceID();
            if (!_mOnClickFunc.ContainsKey(key))
            {
                _mOnClickFunc.Add(obj.GetInstanceID(), new EventSoundComp(DefaultSound, null, onclick));
                EventTriggerListener.Get(obj).onObjClick += OnClick;
            }
        }

        public void ReleaseClick(GameObject obj)
        {
            EventTriggerListener.Get(obj).onObjClick -= OnClick;
            int key = obj.GetInstanceID();
            if (_mOnClickFunc.ContainsKey(key))
            {
                _mOnClickFunc.Remove(obj.GetInstanceID());
            }
        }

        private void OnClick(PointerEventData eventData, GameObject obj)
        {
            int key = obj.GetInstanceID();
            if (_mOnClickFunc.ContainsKey(key))
            {
                if (_mOnClickFunc[key] != null)
                {
                    MainManager.Music.Play(MainManager.Pool.GetRes<AudioClip>(_mOnClickFunc[key]._soundName));
                    if (_mOnClickFunc[key]._onClick != null) _mOnClickFunc[key]._onClick(eventData);
                    if (_mOnClickFunc.Count == 0) return;
                    if (_mOnClickFunc[key]._onObjClick != null) _mOnClickFunc[key]._onObjClick(eventData, obj);
                }
            }
        }

    }
}



