using System;
using System.Text;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.Networking;
using GameEndProtocols;
using Newtonsoft.Json;
using TMPro;

public class GameEndScript : MonoBehaviour
{
    // : Objects
    public Button Submit;
    public Button GameEnd;
    public Button GameRestart;

    public GameObject EndUI;
    public CanvasGroup cg;

    public TMP_InputField IFIELD_UserName;

    public TextMeshProUGUI EndType;
    public TextMeshProUGUI RankType;
    public TextMeshProUGUI PlayerName1;
    public TextMeshProUGUI PlayerName2;
    public TextMeshProUGUI PlayerName3;
    public TextMeshProUGUI PlayerName4;
    public TextMeshProUGUI PlayerName5;
    public TextMeshProUGUI PlayerName6;
    public TextMeshProUGUI PlayerName7;
    public TextMeshProUGUI PlayerName8;
    public TextMeshProUGUI ClearTime1;
    public TextMeshProUGUI ClearTime2;
    public TextMeshProUGUI ClearTime3;
    public TextMeshProUGUI ClearTime4;
    public TextMeshProUGUI ClearTime5;
    public TextMeshProUGUI ClearTime6;
    public TextMeshProUGUI ClearTime7;
    public TextMeshProUGUI ClearTime8;

    private Packets.find_res final;

    // int ClearType = 2;

    // : Status
    const string SERVER_HOST = "http://localhost:";
    const int SERVER_PORT = 4000;

