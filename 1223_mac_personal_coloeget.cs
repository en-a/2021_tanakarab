using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Colorget : MonoBehaviour
{
    public Image image;
    public Image image2;

    public GameObject cameraPanel;
    public GameObject choicePanel;

    public Text text_r;
    public Text text_g;
    public Text text_b;

    public static int width;
    public static int height;

    public static int rsum;
    public static int gsum;
    public static int bsum;
    public static int rav;
    public static int gav;
    public static int bav;

    public static float k;
    public static float l;
    public static float n;
    public static float j;

    int i = -1;
    int[] rlist = new int[3];
    int[] glist = new int[3];
    int[] blist = new int[3];

    void Start()
    {
        Debug.Log("Screen width : " + Screen.width);
        Debug.Log("screen height : " + Screen.height);

        width = Screen.width;
        height = Screen.height;
    }

    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {

            // Input.mousePosition�̓Q�[����ʑS�̂̒��ł̃}�E�X�|�C���^�̈ʒu�������Ă���̂�
            // RawImage�ɉf����Ă���WebCamTexture���琳�����F�����ɂ͂��̂܂܂ł͎g�����A���W�ϊ����K�v�Ȃ͂�
            Vector2 pos = Input.mousePosition;

            Debug.Log("x=" + pos.x + "y=" + pos.y);

            if (width < height)
            {
                k = pos.x;
                l = pos.y;
                if ((height * 23 / 100 < l) && (l < height * 78 / 100) && (width * 13 / 100 < k) && (k < width * 88 / 100))
                {
                    Debug.Log("あたり");

                    i++;
                    // RawImageのRectTransformを取得
                    RectTransform rectTransform = transform as RectTransform;

                    // CanvasmのRectTransformを取得
                    Canvas rootCanvas = rectTransform.GetComponentInParent<Canvas>().rootCanvas;

                    // posを自分自身のローカル座標に直す
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        rectTransform,
                        pos,
                        rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera,
                        out Vector2 localPos);

                    // UV座標に換算
                    Rect rect = rectTransform.rect;
                    Vector2 uv = (localPos - rect.position) / rect.size;

                    // RawImage経由でWebCamTextureのサイズを調べる
                    WebCamTexture webCamTexture = this.GetComponent<RawImage>().texture as WebCamTexture;
                    Vector2Int webCamTexSize = new Vector2Int(webCamTexture.width, webCamTexture.height);

                    // UV座標をピクセル単位の位置に換算
                    Vector2 texPos = uv * webCamTexSize;
                    Vector2Int texPosInt = new Vector2Int(
                        Mathf.Clamp((int)texPos.x, 0, webCamTexSize.x - 1),
                        Mathf.Clamp((int)texPos.y * 180, 0, webCamTexSize.y - 1));

                    // WebCamTextureから色を取得
                    Color32 color = webCamTexture.GetPixel(texPosInt.x, texPosInt.y);

                    rlist[i] = color.r;
                    glist[i] = color.g;
                    blist[i] = color.b;

                    //Debug.Log(rlist[0]);
                    //Debug.Log(rlist[1]);
                    //Debug.Log(rlist[2]);

                    image.color = color;
                    //Debug.Log("r="+color.r+"g="+color.g+"b"+color.b);
                    //Debug.Log("i=" + i);

                    if (i > 1)
                    {
                        rsum = rlist.Sum();
                        gsum = glist.Sum();
                        bsum = blist.Sum();

                        rav = rsum / 3;
                        gav = gsum / 3;
                        bav = bsum / 3;

                        Debug.Log(rsum + "ag_r" + rsum / 3 + "," + gsum + "ag_g" + gsum / 3 + "," + bsum + "ag_b" + bsum / 3);

                        cameraPanel.SetActive(false);
                        choicePanel.SetActive(true);

                        text_r.text = "R:" + rav.ToString();
                        text_g.text = "G:" + gav.ToString();
                        text_b.text = "B:" + bav.ToString();


                        Color32 color2 = new Color32((byte)rav, (byte)gav, (byte)bav, 255);
                        image2.color = color2;

                        i = -1;
                    }
                    else
                    {
                        return;
                    }
                }
            }

            else if (height < width)
            {
                n = pos.x;
                j = pos.y;

                if ((205 < j) && (j < 1327) && (464 < n) && (n < 1585))
                {
                    i++;
                    // �܂��������g��RectTransform...�܂�RawImage��RectTransform���擾��...
                    RectTransform rectTransform = transform as RectTransform;

                    // �������g���������Ă���Canvas���擾��...
                    Canvas rootCanvas = rectTransform.GetComponentInParent<Canvas>().rootCanvas;

                    // pos���������g�̃��[�J�����W�ɒ�����...
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        rectTransform,
                        pos,
                        rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera,
                        out Vector2 localPos);

                    // �����UV���W�Ɋ��Z��...
                    Rect rect = rectTransform.rect;
                    Vector2 uv = (localPos - rect.position) / rect.size;

                    // RawImage�o�R��WebCamTexture�̃T�C�Y�𒲂�...
                    WebCamTexture webCamTexture = this.GetComponent<RawImage>().texture as WebCamTexture;
                    Vector2Int webCamTexSize = new Vector2Int(webCamTexture.width, webCamTexture.height);

                    // UV���W���s�N�Z���P�ʂ̈ʒu�Ɋ��Z��...
                    Vector2 texPos = uv * webCamTexSize;
                    Vector2Int texPosInt = new Vector2Int(
                        Mathf.Clamp((int)texPos.x, 0, webCamTexSize.x - 1),
                        Mathf.Clamp((int)texPos.y, 0, webCamTexSize.y - 1));

                    // WebCamTexture����F���擾����
                    Color32 color = webCamTexture.GetPixel(texPosInt.x, texPosInt.y);

                    rlist[i] = color.r;
                    glist[i] = color.g;
                    blist[i] = color.b;

                    //Debug.Log(rlist[0]);
                    //Debug.Log(rlist[1]);
                    //Debug.Log(rlist[2]);

                    image.color = color;
                    //Debug.Log("r="+color.r+"g="+color.g+"b"+color.b);
                    //Debug.Log("i=" + i);

                    if (i > 1)
                    {
                        rsum = rlist.Sum();
                        gsum = glist.Sum();
                        bsum = blist.Sum();

                        rav = rsum / 3;
                        gav = gsum / 3;
                        bav = bsum / 3;

                        Debug.Log(rsum + "ag_r" + rsum / 3 + "," + gsum + "ag_g" + gsum / 3 + "," + bsum + "ag_b" + bsum / 3);

                        cameraPanel.SetActive(false);
                        choicePanel.SetActive(true);

                        text_r.text = "R:" + rav.ToString();
                        text_g.text = "G:" + gav.ToString();
                        text_b.text = "B:" + bav.ToString();


                        Color32 color2 = new Color32((byte)rav, (byte)gav, (byte)bav, 255);
                        image2.color = color2;

                        i = -1;
                    }
                }

                else
                {
                    return;
                }
            }
        }

    }


    public static int getColor()
    {
        return rav;
        return gav;
        return bav;
    }

    public void Button()
    {
        if (gav - bav < 20 || rav - bav < 60)
        {
            Debug.Log("�u���[");
            SceneManager.LoadScene("resultblue");
        }

        else
        {
            Debug.Log("�C�G���[");
            SceneManager.LoadScene("resultyellow");
        }
    }

}
