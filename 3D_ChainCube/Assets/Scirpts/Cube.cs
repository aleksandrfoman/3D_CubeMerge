using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Cube : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private new Renderer renderer;
    [SerializeField]
    private new Rigidbody rigidbody;
    [SerializeField]
    private new Collider collider;
    [SerializeField]
    private TMP_Text[] textsNumbers;
    [SerializeField]
    private CubeSettings[] cubeSettings;
    public int CubeSettingsLength => cubeSettings.Length;
    [SerializeField]
    private int indexCube;
    public int IndexCube => indexCube;
    [SerializeField]
    private float shootForce;
    [SerializeField]
    private float jumpForce;

    private bool isDestroy = false;
    public bool IsDestroy => isDestroy;
    public void SetSettings(int index)
    {
        indexCube = index;
        renderer.material = cubeSettings[index].MaterialCube;
        SetNubmer(cubeSettings[index].Number);
    }
    private void SetNubmer(int value)
    {
        for (int i = 0; i < textsNumbers.Length; i++)
        {
            textsNumbers[i].text = value.ToString();
        }
    }

    public void ShootCube()
    {
        rigidbody.AddForce(Vector3.forward * shootForce, ForceMode.Impulse);
    }
    public void UpdateCube()
    {
        indexCube++;
        if (indexCube > GameController.Instance.MaxIndex)
        {
            GameController.Instance.SetMaxIndex(indexCube);
        }
        if (indexCube == CubeSettingsLength-1)
        {
            GameController.Instance.ActiveWinPanel();
        }
        if (transform.position.z<GameController.Instance.FinishCoord)
        {
            GameController.Instance.ActivaeLosePanel();
        }
        SetSettings(indexCube);
        GameController.Instance.UpdateScore(cubeSettings[indexCube].Number);

        rigidbody.velocity = Vector3.zero;
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    public void DestroyCube()
    {
        isDestroy = true;
        
        Destroy(gameObject);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (!isDestroy)
        {
            if (collision.gameObject.TryGetComponent(out Cube collisionCube))
            {
                collider.material = null;
                if (indexCube != CubeSettingsLength-1 && collisionCube.indexCube != CubeSettingsLength- 1 && !collisionCube.IsDestroy)
                {
                    if (collisionCube.indexCube == indexCube)
                    {
                        collisionCube.DestroyCube();
                        UpdateCube();
                    }
                }
            }
            else if (collision.gameObject.GetComponent<StopWall>())
            {
                collider.material = null;
            }
        }
    }

    [System.Serializable]
    private struct CubeSettings
    {
        [SerializeField]
        private Material materialCube;
        public Material MaterialCube => materialCube;
        [SerializeField]
        private int number;
        public int Number => number;
    }

}
