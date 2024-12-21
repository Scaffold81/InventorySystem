using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace InventoryTest.Inventory.Network
{
    /// <summary>
    /// Sending a request to the server
    /// </summary>
    public class InventoryInteractionSend : MonoBehaviour
    {
        private string serverUrl = "https://wadahub.manerai.com/api/inventory/status";
        private string bearerToken = "kPERnYcWAY46xaSy8CEzanosAgsWM84Nx7SKM4QBSqPq6c7StWfGxzhxPfDh8MaP";



        public void SendInventoryEvent(string itemId, string inventoryEvent)
        {
            StartCoroutine(PostInventoryEvent(itemId, inventoryEvent));
        }

        private IEnumerator PostInventoryEvent(string itemId, string inventoryEvent)
        {
            WWWForm form = new WWWForm();
            form.AddField("item_id", itemId);
            form.AddField("event", inventoryEvent);

            UnityWebRequest request = UnityWebRequest.Post(serverUrl, form);
            request.SetRequestHeader("Authorization", "Bearer " + bearerToken);


            yield return request.SendWebRequest();


            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error sending request: " + request.error);
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);
            }
        }
    }
}
