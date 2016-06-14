using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkManager {

    private int TeamID = -1;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        TeamID++;
        //base.OnServerAddPlayer(conn, playerControllerId);
        GameObject player = (GameObject)GameObject.Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        player.GetComponent<Player>().TeamID = TeamID;
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
