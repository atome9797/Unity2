using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
using UnityEngine;
using UnityEngine.UI;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class LobbyManager : MonoBehaviourPunCallbacks {
    private static readonly string GAME_VERSION = "1"; // 게임 버전

    public Text ConnectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button JoinButton; // 룸 접속 버튼

    // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start() 
    {
        // 접속에 필요한 정보를 설정한다.
        PhotonNetwork.GameVersion = GAME_VERSION;

        // 마스터 서버로 접속을 시도한다.
        PhotonNetwork.ConnectUsingSettings();

        // UI 표시
        JoinButton.interactable = false;
        ConnectionInfoText.text = "마스터 서버에 접속 중...";
    }

    // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster() 
    {
        //UI 표시
        JoinButton.interactable = true;
        ConnectionInfoText.text = "온라인 : 마스터 서버와 접속 됨";
    }

    // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause) 
    {
        //룸 접속 버튼 비활성화
        JoinButton.interactable = false;

        //접속 정보 표시
        ConnectionInfoText.text = " 오프라인 : 마스터 서버와 연결되지 않음\n접속 재시도 중...";

        //마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

    
    // 룸 session 접속 시도
    public void Connect() 
    {
        // 접속 버튼을 비활성화
        JoinButton.interactable = false;
        //서버에 접속중이냐
        if (PhotonNetwork.IsConnected)
        {
            // 접속을 실행  
            ConnectionInfoText.text = "룸에 접속...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // 아니라면
            ConnectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n 접속 재시도 중...";
            // 다시 마스터 서버에 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    private static readonly RoomOptions ROOM_OPTIONS = new RoomOptions()
    {
        MaxPlayers = 4
    };

    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message){

        //UI 표시
        ConnectionInfoText.text = "방 생성...";

        // 방 생성
        PhotonNetwork.CreateRoom(null, ROOM_OPTIONS);
    }

    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom() 
    {
        //UI 표시
        ConnectionInfoText.text = "방에 참가합니다.";

        //모든 클라이언트  Main 씬 로드
        PhotonNetwork.LoadLevel("Main");
    }
}