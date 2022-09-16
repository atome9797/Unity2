using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField _nickname;

    [SerializeField]
    private Button _joinButton;
    private TextMeshProUGUI _joinButtonText;

    private void deactivateJoinButton(string message)
    {
        _joinButton.interactable = false;
        _joinButtonText.text = message;
    }

    private void activateJoinButton()
    {
        _joinButton.interactable = true;
        _joinButtonText.text = "입장하기";
    }

    private void Awake()
    {
        // 버튼의 텍스트 참조
        _joinButtonText = _joinButton.GetComponentInChildren<TextMeshProUGUI>();

        // 버튼 이벤트 메소드 연결
        _joinButton.onClick.AddListener(OnClickedJoinButton);

        // 마스터 서버와 연결 시도
        PhotonNetwork.ConnectUsingSettings();

        // UI 표시 변경
        deactivateJoinButton("접속 시도");
    }

    public override void OnConnectedToMaster()
    {
        // 로그 찍음
        Debug.Log("마스터 서버에 접속 됨.");

        // 버튼 활성화
        activateJoinButton();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // 연결 다시 시도
        PhotonNetwork.ConnectUsingSettings();

        // 버튼 비활성화
        deactivateJoinButton("재접속 시도");
    }

    private static readonly RoomOptions RandomRoomOptions = new RoomOptions()
    {
        MaxPlayers = 20
    };

    private void OnClickedJoinButton()
    {
        if (_nickname.text.Length == 0)
        {
            Debug.Log("닉네임을 입력하세요.");

            return;
        }

        Data data = FindObjectOfType<Data>();
        data.Nickname = _nickname.text;
        Debug.Log($"입력된 닉네임 : {data.Nickname}");
        PhotonNetwork.JoinOrCreateRoom("Metaverse", RandomRoomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        // 로그 남김
        Debug.Log("방에 입장함.");

        // 레벨 이동
        PhotonNetwork.LoadLevel("Main");
    }
}
