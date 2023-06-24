using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class BoardSetupManager : MonoBehaviour
{
    public GameObject board;
    public GameObject boardVertice;
    public GameObject boardShadow;
    public GameObject boardInterior;
    public GameObject[] tiles;
    public GameObject[] tile2s;
    public GameObject[] tile3s;

    public float boardSetupDelay = 1f;
    public float boardInteriorSetupDelay = 1f;
    private float fallDelay = 0.2f;

    public event Action OnBoardBaseSetupComplete;
    private bool skipPressed = false;

    private Vector3 boardTargetPosition;
    private Vector3 boardInteriorTargetPosition;

    public void StartSetupBoard()
    {
        board.gameObject.SetActive(true);
        StartCoroutine(SetupBoard());
    }

    private IEnumerator SetupBoard()
    {
        board.transform.position = new Vector3(board.transform.position.x, board.transform.position.y, -30f);
        yield return new WaitForSeconds(boardSetupDelay);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX("Board Drop");
        }

        float startTime = Time.time;
        float journeyDuration = .25f; 
        Vector3 startPosition = board.transform.position;
        Vector3 boardTargetPosition = new Vector3(board.transform.position.x, board.transform.position.y, 0f);

        while (Time.time < startTime + journeyDuration)
        {
            if (skipPressed)
            {
                board.transform.position = boardTargetPosition;
                yield break;
            }

            float t = (Time.time - startTime) / journeyDuration;
            board.transform.position = Vector3.Lerp(startPosition, boardTargetPosition, t);

            yield return null;
        }
        board.transform.position = boardTargetPosition;
        boardVertice.gameObject.SetActive(true);
        boardShadow.gameObject.SetActive(true);
        boardInterior.gameObject.SetActive(true);
        StartCoroutine(SetupBoardInterior());
    }

    private IEnumerator SetupBoardInterior()
    {
        boardInterior.transform.position = new Vector3(boardInterior.transform.position.x, boardInterior.transform.position.y, -30f);
        yield return new WaitForSeconds(boardInteriorSetupDelay);

        if (AudioManager.instance != null)
        {
            AudioManager.instance.StopSFX();
            AudioManager.instance.PlaySFX("Board Drop");
        }

        float startTime = Time.time;
        float journeyDuration = .25f; 
        Vector3 startPosition = boardInterior.transform.position;
        Vector3 boardInteriorTargetPosition = new Vector3(boardInterior.transform.position.x, boardInterior.transform.position.y, 0f);

        while (Time.time < startTime + journeyDuration)
        {
            if (skipPressed)
            {
                boardInterior.transform.position = boardInteriorTargetPosition;
                yield break;
            }

            float t = (Time.time - startTime) / journeyDuration;
            boardInterior.transform.position = Vector3.Lerp(startPosition, boardInteriorTargetPosition, t);
            
                yield return null;
        }

        boardInterior.transform.position = boardInteriorTargetPosition;
        OnBoardBaseSetupComplete?.Invoke();
    }

    public void StartSetupTiles()
    {
        StartCoroutine(SetupTiles());
    }

    private IEnumerator SetupTiles()
    {
        foreach (GameObject tile in tiles)
        {
            tile.SetActive(false);
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            GameObject tile = tiles[i];
            tile.SetActive(true);

            Vector3 startPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, -30f);
            tile.transform.position = startPosition;

            Vector3 targetPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, 0f);

            if (skipPressed)
            {
                tile.transform.position = targetPosition;
                break;
            }
            else
            {
                yield return new WaitForSeconds(fallDelay);
                float startTime = Time.time;
                float duration = 0.225f;

                while (Time.time < startTime + duration)
                {
                    float t = (Time.time - startTime) / duration;
                    tile.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

                    if (t >= 0.8f )
                    {
                        if (AudioManager.instance != null)
                        {
                            AudioManager.instance.StopSFX();
                            AudioManager.instance.PlaySFX("Tile Drop");
                        }
                    }

                    yield return null;
                }

                tile.transform.position = targetPosition;
            }

            if (i == 1 && tile2s != null)
            {
                StartCoroutine(SetupTile2s());
            }

            if (i == 3 && tile3s != null)
            {
                StartCoroutine(SetupTile3s());
            }
        }
    }

    private IEnumerator SetupTile2s()
    {
        foreach (GameObject tile in tile2s)
        {
            tile.SetActive(false);
        }

        for (int i = 0; i < tile2s.Length; i++)
        {
            GameObject tile = tile2s[i];
            tile.SetActive(true);

            Vector3 startPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, -30f);
            tile.transform.position = startPosition;

            Vector3 targetPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, 0f);

            if (skipPressed)
            {
                tile.transform.position = targetPosition;
                break;
            }
            else
            {
                yield return new WaitForSeconds(fallDelay);
                float startTime = Time.time;
                float duration = 0.225f;

                while (Time.time < startTime + duration)
                {
                    float t = (Time.time - startTime) / duration;
                    tile.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                    yield return null;
                }

                tile.transform.position = targetPosition;
            }
        }
    }

    private IEnumerator SetupTile3s()
    {
        foreach (GameObject tile in tile3s)
        {
            tile.SetActive(false);
        }

        for (int i = 0; i < tile3s.Length; i++)
        {
            GameObject tile = tile3s[i];
            tile.SetActive(true);

            Vector3 startPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, -30f);
            tile.transform.position = startPosition;

            Vector3 targetPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, 0f);

            if (skipPressed)
            {
                tile.transform.position = targetPosition;
                break;
            }
            else
            {
                yield return new WaitForSeconds(fallDelay);
                float startTime = Time.time;
                float duration = 0.225f;

                while (Time.time < startTime + duration)
                {
                    float t = (Time.time - startTime) / duration;
                    tile.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
                    yield return null;
                }

                tile.transform.position = targetPosition;
            }
        }
    }


    public void OnSkipButtonPress()
    {
        AudioManager.instance.StopSFX();
        skipPressed = true;
        board.gameObject.SetActive(true);
        boardVertice.gameObject.SetActive(true);
        boardShadow.gameObject.SetActive(true);
        boardInterior.gameObject.SetActive(true);
        ActiveTiles();
        ActiveTile2s();
        ActiveTile3s();
    }

    private void ActiveTiles()
    {
        foreach (GameObject tile in tiles)
        {
            tile.SetActive(true);
        }

        for (int i = 0; i < tiles.Length; i++)
        {
            GameObject tile = tiles[i];
            tile.SetActive(true);

            Vector3 startPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, -30f);
            tile.transform.position = startPosition;

            Vector3 targetPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, 0f);

            tile.transform.position = targetPosition;

        }
    }

    private void ActiveTile2s()
    {
        foreach (GameObject tile in tile2s)
        {
            tile.SetActive(true);
        }

        for (int i = 0; i < tile2s.Length; i++)
        {
            GameObject tile = tile2s[i];
            tile.SetActive(true);

            Vector3 startPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, -30f);
            tile.transform.position = startPosition;

            Vector3 targetPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, 0f);

            tile.transform.position = targetPosition;

        }
    }

    private void ActiveTile3s()
    {
        foreach (GameObject tile in tile3s)
        {
            tile.SetActive(true);
        }

        for (int i = 0; i < tile3s.Length; i++)
        {
            GameObject tile = tile3s[i];
            tile.SetActive(true);

            Vector3 startPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, -30f);
            tile.transform.position = startPosition;

            Vector3 targetPosition = new Vector3(tile.transform.position.x, tile.transform.position.y, 0f);

            tile.transform.position = targetPosition;
            
        }
    }
}