    // Start is called before the first frame update
    void Start()
    {
        this.Submit.onClick.AddListener(() => {
            PlayerPrefs.SetString("ClearType", "Normal");
            string userId = PlayerPrefs.GetString("PlayerId");
            string userName = IFIELD_UserName.text;
            string startTime = PlayerPrefs.GetString("StartTime");
            Debug.Log(startTime);
            DateTime start = DateTime.ParseExact(startTime, @"MM\/dd\/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            Debug.Log(start);
            TimeSpan ts = DateTime.Now - start;
            Debug.Log(ts);
            string totalTime = ts.ToString();
            Debug.Log(totalTime);
            string userClearTime = totalTime;
            string userClearType = PlayerPrefs.GetString("ClearType");
            Debug.Log(userName);
            Packets.find_req findRequest = new Packets.find_req(userId, userName, userClearTime, userClearType);
            // :: 요청
            this.StartCoroutine(this.RequestFind(findRequest));
            EndType.text = userClearType + " Ending";
        });
        GameEnd.onClick.AddListener(() => OnButtonClick(GameEnd));
        GameRestart.onClick.AddListener(() => OnButtonClick(GameRestart));
        Debug.LogFormat("end {0}", final);
    }

    // : Request
    // :: IEnumerator 사용 이유 = SendWebRequest를 비동기 전송으로 하려고
    private IEnumerator RequestFind(Packets.find_req packet)
    {
        // :: 직렬화
        string json = JsonConvert.SerializeObject(packet);

        // :: 요청객체 생성
        // string url = string.Format("{0}{1}", SERVER_HOST + SERVER_PORT, "/ranking");
        // string url = string.Format("{0}{1}", SERVER_HOST + SERVER_PORT, "/ranking/start");
        string url = string.Format("{0}{1}", SERVER_HOST + SERVER_PORT, "/ranking/end");
        UnityWebRequest webRequest = new UnityWebRequest(url, "POST");

        // :: 바디
        var bodyRaw = Encoding.UTF8.GetBytes(json);

        // :: 이벤트
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        // :: 헤더 설정
        webRequest.SetRequestHeader("Content-Type", "application/json");

        // :: 비동기 전송
        yield return webRequest.SendWebRequest();

        // :: 응답 : 에러 처리
        // if ( webRequest.isNetworkError || webRequest.isHttpError )
        if ((webRequest.result == UnityWebRequest.Result.ConnectionError) || (webRequest.result == UnityWebRequest.Result.ProtocolError))
        {
            Debug.Log("www error");
        }
        else 
        {
            Debug.LogFormat("responseCode: {0}", webRequest.responseCode);

            // :: 데이터 받기
            byte[] resultRaw = webRequest.downloadHandler.data;
            string resultJson = Encoding.UTF8.GetString(resultRaw);
            Debug.LogFormat("resultJson : {0}", resultJson);

            // :: 역직렬화
            final = JsonConvert.DeserializeObject<Packets.find_res>(resultJson);

        }
        this.SetText_Output_Null();
        this.SetText_Output_End(final);
        webRequest.Dispose();
    }

    // : Set
    public void OnButtonClick(Button button)
    {
        if (button == GameEnd) {
            this.StartCoroutine(this.FadeOut());
        } 
        else if (button == GameRestart) {
            this.StartCoroutine(this.FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        // Debug.LogFormat("cg.alpha {0}", cg.alpha);
        float fadeCount = 1;
        while (fadeCount > 0.0f)
        {
            // Debug.LogFormat("cg.alpha in {0}", cg.alpha);
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            cg.alpha = fadeCount;
            // Debug.LogFormat("cg.alpha minus {0}", cg.alpha);
        }
        EndUI.SetActive(false);
    }

    private void SetText_Output_Null()
    {
        this.PlayerName1.enabled = false;
        this.ClearTime1.enabled = false;
        this.PlayerName2.enabled = false;
        this.ClearTime2.enabled = false;
        this.PlayerName3.enabled = false;
        this.ClearTime3.enabled = false;
        this.PlayerName4.enabled = false;
        this.ClearTime4.enabled = false;
        this.PlayerName5.enabled = false;
        this.ClearTime5.enabled = false;
        this.PlayerName6.enabled = false;
        this.ClearTime6.enabled = false;
        this.PlayerName7.enabled = false;
        this.ClearTime7.enabled = false;
        this.PlayerName8.enabled = false;
        this.ClearTime8.enabled = false;
    }

    private void SetText_Output_End(Packets.find_res final)
    {
        RankType.text = PlayerPrefs.GetString("ClearType") + " Ending";
        for (int i = 0; i<final.end.Count; i++)
        {
            // Debug.Log(i);
            if (final.end[i] != null){
                this.WriteText(final.end[i], i);
                // Debug.LogFormat("End : {0}", final.end[i].clear_time);
            }
        }
    }

    private void WriteText(Packets.info_userData data, int i)
    {
        string userName = "";
        string userClearTime = "";
        if (data != null)
        {
            userName += string.Format("{0}", data.name);
            userClearTime += string.Format("{0}", data.clear_time);

            switch (i)
            {
                case 0:
                    OutputText1(userName, userClearTime);
                    // Debug.Log("1");
                    break;
                case 1:
                    OutputText2(userName, userClearTime);
                    // Debug.Log("2");
                    break;
                case 2:
                    OutputText3(userName, userClearTime);
                    // Debug.Log("3");
                    break;
                case 3:
                    OutputText4(userName, userClearTime);
                    // Debug.Log("4");
                    break;
                case 4:
                    OutputText5(userName, userClearTime);
                    // Debug.Log("5");
                    break;
                case 5:
                    OutputText6(userName, userClearTime);
                    // Debug.Log("6");
                    break;
                case 6:
                    OutputText7(userName, userClearTime);
                    // Debug.Log("7");
                    break;
                case 7:
                    OutputText8(userName, userClearTime);
                    // Debug.Log("8");
                    break;
            }
        }
    }

    private void OutputText1(string name, string clear_time)
    {
        this.PlayerName1.enabled = true;
        this.ClearTime1.enabled = true;
        this.PlayerName1.text = name;
        this.ClearTime1.text = clear_time;
    }

    private void OutputText2(string name, string clear_time)
    {
        this.PlayerName2.enabled = true;
        this.ClearTime2.enabled = true;
        this.PlayerName2.text = name;
        this.ClearTime2.text = clear_time;
    }

    private void OutputText3(string name, string clear_time)
    {
        this.PlayerName3.enabled = true;
        this.ClearTime3.enabled = true;
        this.PlayerName3.text = name;
        this.ClearTime3.text = clear_time;
    }

    private void OutputText4(string name, string clear_time)
    {
        this.PlayerName4.enabled = true;
        this.ClearTime4.enabled = true;
        this.PlayerName4.text = name;
        this.ClearTime4.text = clear_time;
    }

    private void OutputText5(string name, string clear_time)
    {
        this.PlayerName5.enabled = true;
        this.ClearTime5.enabled = true;
        this.PlayerName5.text = name;
        this.ClearTime5.text = clear_time;
    }

    private void OutputText6(string name, string clear_time)
    {
        this.PlayerName6.enabled = true;
        this.ClearTime6.enabled = true;
        this.PlayerName6.text = name;
        this.ClearTime6.text = clear_time;
    }

    private void OutputText7(string name, string clear_time)
    {
        this.PlayerName7.enabled = true;
        this.ClearTime7.enabled = true;
        this.PlayerName7.text = name;
        this.ClearTime7.text = clear_time;
    }

    private void OutputText8(string name, string clear_time)
    {
        this.PlayerName8.enabled = true;
        this.ClearTime8.enabled = true;
        this.PlayerName8.text = name;
        this.ClearTime8.text = clear_time;
    }
}
