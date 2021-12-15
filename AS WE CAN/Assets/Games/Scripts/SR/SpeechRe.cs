using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SpeechRe : MonoBehaviour
{
    public string APP_ID = "14131279";
    public string API_KEY = "E4kRdXVk4GmFROT6IubGGXpy";
    public string SECRET_KEY = "duBtLVsVHxQ81r8E59ScSzRR8BRGhG9c";

    /// <summary>
    /// ��ȡtocken�������ַ
    /// ����json����,ÿ�λ�ȡtoken���ݶ���һ��
    /// </summary>
    public string GetTokenUrl = "https://aip.baidubce.com/oauth/2.0/token";
    /// <summary>
    /// ��ȡ����Token
    /// </summary>
    public string Access_Token = "";

    /// <summary>
    /// ¼����ť
    /// </summary>
    [Header("¼����ť")]
    public Button RecordButton;

    [Header("���Text")]
    public Text ResultTest;

    [Header("�Ƿ���¼����״̬")]
    public bool isRecording = false;
    [Header("��˷��豸������")]
    public string MicrophoneName = "";
    /// <summary>
    /// ¼�Ƶ���ƵƬ��
    /// </summary>
    public AudioClip RecordAudioClip;

    /// <summary>
    /// ������Ƶ����Դ
    /// </summary>
    public AudioSource audioSource;

    /// <summary>
    /// ����ʶ���ַ
    /// </summary>
    public string SpeechRecognition_Address = "https://vop.baidu.com/server_api";
    /// <summary>
    /// �����ϳɵ�ַ
    /// </summary>
    public string SpeechSynthesis_Address = "http://tsn.baidu.com/text2audio";
    [Header("ʶ�𵽵�����")]
    public string RecognizeContent = "";
    [Header("¼����ʱ��")]
    public int recordTime = 5;

    private float interval = 1.5f;
    private float timer = 0;


    private void Start()
    {
        GetToken(GetTokenUrl);

        //RecordButton.onClick.AddListener(OnRecordButtonClick);

        //��ȡ�����ϵ�һ��¼���豸������
        if (Microphone.devices.Length > 0)
        {
            MicrophoneName = Microphone.devices[0];
        }
        else
        {
            Debug.LogError("��ǰ�豸ȱ����˷��豸����¼��");
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        OnRecord();
    }

    private void OnRecord()
    {
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                Debug.Log("����");
                timer = 0;
                Microphone.End(MicrophoneName);
                StartCoroutine(Recognition(RecordAudioClip, recordTime));
            }

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (timer == 0)
            {
                Debug.Log("��ʼ");
                timer = interval;

                RecordAudioClip = Microphone.Start(MicrophoneName, false, recordTime, 16000);

            }
        }
    }

    /// <summary>
    /// ¼����ť����¼�
    /// </summary>
    private void OnRecordButtonClick()
    {
        if (isRecording == false)
        {
            //��ʼ¼��
            isRecording = true;
            //��ʼ¼��,¼�Ƴ���5��,Ƶ��16000
            RecordAudioClip = Microphone.Start(MicrophoneName, false, recordTime, 16000);

            //RecordButton.GetComponent<Image>().color = Color.red;
        }
        else
        {
            //����¼��
            isRecording = false;

            Microphone.End(MicrophoneName);
            //�޸İ�ť����ɫ
            //RecordButton.GetComponent<Image>().color = Color.white;
            //����¼�Ƶ�����
            //audioSource.PlayOneShot(RecordAudioClip);
            //��ʼ����ʶ��
            StartCoroutine(Recognition(RecordAudioClip, recordTime));

            //Debug.Log(GetVolume(RecordAudioClip));
        }
    }

    /// <summary>
    /// ��ȡtoken
    /// </summary>
    /// <param name="url"></param>
    public void GetToken(string url)
    {
        WWWForm form = new WWWForm();
        form.AddField("grant_type", "client_credentials");
        form.AddField("client_id", API_KEY);
        form.AddField("client_secret", SECRET_KEY);

        StartCoroutine(HttpPostRequest(url, form));
    }

    /// <summary>
    /// post http����
    /// </summary>
    /// <param name="url">����ĵ�ַ</param>
    /// <param name="form">�����ַ���Ĳ���</param>
    /// <returns></returns>
    IEnumerator HttpPostRequest(string url, WWWForm form)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, form);

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.isNetworkError)
        {
            Debug.Log("�������:" + unityWebRequest.error);
        }
        else
        {
            if (unityWebRequest.responseCode == 200)
            {
                string result = unityWebRequest.downloadHandler.text;
                print("�ɹ���ȡ������:" + result);
                OnGetHttpResponse_Success(result);
            }
            else
            {
                print("״̬�벻Ϊ200:" + unityWebRequest.responseCode);
            }
        }

    }

    /// <summary>
    /// ���ɹ���ȡ�����������ص�json����ʱ,���н���
    /// </summary>
    /// <param name="result">json����</param>
    private void OnGetHttpResponse_Success(string result)
    {
        MyAccessToken accessToken = JsonMapper.ToObject<MyAccessToken>(result);
        Access_Token = accessToken.access_token;
    }

    /// <summary>
    /// ����ʶ��
    /// </summary>
    /// <param name="audioClip"></param>
    /// <returns></returns>
    IEnumerator Recognition(AudioClip audioClip, int recordTime)
    {
        WWWForm form = new WWWForm();

        string url = string.Format("{0}?(dev_pid=1737||dev_pid=1537)&cuid={1}&token={2}", SpeechRecognition_Address, SystemInfo.deviceUniqueIdentifier, Access_Token);
        float[] samples = new float[16000 * recordTime * audioClip.channels];
        //��audioclip��䵽������
        audioClip.GetData(samples, 0);
        short[] sampleShort = new short[samples.Length];
        for (int i = 0; i < samples.Length; i++)
        {
            sampleShort[i] = (short)(samples[i] * short.MaxValue);
        }

        byte[] data = new byte[sampleShort.Length * 2];
        Buffer.BlockCopy(sampleShort, 0, data, 0, data.Length);



        form.AddBinaryData("audio", data);
        UnityWebRequest request = UnityWebRequest.Post(url, form);
        request.SetRequestHeader("Content-Type", "audio/pcm;rate=16000");
        yield return request.SendWebRequest();

        if (request.isNetworkError)
        {
            Debug.Log("�������:" + request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                string result = request.downloadHandler.text;
                print("�ɹ���ȡ������:" + result);

                MyRecognizeResult resultContent = JsonMapper.ToObject<MyRecognizeResult>(result);
                RecognizeContent = resultContent.result[0];
                RecognizeResult(RecognizeContent);
                //Debug.Log(RecognizeContent);
                //ResultTest.GetComponent<Text>().text = resultContent.result[0];
                //RecordButton.GetComponentInChildren<Text>().text = resultContent.result[0];
            }
            else
            {
                print("״̬�벻Ϊ200:" + request.responseCode);
            }
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    private float GetMaxVolume(AudioClip audioClip)
    {
        float maxVolume = 0f;
        int maxTime = 16000 * recordTime * audioClip.channels;

        float[] volumeData = new float[maxTime];

        audioClip.GetData(volumeData, 0);

        for (int i = 0; i < maxTime; i++)
        {
            float tempMax = volumeData[i];
            if (maxVolume < tempMax)
            {
                maxVolume = tempMax;
            }
        }

        return maxVolume;
    }

    /// <summary>
    /// ʶ����
    /// </summary>
    private void RecognizeResult(string recognize)
    {
        if(recognize.IndexOf("��")!=-1)
        {
            Debug.Log("ʶ��ɹ�");
            gameObject.transform.GetChild(0).GetComponent<VolumeGet>().SetTime();
        }
        /*for(int i=0;i<recognize.Length;i++)
        {
            if (recognize[i] == '��')
            {
                Debug.Log("hhh");
            }
        }
        if (recognize == "��?")
        //{
        //    Debug.Log("hhh");
        //}*/
    }

}


/// <summary>
/// AccessToken���л�json�Ķ���
/// </summary>
public class MyAccessToken
{
    public string access_token;

    public int expires_in;

    public string session_key;

    public string scope;

    public string refresh_token;

    public string session_secret;

    /*
    
    {
    "access_token": "24.b243f17d64fa69b413d827f6a0965846.2592000.1542375343.282335-14131279",
    "session_key": "9mzdWWhYL0oUaqTY7WohNY0Fhd8Wxm4M7t4bTtlaq9/fyw7RXgztqR8+tmnAFpgywswOL3CQsU/v6PZ3ijK91/RmmiLb9Q==",
    "scope": "audio_voice_assistant_get audio_tts_post public brain_all_scope wise_adapt lebo_resource_base lightservice_public hetu_basic lightcms_map_poi kaidian_kaidian ApsMisTest_TestȨ�� vis-classify_flower lpq_���� cop_helloScope ApsMis_fangdi_permission smartapp_snsapi_base iop_autocar oauth_tp_app smartapp_smart_game_openapi oauth_sessionkey",
    "refresh_token": "25.c2cf87484f244b6ef3d1d6a330727700.315360000.1855143343.282335-14131279",
    "session_secret": "7b9a68a03cbad17db3d13985dc7690d2",
    "expires_in": 2592000
}
    

    */
}

/// <summary>
/// ����ʶ��ɹ���,���ص�json���ݸ�ʽ
/// </summary>
public class MyRecognizeResult
{
    public string corpus_no;

    public string err_msg;

    public int err_no;
    /// <summary>
    /// ����ʶ�𵽵Ľ��
    /// </summary>
    public List<string> result;

    public string sn;

    //{"corpus_no":"6612962645817945596","err_msg":"success.","err_no":0,"result":["�������"],"sn":"845877030391539700349"}
}
